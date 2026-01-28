Imports System.Net
Imports System.Net.Mail
Imports System.Text.RegularExpressions
Imports System.IO

Public Class Form1

    ' Store seen listing IDs with prices to detect new ones and price changes
    Private SeenListings As New Dictionary(Of String, Decimal) ' ID -> Price
    Private SettingsFile As String = "settings.txt"
    Private SeenListingsFile As String = "seen_listings.txt"
    Private SearchesFile As String = "searches.txt"
    Private ScrapeCount As Integer = 0
    Private EmailPanelCollapsed As Boolean = True
    Private WithEvents NotifyIcon1 As NotifyIcon
    Private WithEvents ContextMenu1 As ContextMenuStrip
    Private WithEvents TimerCycle As New Timer() ' Timer for full cycle repeat (minutes)
    
    ' Search configurations (up to 10)
    Private SearchConfigs As New List(Of SearchConfig)
    Private Const MaxSearches As Integer = 10
    Private CurrentSearchIndex As Integer = 0
    Private IsRunningCycle As Boolean = False

    ' Category definitions for bazos.sk
    Private Categories As New Dictionary(Of String, String) From {
        {"Auto", "auto"},
        {"Deti (Children)", "deti"},
        {"Dom a záhrada (Home & Garden)", "dom"},
        {"Elektro", "elektro"},
        {"Foto", "foto"},
        {"Hudba (Music)", "hudba"},
        {"Knihy (Books)", "knihy"},
        {"Mobily (Mobile Phones)", "mobil"},
        {"Motocykle (Motorcycles)", "motocykle"},
        {"Nábytok (Furniture)", "nabytok"},
        {"Oblečenie (Clothing)", "oblecenie"},
        {"PC", "pc"},
        {"Práca (Jobs)", "praca"},
        {"Reality (Real Estate)", "reality"},
        {"Služby (Services)", "sluzby"},
        {"Stroje (Machines)", "stroje"},
        {"Šport (Sport)", "sport"},
        {"Vstupenky (Tickets)", "vstupenky"},
        {"Zvieratá (Animals)", "zvierata"},
        {"Ostatné (Other)", "ostatne"}
    }

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Populate category dropdown
        For Each cat In Categories
            ComboBoxCategory.Items.Add(cat.Key)
        Next
        ComboBoxCategory.SelectedIndex = 0

        ' Load seen listings from file
        LoadSeenListings()
        
        ' Load search configurations
        LoadSearchConfigs()

        ' Try to load saved settings
        If File.Exists(SettingsFile) Then
            LoadSettings()
        End If

        AddLog("Application started. Ready to scrape bazos.sk")
        
        ' Start with email panel collapsed
        CollapseEmailPanel()
        
        ' Setup system tray icon
        SetupNotifyIcon()
        
        ' Update search list display
        UpdateSearchListDisplay()
    End Sub

    Private Sub SetupNotifyIcon()
        NotifyIcon1 = New NotifyIcon()
        NotifyIcon1.Text = "BAZscrap - Bazos.sk Scraper"
        NotifyIcon1.Icon = Me.Icon
        NotifyIcon1.Visible = False
        
        ' Create context menu for tray icon
        ContextMenu1 = New ContextMenuStrip()
        ContextMenu1.Items.Add("Show", Nothing, AddressOf ShowFromTray)
        ContextMenu1.Items.Add("-")
        ContextMenu1.Items.Add("Exit", Nothing, AddressOf ExitFromTray)
        NotifyIcon1.ContextMenuStrip = ContextMenu1
    End Sub

    Private Sub ShowFromTray(sender As Object, e As EventArgs)
        Me.Show()
        Me.WindowState = FormWindowState.Normal
        NotifyIcon1.Visible = False
    End Sub

    Private Sub ExitFromTray(sender As Object, e As EventArgs)
        NotifyIcon1.Visible = False
        Application.Exit()
    End Sub

    Private Sub NotifyIcon1_DoubleClick(sender As Object, e As EventArgs) Handles NotifyIcon1.DoubleClick
        ShowFromTray(sender, e)
    End Sub

    Private Sub ButtonMinimizeToTray_Click(sender As Object, e As EventArgs) Handles ButtonMinimizeToTray.Click
        Me.Hide()
        NotifyIcon1.Visible = True
        NotifyIcon1.ShowBalloonTip(2000, "BAZscrap", "Running in background. Double-click to restore.", ToolTipIcon.Info)
    End Sub

    Private Sub ButtonToggleEmail_Click(sender As Object, e As EventArgs) Handles ButtonToggleEmail.Click
        If EmailPanelCollapsed Then
            ExpandEmailPanel()
        Else
            CollapseEmailPanel()
        End If
    End Sub

    Private Sub CollapseEmailPanel()
        GroupBoxEmail.Visible = False
        EmailPanelCollapsed = True
        ButtonToggleEmail.Text = "▶ Email Settings"
        
        ' Move interval box right after toggle button
        GroupBoxInterval.Top = ButtonToggleEmail.Bottom + 6
        
        ' Move buttons below interval
        ButtonStart.Top = GroupBoxInterval.Bottom + 6
        ButtonStop.Top = ButtonStart.Top
        ButtonMinimizeToTray.Top = ButtonStart.Top
        ButtonClearHistory.Top = ButtonStart.Top
        
        ' Move status and log
        LabelStatus.Top = ButtonStart.Bottom + 6
        LabelLog.Top = LabelStatus.Bottom + 6
        ListBoxLog.Top = LabelLog.Bottom + 3
        
        ' Resize form
        Me.ClientSize = New Size(Me.ClientSize.Width, ListBoxLog.Bottom + 12)
    End Sub

    Private Sub ExpandEmailPanel()
        GroupBoxEmail.Visible = True
        EmailPanelCollapsed = False
        ButtonToggleEmail.Text = "▼ Email Settings"
        
        ' Position email box right after toggle button
        GroupBoxEmail.Top = ButtonToggleEmail.Bottom + 6
        
        ' Move interval box below email
        GroupBoxInterval.Top = GroupBoxEmail.Bottom + 6
        
        ' Move buttons below interval
        ButtonStart.Top = GroupBoxInterval.Bottom + 6
        ButtonStop.Top = ButtonStart.Top
        ButtonMinimizeToTray.Top = ButtonStart.Top
        ButtonClearHistory.Top = ButtonStart.Top
        
        ' Move status and log
        LabelStatus.Top = ButtonStart.Bottom + 6
        LabelLog.Top = LabelStatus.Bottom + 6
        ListBoxLog.Top = LabelLog.Bottom + 3
        
        ' Resize form
        Me.ClientSize = New Size(Me.ClientSize.Width, ListBoxLog.Bottom + 12)
    End Sub

    Private Sub ButtonAddSearch_Click(sender As Object, e As EventArgs) Handles ButtonAddSearch.Click
        If SearchConfigs.Count >= MaxSearches Then
            MessageBox.Show($"Maximum {MaxSearches} searches allowed.", "Limit Reached", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If
        
        If String.IsNullOrWhiteSpace(TextBoxSearchPhrase.Text) Then
            MessageBox.Show("Please enter a search phrase.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If
        
        Dim config As New SearchConfig()
        config.SearchPhrase = TextBoxSearchPhrase.Text.Trim()
        config.CategoryName = ComboBoxCategory.SelectedItem.ToString()
        config.CategorySubdomain = Categories(config.CategoryName)
        config.MinPrice = NumericMinPrice.Value
        
        SearchConfigs.Add(config)
        SaveSearchConfigs()
        UpdateSearchListDisplay()
        
        ' Clear input
        TextBoxSearchPhrase.Text = ""
        NumericMinPrice.Value = 0
        
        AddLog($"Added search: '{config.SearchPhrase}' in {config.CategoryName}")
    End Sub
    
    Private Sub UpdateSearchListDisplay()
        PanelSearchList.Controls.Clear()
        
        Dim yPos As Integer = 5
        For i As Integer = 0 To SearchConfigs.Count - 1
            Dim config = SearchConfigs(i)
            Dim index As Integer = i
            
            Dim lbl As New Label()
            lbl.Text = $"{i + 1}. {config.SearchPhrase} | {config.CategoryName} | Min: {config.MinPrice}€"
            lbl.Location = New Point(5, yPos + 3)
            lbl.Size = New Size(300, 20)
            lbl.Font = New Font("Segoe UI", 8)
            PanelSearchList.Controls.Add(lbl)
            
            Dim btnDelete As New Button()
            btnDelete.Text = "🗑"
            btnDelete.Location = New Point(310, yPos)
            btnDelete.Size = New Size(30, 22)
            btnDelete.Tag = index
            btnDelete.FlatStyle = FlatStyle.Flat
            AddHandler btnDelete.Click, AddressOf DeleteSearch_Click
            PanelSearchList.Controls.Add(btnDelete)
            
            yPos += 26
        Next
        
        If SearchConfigs.Count = 0 Then
            Dim lbl As New Label()
            lbl.Text = "No searches configured. Add one above."
            lbl.Location = New Point(5, 5)
            lbl.Size = New Size(330, 20)
            lbl.ForeColor = Color.Gray
            PanelSearchList.Controls.Add(lbl)
        End If
        
        LabelSearchCount.Text = $"Searches: {SearchConfigs.Count}/{MaxSearches}"
    End Sub
    
    Private Sub DeleteSearch_Click(sender As Object, e As EventArgs)
        Dim btn As Button = CType(sender, Button)
        Dim index As Integer = CInt(btn.Tag)
        
        If index >= 0 AndAlso index < SearchConfigs.Count Then
            Dim config = SearchConfigs(index)
            Dim result = MessageBox.Show($"Delete search '{config.SearchPhrase}'?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If result = DialogResult.Yes Then
                AddLog($"Removed search: '{config.SearchPhrase}'")
                SearchConfigs.RemoveAt(index)
                SaveSearchConfigs()
                UpdateSearchListDisplay()
            End If
        End If
    End Sub

    Private Sub ButtonStart_Click(sender As Object, e As EventArgs) Handles ButtonStart.Click
        If SearchConfigs.Count = 0 Then
            MessageBox.Show("Please add at least one search configuration.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        If String.IsNullOrWhiteSpace(TextBoxSmtpServer.Text) OrElse
           String.IsNullOrWhiteSpace(TextBoxSmtpUsername.Text) OrElse
           String.IsNullOrWhiteSpace(TextBoxSmtpPassword.Text) OrElse
           String.IsNullOrWhiteSpace(TextBoxEmailTo.Text) Then
            MessageBox.Show("Please fill in email settings before starting." & vbCrLf & vbCrLf & 
                           "Click '▶ Email Settings' to expand and configure.", 
                           "Email Settings Required", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ' TimerScrape = seconds between individual searches within a cycle
        TimerScrape.Interval = CInt(NumericIntervalSeconds.Value) * 1000
        
        ' TimerCycle = minutes between full cycles
        TimerCycle.Interval = CInt(NumericIntervalMinutes.Value) * 60 * 1000
        
        CurrentSearchIndex = 0
        
        ButtonStart.Enabled = False
        ButtonStop.Enabled = True
        LabelStatus.Text = $"Status: Running ({SearchConfigs.Count} searches)"
        LabelStatus.ForeColor = Color.Green

        AddLog($"Scraping started. {SearchConfigs.Count} searches")
        AddLog($"Cycle repeats every {NumericIntervalMinutes.Value} minutes")
        AddLog($"Delay between searches: {NumericIntervalSeconds.Value} seconds")

        ' Start first cycle immediately
        StartNewCycle()
    End Sub
    
    Private Sub StartNewCycle()
        CurrentSearchIndex = 0
        IsRunningCycle = True
        AddLog("═══════════════════════════════════════")
        AddLog("Starting new search cycle...")
        PerformScrape()
    End Sub

    Private Sub ButtonStop_Click(sender As Object, e As EventArgs) Handles ButtonStop.Click
        TimerScrape.Stop()
        TimerCycle.Stop()
        IsRunningCycle = False
        ButtonStart.Enabled = True
        ButtonStop.Enabled = False
        LabelStatus.Text = "Status: Stopped"
        LabelStatus.ForeColor = Color.Gray
        AddLog("Scraping stopped.")
    End Sub

    Private Sub NumericIntervalMinutes_ValueChanged(sender As Object, e As EventArgs) Handles NumericIntervalMinutes.ValueChanged
        If TimerCycle.Enabled Then
            TimerCycle.Interval = CInt(NumericIntervalMinutes.Value) * 60 * 1000
            AddLog($"Cycle interval changed to {NumericIntervalMinutes.Value} minutes")
        End If
    End Sub
    
    Private Sub NumericIntervalSeconds_ValueChanged(sender As Object, e As EventArgs) Handles NumericIntervalSeconds.ValueChanged
        If TimerScrape.Enabled Then
            TimerScrape.Interval = CInt(NumericIntervalSeconds.Value) * 1000
            AddLog($"Search delay changed to {NumericIntervalSeconds.Value} seconds")
        End If
    End Sub

    Private Sub TimerScrape_Tick(sender As Object, e As EventArgs) Handles TimerScrape.Tick
        PerformScrape()
    End Sub
    
    Private Sub TimerCycle_Tick(sender As Object, e As EventArgs) Handles TimerCycle.Tick
        StartNewCycle()
    End Sub

    Private Sub PerformScrape()
        If SearchConfigs.Count = 0 Then Return
        
        ' Stop the between-search timer while processing
        TimerScrape.Stop()
        
        Dim config = SearchConfigs(CurrentSearchIndex)
        
        Try
            ScrapeCount += 1
            AddLog($"Search {CurrentSearchIndex + 1}/{SearchConfigs.Count}: '{config.SearchPhrase}'")

            Dim searchPhrase As String = Uri.EscapeDataString(config.SearchPhrase)
            Dim searchUrl As String = $"https://{config.CategorySubdomain}.bazos.sk/?hledat={searchPhrase}&hlokalita=&humkreis=0&cenaod=&cenado="

            AddLog($"Category: {config.CategoryName}, Min price: {config.MinPrice}€")

            Dim html As String = DownloadPage(searchUrl)

            If String.IsNullOrEmpty(html) Then
                AddLog("ERROR: Failed to download page!")
                AdvanceToNextSearch()
                Return
            End If

            AddLog($"Downloaded {html.Length} characters")
            
            Dim listings As List(Of Listing) = ParseListings(html, config.CategorySubdomain)
            AddLog($"Found {listings.Count} listings, tracking {SeenListings.Count} in history")

            Dim newListings As New List(Of Listing)
            Dim priceChangedListings As New List(Of Listing)
            Dim skippedByPrice As Integer = 0

            For Each listing In listings
                If config.MinPrice > 0 AndAlso listing.Price < config.MinPrice Then
                    skippedByPrice += 1
                    Continue For
                End If
                
                If Not SeenListings.ContainsKey(listing.Id) Then
                    newListings.Add(listing)
                    SeenListings(listing.Id) = listing.Price
                ElseIf SeenListings(listing.Id) <> listing.Price Then
                    listing.OldPrice = SeenListings(listing.Id)
                    listing.PriceChanged = True
                    priceChangedListings.Add(listing)
                    SeenListings(listing.Id) = listing.Price
                End If
            Next

            SaveSeenListings()

            If skippedByPrice > 0 Then
                AddLog($"Skipped {skippedByPrice} under {config.MinPrice}€")
            End If

            If newListings.Count > 0 Then
                AddLog($"★ NEW: {newListings.Count} ★")
                For Each listing In newListings
                    If listing.Price > 0 Then
                        AddLog($"  → {listing.Title} - {listing.Price:N0}€")
                    Else
                        AddLog($"  → {listing.Title}")
                    End If
                Next
            End If
            
            If priceChangedListings.Count > 0 Then
                AddLog($"💰 PRICE CHANGED: {priceChangedListings.Count} 💰")
                For Each listing In priceChangedListings
                    Dim direction As String = If(listing.Price < listing.OldPrice, "↓", "↑")
                    AddLog($"  → {listing.Title}: {listing.OldPrice:N0}€ → {listing.Price:N0}€ {direction}")
                Next
            End If

            If newListings.Count > 0 OrElse priceChangedListings.Count > 0 Then
                AddLog("Sending email...")
                SendEmailNotification(newListings, priceChangedListings, config)
            Else
                AddLog("No changes.")
            End If

        Catch ex As Exception
            AddLog($"ERROR: {ex.Message}")
        End Try
        
        AdvanceToNextSearch()
    End Sub
    
    Private Sub AdvanceToNextSearch()
        CurrentSearchIndex += 1
        
        If CurrentSearchIndex >= SearchConfigs.Count Then
            ' Cycle complete - stop search timer, start cycle timer
            TimerScrape.Stop()
            IsRunningCycle = False
            
            Dim nextCycle As DateTime = DateTime.Now.AddMinutes(CDbl(NumericIntervalMinutes.Value))
            AddLog("───────────────────────────────────────")
            AddLog($"Cycle complete. Next cycle at: {nextCycle:HH:mm:ss}")
            AddLog("═══════════════════════════════════════")
            
            TimerCycle.Start()
        Else
            ' More searches in this cycle - start delay timer
            AddLog($"Next search in {NumericIntervalSeconds.Value} sec...")
            TimerScrape.Start()
        End If
    End Sub

    Private Function DownloadPage(url As String) As String
        Try
            Using client As New WebClient()
                client.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36")
                client.Headers.Add("Accept-Language", "sk-SK,sk;q=0.9")
                client.Encoding = System.Text.Encoding.UTF8
                Return client.DownloadString(url)
            End Using
        Catch ex As Exception
            AddLog($"Download error: {ex.Message}")
            Return String.Empty
        End Try
    End Function

    Private Function ParseListings(html As String, categorySubdomain As String) As List(Of Listing)
        Dim listings As New List(Of Listing)
        Dim seenIds As New HashSet(Of String)

        Try
            Dim pattern As String = "href=""(/inzerat/(\d+)/([^""]+)\.php)"""
            Dim matches As MatchCollection = Regex.Matches(html, pattern, RegexOptions.IgnoreCase)

            For Each match As Match In matches
                Dim id As String = match.Groups(2).Value
                If seenIds.Contains(id) Then Continue For
                seenIds.Add(id)

                Dim listing As New Listing()
                listing.Id = id
                listing.Url = $"https://{categorySubdomain}.bazos.sk" & match.Groups(1).Value
                listing.Title = match.Groups(3).Value.Replace("-", " ")
                listing.Price = 0
                listings.Add(listing)
            Next

            Dim titlePattern As String = "/inzerat/(\d+)/[^""]+\.php[^>]*>.*?<h2[^>]*>([^<]+)</h2>"
            Dim titleMatches As MatchCollection = Regex.Matches(html, titlePattern, RegexOptions.IgnoreCase Or RegexOptions.Singleline)

            For Each match As Match In titleMatches
                Dim id As String = match.Groups(1).Value
                Dim title As String = WebUtility.HtmlDecode(match.Groups(2).Value.Trim())
                For Each listing In listings
                    If listing.Id = id AndAlso Not String.IsNullOrEmpty(title) Then
                        listing.Title = title
                        Exit For
                    End If
                Next
            Next

            Dim pricePattern As String = "inzerat/(\d+)/[^""]+\.php.*?(\d[\d\s]*)\s*€"
            Dim priceMatches As MatchCollection = Regex.Matches(html, pricePattern, RegexOptions.IgnoreCase Or RegexOptions.Singleline)

            For Each match As Match In priceMatches
                Dim id As String = match.Groups(1).Value
                Dim priceStr As String = match.Groups(2).Value.Replace(" ", "").Trim()
                Dim price As Decimal = 0
                Decimal.TryParse(priceStr, price)
                For Each listing In listings
                    If listing.Id = id Then
                        listing.Price = price
                        Exit For
                    End If
                Next
            Next

        Catch ex As Exception
            AddLog($"Parsing error: {ex.Message}")
        End Try

        Return listings
    End Function

    Private Sub SendEmailNotification(newListings As List(Of Listing), priceChangedListings As List(Of Listing), config As SearchConfig)
        Try
            Dim smtpClient As New SmtpClient()
            smtpClient.Host = TextBoxSmtpServer.Text.Trim()
            smtpClient.Port = CInt(NumericSmtpPort.Value)
            smtpClient.Credentials = New NetworkCredential(TextBoxSmtpUsername.Text.Trim(), TextBoxSmtpPassword.Text)
            smtpClient.EnableSsl = CheckBoxUseSsl.Checked
            smtpClient.Timeout = 20000

            If NumericSmtpPort.Value = 465 Then
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 Or SecurityProtocolType.Tls11 Or SecurityProtocolType.Tls
            End If

            Dim fromAddress As String = TextBoxSmtpUsername.Text.Trim()
            If Not fromAddress.Contains("@") Then fromAddress = TextBoxEmailTo.Text.Trim()

            Dim mailMessage As New MailMessage()
            mailMessage.From = New MailAddress(fromAddress, "BAZscrap")
            mailMessage.To.Add(TextBoxEmailTo.Text.Trim())
            
            Dim subjectParts As New List(Of String)
            If newListings.Count > 0 Then subjectParts.Add($"{newListings.Count} new")
            If priceChangedListings.Count > 0 Then subjectParts.Add($"{priceChangedListings.Count} price changed")
            
            mailMessage.Subject = $"[BAZscrap] {String.Join(", ", subjectParts)} - '{config.SearchPhrase}'"
            mailMessage.SubjectEncoding = System.Text.Encoding.UTF8
            mailMessage.BodyEncoding = System.Text.Encoding.UTF8
            mailMessage.IsBodyHtml = True

            Dim body As New System.Text.StringBuilder()
            body.AppendLine("<html><body>")
            body.AppendLine($"<h2>BAZscrap Notification</h2>")
            body.AppendLine($"<p><strong>Search:</strong> {config.SearchPhrase}</p>")
            body.AppendLine($"<p><strong>Category:</strong> {config.CategoryName}</p>")
            body.AppendLine($"<p><strong>Time:</strong> {DateTime.Now:yyyy-MM-dd HH:mm:ss}</p>")
            
            If newListings.Count > 0 Then
                body.AppendLine("<hr/><h3>🆕 New Listings</h3><ul>")
                For Each listing In newListings
                    body.AppendLine($"<li><strong><a href=""{listing.Url}"">{listing.Title}</a></strong>")
                    If listing.Price > 0 Then body.AppendLine($" - <b>{listing.Price:N0}€</b>")
                    body.AppendLine("</li>")
                Next
                body.AppendLine("</ul>")
            End If
            
            If priceChangedListings.Count > 0 Then
                body.AppendLine("<hr/><h3>💰 Price Changes</h3><ul>")
                For Each listing In priceChangedListings
                    Dim direction As String = If(listing.Price < listing.OldPrice, "↓ DROPPED", "↑ increased")
                    Dim color As String = If(listing.Price < listing.OldPrice, "green", "red")
                    body.AppendLine($"<li><strong><a href=""{listing.Url}"">{listing.Title}</a></strong><br/>")
                    body.AppendLine($"<span style=""color:{color}""><b>{listing.OldPrice:N0}€ → {listing.Price:N0}€</b> {direction}</span></li>")
                Next
                body.AppendLine("</ul>")
            End If

            body.AppendLine("<hr/><p><em>Sent by BAZscrap</em></p></body></html>")
            mailMessage.Body = body.ToString()
            smtpClient.Send(mailMessage)
            AddLog("Email sent successfully!")

        Catch ex As Exception
            AddLog($"Email error: {ex.Message}")
        End Try
    End Sub

    Private Sub ButtonSaveSettings_Click(sender As Object, e As EventArgs) Handles ButtonSaveSettings.Click
        Try
            Using writer As New StreamWriter(SettingsFile, False, System.Text.Encoding.UTF8)
                writer.WriteLine(TextBoxSmtpServer.Text)
                writer.WriteLine(NumericSmtpPort.Value)
                writer.WriteLine(TextBoxSmtpUsername.Text)
                writer.WriteLine(TextBoxSmtpPassword.Text)
                writer.WriteLine(TextBoxEmailTo.Text)
                writer.WriteLine(CheckBoxUseSsl.Checked)
                writer.WriteLine(NumericIntervalMinutes.Value)
                writer.WriteLine(NumericIntervalSeconds.Value)
            End Using
            AddLog("Settings saved")
            MessageBox.Show("Settings saved!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            AddLog($"Error saving: {ex.Message}")
        End Try
    End Sub

    Private Sub ButtonLoadSettings_Click(sender As Object, e As EventArgs) Handles ButtonLoadSettings.Click
        LoadSettings()
    End Sub

    Private Sub LoadSettings()
        Try
            If Not File.Exists(SettingsFile) Then Return
            Dim lines() As String = File.ReadAllLines(SettingsFile, System.Text.Encoding.UTF8)
            If lines.Length >= 7 Then
                TextBoxSmtpServer.Text = lines(0)
                NumericSmtpPort.Value = CDec(lines(1))
                TextBoxSmtpUsername.Text = lines(2)
                TextBoxSmtpPassword.Text = lines(3)
                TextBoxEmailTo.Text = lines(4)
                CheckBoxUseSsl.Checked = Boolean.Parse(lines(5))
                NumericIntervalMinutes.Value = CDec(lines(6))
                If lines.Length >= 8 Then
                    NumericIntervalSeconds.Value = CDec(lines(7))
                End If
                AddLog("Settings loaded")
            End If
        Catch ex As Exception
            AddLog($"Error loading settings: {ex.Message}")
        End Try
    End Sub
    
    Private Sub SaveSearchConfigs()
        Try
            Using writer As New StreamWriter(SearchesFile, False, System.Text.Encoding.UTF8)
                For Each config In SearchConfigs
                    writer.WriteLine($"{config.SearchPhrase}|{config.CategoryName}|{config.CategorySubdomain}|{config.MinPrice}")
                Next
            End Using
        Catch ex As Exception
            AddLog($"Error saving searches: {ex.Message}")
        End Try
    End Sub
    
    Private Sub LoadSearchConfigs()
        Try
            SearchConfigs.Clear()
            If Not File.Exists(SearchesFile) Then Return
            Dim lines() As String = File.ReadAllLines(SearchesFile, System.Text.Encoding.UTF8)
            For Each line In lines
                If String.IsNullOrWhiteSpace(line) Then Continue For
                Dim parts() As String = line.Split("|"c)
                If parts.Length >= 4 Then
                    Dim config As New SearchConfig()
                    config.SearchPhrase = parts(0)
                    config.CategoryName = parts(1)
                    config.CategorySubdomain = parts(2)
                    config.MinPrice = CDec(parts(3))
                    SearchConfigs.Add(config)
                End If
            Next
            AddLog($"Loaded {SearchConfigs.Count} search configurations")
        Catch ex As Exception
            AddLog($"Error loading searches: {ex.Message}")
        End Try
    End Sub

    Private Sub LoadSeenListings()
        Try
            SeenListings.Clear()
            If Not File.Exists(SeenListingsFile) Then Return
            Dim lines() As String = File.ReadAllLines(SeenListingsFile)
            For Each line In lines
                If String.IsNullOrWhiteSpace(line) Then Continue For
                Dim parts() As String = line.Split("|"c)
                If parts.Length >= 2 Then
                    Dim price As Decimal = 0
                    Decimal.TryParse(parts(1), price)
                    SeenListings(parts(0).Trim()) = price
                ElseIf parts.Length = 1 Then
                    SeenListings(parts(0).Trim()) = 0
                End If
            Next
            AddLog($"Loaded {SeenListings.Count} seen listings")
        Catch ex As Exception
            AddLog($"Error loading seen listings: {ex.Message}")
        End Try
    End Sub

    Private Sub SaveSeenListings()
        Try
            Using writer As New StreamWriter(SeenListingsFile, False)
                For Each kvp In SeenListings
                    writer.WriteLine($"{kvp.Key}|{kvp.Value}")
                Next
            End Using
        Catch ex As Exception
            AddLog($"Error saving: {ex.Message}")
        End Try
    End Sub

    Private Sub AddLog(message As String)
        Dim timestamp As String = DateTime.Now.ToString("HH:mm:ss")
        ListBoxLog.Items.Insert(0, $"[{timestamp}] {message}")
        If ListBoxLog.Items.Count > 500 Then
            ListBoxLog.Items.RemoveAt(ListBoxLog.Items.Count - 1)
        End If
    End Sub

    Private Sub ButtonTestEmail_Click(sender As Object, e As EventArgs) Handles ButtonTestEmail.Click
        Try
            AddLog("Testing email...")
            If String.IsNullOrWhiteSpace(TextBoxSmtpServer.Text) OrElse
               String.IsNullOrWhiteSpace(TextBoxSmtpUsername.Text) OrElse
               String.IsNullOrWhiteSpace(TextBoxSmtpPassword.Text) OrElse
               String.IsNullOrWhiteSpace(TextBoxEmailTo.Text) Then
                MessageBox.Show("Fill in all email settings.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            ButtonTestEmail.Enabled = False
            ButtonTestEmail.Text = "..."
            Application.DoEvents()

            Dim smtpClient As New SmtpClient()
            smtpClient.Host = TextBoxSmtpServer.Text.Trim()
            smtpClient.Port = CInt(NumericSmtpPort.Value)
            smtpClient.Credentials = New NetworkCredential(TextBoxSmtpUsername.Text.Trim(), TextBoxSmtpPassword.Text)
            smtpClient.Timeout = 20000
            smtpClient.EnableSsl = CheckBoxUseSsl.Checked

            Dim fromAddress As String = TextBoxSmtpUsername.Text.Trim()
            If Not fromAddress.Contains("@") Then fromAddress = TextBoxEmailTo.Text.Trim()

            Dim mailMessage As New MailMessage()
            mailMessage.From = New MailAddress(fromAddress, "BAZscrap")
            mailMessage.To.Add(TextBoxEmailTo.Text.Trim())
            mailMessage.Subject = "[BAZscrap] Test Email"
            mailMessage.Body = "Test email from BAZscrap. Email settings are correct!"

            smtpClient.Send(mailMessage)
            AddLog("Test email sent!")
            MessageBox.Show("Test email sent!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

        Catch ex As Exception
            AddLog($"Test failed: {ex.Message}")
            MessageBox.Show($"Failed: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            ButtonTestEmail.Enabled = True
            ButtonTestEmail.Text = "Test"
        End Try
    End Sub

    Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        TimerScrape.Stop()
        SaveSeenListings()
        If NotifyIcon1 IsNot Nothing Then
            NotifyIcon1.Visible = False
            NotifyIcon1.Dispose()
        End If
    End Sub

    Private Sub ButtonClearHistory_Click(sender As Object, e As EventArgs) Handles ButtonClearHistory.Click
        Dim count As Integer = SeenListings.Count
        If count = 0 Then
            MessageBox.Show("History is empty.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If
        
        If MessageBox.Show($"Clear {count} listings?", "Step 1/2", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) <> DialogResult.Yes Then Return
        If MessageBox.Show("FINAL: Cannot be undone!", "Step 2/2", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) = DialogResult.Yes Then
            SeenListings.Clear()
            SaveSeenListings()
            AddLog($"History cleared! {count} listings removed.")
            MessageBox.Show("Done!", "Cleared", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

End Class

Public Class SearchConfig
    Public Property SearchPhrase As String
    Public Property CategoryName As String
    Public Property CategorySubdomain As String
    Public Property MinPrice As Decimal
End Class

Public Class Listing
    Public Property Id As String
    Public Property Title As String
    Public Property Url As String
    Public Property Price As Decimal
    Public Property OldPrice As Decimal
    Public Property PriceChanged As Boolean
End Class
