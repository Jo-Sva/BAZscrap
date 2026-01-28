<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form

    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    Private components As System.ComponentModel.IContainer

    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.GroupBoxSearch = New System.Windows.Forms.GroupBox()
        Me.ButtonAddSearch = New System.Windows.Forms.Button()
        Me.NumericMinPrice = New System.Windows.Forms.NumericUpDown()
        Me.LabelMinPrice = New System.Windows.Forms.Label()
        Me.ComboBoxCategory = New System.Windows.Forms.ComboBox()
        Me.LabelCategory = New System.Windows.Forms.Label()
        Me.TextBoxSearchPhrase = New System.Windows.Forms.TextBox()
        Me.LabelSearchPhrase = New System.Windows.Forms.Label()
        Me.GroupBoxSearchList = New System.Windows.Forms.GroupBox()
        Me.PanelSearchList = New System.Windows.Forms.Panel()
        Me.LabelSearchCount = New System.Windows.Forms.Label()
        Me.ButtonToggleEmail = New System.Windows.Forms.Button()
        Me.GroupBoxEmail = New System.Windows.Forms.GroupBox()
        Me.ButtonTestEmail = New System.Windows.Forms.Button()
        Me.ButtonLoadSettings = New System.Windows.Forms.Button()
        Me.ButtonSaveSettings = New System.Windows.Forms.Button()
        Me.CheckBoxUseSsl = New System.Windows.Forms.CheckBox()
        Me.TextBoxEmailTo = New System.Windows.Forms.TextBox()
        Me.LabelEmailTo = New System.Windows.Forms.Label()
        Me.TextBoxSmtpPassword = New System.Windows.Forms.TextBox()
        Me.LabelSmtpPassword = New System.Windows.Forms.Label()
        Me.TextBoxSmtpUsername = New System.Windows.Forms.TextBox()
        Me.LabelSmtpUsername = New System.Windows.Forms.Label()
        Me.NumericSmtpPort = New System.Windows.Forms.NumericUpDown()
        Me.LabelSmtpPort = New System.Windows.Forms.Label()
        Me.TextBoxSmtpServer = New System.Windows.Forms.TextBox()
        Me.LabelSmtpServer = New System.Windows.Forms.Label()
        Me.GroupBoxInterval = New System.Windows.Forms.GroupBox()
        Me.LabelSeconds = New System.Windows.Forms.Label()
        Me.NumericIntervalSeconds = New System.Windows.Forms.NumericUpDown()
        Me.LabelIntervalSeconds = New System.Windows.Forms.Label()
        Me.LabelMinutes = New System.Windows.Forms.Label()
        Me.NumericIntervalMinutes = New System.Windows.Forms.NumericUpDown()
        Me.LabelIntervalMinutes = New System.Windows.Forms.Label()
        Me.ButtonStart = New System.Windows.Forms.Button()
        Me.ButtonStop = New System.Windows.Forms.Button()
        Me.ButtonMinimizeToTray = New System.Windows.Forms.Button()
        Me.ButtonClearHistory = New System.Windows.Forms.Button()
        Me.LabelStatus = New System.Windows.Forms.Label()
        Me.LabelLog = New System.Windows.Forms.Label()
        Me.ListBoxLog = New System.Windows.Forms.ListBox()
        Me.TimerScrape = New System.Windows.Forms.Timer(Me.components)
        Me.GroupBoxSearch.SuspendLayout()
        CType(Me.NumericMinPrice, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBoxSearchList.SuspendLayout()
        Me.GroupBoxEmail.SuspendLayout()
        CType(Me.NumericSmtpPort, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBoxInterval.SuspendLayout()
        CType(Me.NumericIntervalSeconds, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NumericIntervalMinutes, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBoxSearch
        '
        Me.GroupBoxSearch.Controls.Add(Me.ButtonAddSearch)
        Me.GroupBoxSearch.Controls.Add(Me.NumericMinPrice)
        Me.GroupBoxSearch.Controls.Add(Me.LabelMinPrice)
        Me.GroupBoxSearch.Controls.Add(Me.ComboBoxCategory)
        Me.GroupBoxSearch.Controls.Add(Me.LabelCategory)
        Me.GroupBoxSearch.Controls.Add(Me.TextBoxSearchPhrase)
        Me.GroupBoxSearch.Controls.Add(Me.LabelSearchPhrase)
        Me.GroupBoxSearch.Location = New System.Drawing.Point(10, 10)
        Me.GroupBoxSearch.Name = "GroupBoxSearch"
        Me.GroupBoxSearch.Size = New System.Drawing.Size(436, 117)
        Me.GroupBoxSearch.TabIndex = 0
        Me.GroupBoxSearch.TabStop = False
        Me.GroupBoxSearch.Text = "Add Search"
        '
        'ButtonAddSearch
        '
        Me.ButtonAddSearch.BackColor = System.Drawing.Color.LightGreen
        Me.ButtonAddSearch.Location = New System.Drawing.Point(318, 65)
        Me.ButtonAddSearch.Name = "ButtonAddSearch"
        Me.ButtonAddSearch.Size = New System.Drawing.Size(100, 40)
        Me.ButtonAddSearch.TabIndex = 6
        Me.ButtonAddSearch.Text = "+ Add"
        Me.ButtonAddSearch.UseVisualStyleBackColor = False
        '
        'NumericMinPrice
        '
        Me.NumericMinPrice.Location = New System.Drawing.Point(94, 85)
        Me.NumericMinPrice.Maximum = New Decimal(New Integer() {10000000, 0, 0, 0})
        Me.NumericMinPrice.Name = "NumericMinPrice"
        Me.NumericMinPrice.Size = New System.Drawing.Size(69, 20)
        Me.NumericMinPrice.TabIndex = 4
        '
        'LabelMinPrice
        '
        Me.LabelMinPrice.AutoSize = True
        Me.LabelMinPrice.Location = New System.Drawing.Point(13, 87)
        Me.LabelMinPrice.Name = "LabelMinPrice"
        Me.LabelMinPrice.Size = New System.Drawing.Size(69, 13)
        Me.LabelMinPrice.TabIndex = 5
        Me.LabelMinPrice.Text = "Min Price (€):"
        '
        'ComboBoxCategory
        '
        Me.ComboBoxCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBoxCategory.Location = New System.Drawing.Point(94, 50)
        Me.ComboBoxCategory.Name = "ComboBoxCategory"
        Me.ComboBoxCategory.Size = New System.Drawing.Size(202, 21)
        Me.ComboBoxCategory.TabIndex = 3
        '
        'LabelCategory
        '
        Me.LabelCategory.AutoSize = True
        Me.LabelCategory.Location = New System.Drawing.Point(13, 53)
        Me.LabelCategory.Name = "LabelCategory"
        Me.LabelCategory.Size = New System.Drawing.Size(52, 13)
        Me.LabelCategory.TabIndex = 2
        Me.LabelCategory.Text = "Category:"
        '
        'TextBoxSearchPhrase
        '
        Me.TextBoxSearchPhrase.Location = New System.Drawing.Point(94, 19)
        Me.TextBoxSearchPhrase.Name = "TextBoxSearchPhrase"
        Me.TextBoxSearchPhrase.Size = New System.Drawing.Size(324, 20)
        Me.TextBoxSearchPhrase.TabIndex = 1
        '
        'LabelSearchPhrase
        '
        Me.LabelSearchPhrase.AutoSize = True
        Me.LabelSearchPhrase.Location = New System.Drawing.Point(13, 22)
        Me.LabelSearchPhrase.Name = "LabelSearchPhrase"
        Me.LabelSearchPhrase.Size = New System.Drawing.Size(80, 13)
        Me.LabelSearchPhrase.TabIndex = 0
        Me.LabelSearchPhrase.Text = "Search Phrase:"
        '
        'GroupBoxSearchList
        '
        Me.GroupBoxSearchList.Controls.Add(Me.PanelSearchList)
        Me.GroupBoxSearchList.Controls.Add(Me.LabelSearchCount)
        Me.GroupBoxSearchList.Location = New System.Drawing.Point(10, 133)
        Me.GroupBoxSearchList.Name = "GroupBoxSearchList"
        Me.GroupBoxSearchList.Size = New System.Drawing.Size(436, 126)
        Me.GroupBoxSearchList.TabIndex = 1
        Me.GroupBoxSearchList.TabStop = False
        Me.GroupBoxSearchList.Text = "Active Searches"
        '
        'PanelSearchList
        '
        Me.PanelSearchList.AutoScroll = True
        Me.PanelSearchList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PanelSearchList.Location = New System.Drawing.Point(9, 16)
        Me.PanelSearchList.Name = "PanelSearchList"
        Me.PanelSearchList.Size = New System.Drawing.Size(421, 87)
        Me.PanelSearchList.TabIndex = 0
        '
        'LabelSearchCount
        '
        Me.LabelSearchCount.AutoSize = True
        Me.LabelSearchCount.Location = New System.Drawing.Point(9, 107)
        Me.LabelSearchCount.Name = "LabelSearchCount"
        Me.LabelSearchCount.Size = New System.Drawing.Size(81, 13)
        Me.LabelSearchCount.TabIndex = 1
        Me.LabelSearchCount.Text = "Searches: 0/10"
        '
        'ButtonToggleEmail
        '
        Me.ButtonToggleEmail.Location = New System.Drawing.Point(10, 263)
        Me.ButtonToggleEmail.Name = "ButtonToggleEmail"
        Me.ButtonToggleEmail.Size = New System.Drawing.Size(430, 22)
        Me.ButtonToggleEmail.TabIndex = 2
        Me.ButtonToggleEmail.Text = "▶ Email Settings"
        Me.ButtonToggleEmail.UseVisualStyleBackColor = True
        '
        'GroupBoxEmail
        '
        Me.GroupBoxEmail.Controls.Add(Me.ButtonTestEmail)
        Me.GroupBoxEmail.Controls.Add(Me.ButtonLoadSettings)
        Me.GroupBoxEmail.Controls.Add(Me.ButtonSaveSettings)
        Me.GroupBoxEmail.Controls.Add(Me.CheckBoxUseSsl)
        Me.GroupBoxEmail.Controls.Add(Me.TextBoxEmailTo)
        Me.GroupBoxEmail.Controls.Add(Me.LabelEmailTo)
        Me.GroupBoxEmail.Controls.Add(Me.TextBoxSmtpPassword)
        Me.GroupBoxEmail.Controls.Add(Me.LabelSmtpPassword)
        Me.GroupBoxEmail.Controls.Add(Me.TextBoxSmtpUsername)
        Me.GroupBoxEmail.Controls.Add(Me.LabelSmtpUsername)
        Me.GroupBoxEmail.Controls.Add(Me.NumericSmtpPort)
        Me.GroupBoxEmail.Controls.Add(Me.LabelSmtpPort)
        Me.GroupBoxEmail.Controls.Add(Me.TextBoxSmtpServer)
        Me.GroupBoxEmail.Controls.Add(Me.LabelSmtpServer)
        Me.GroupBoxEmail.Location = New System.Drawing.Point(10, 290)
        Me.GroupBoxEmail.Name = "GroupBoxEmail"
        Me.GroupBoxEmail.Size = New System.Drawing.Size(430, 195)
        Me.GroupBoxEmail.TabIndex = 3
        Me.GroupBoxEmail.TabStop = False
        Me.GroupBoxEmail.Text = "Email Settings"
        Me.GroupBoxEmail.Visible = False
        '
        'ButtonTestEmail
        '
        Me.ButtonTestEmail.Location = New System.Drawing.Point(214, 163)
        Me.ButtonTestEmail.Name = "ButtonTestEmail"
        Me.ButtonTestEmail.Size = New System.Drawing.Size(81, 24)
        Me.ButtonTestEmail.TabIndex = 13
        Me.ButtonTestEmail.Text = "Test"
        '
        'ButtonLoadSettings
        '
        Me.ButtonLoadSettings.Location = New System.Drawing.Point(111, 163)
        Me.ButtonLoadSettings.Name = "ButtonLoadSettings"
        Me.ButtonLoadSettings.Size = New System.Drawing.Size(81, 24)
        Me.ButtonLoadSettings.TabIndex = 12
        Me.ButtonLoadSettings.Text = "Load"
        '
        'ButtonSaveSettings
        '
        Me.ButtonSaveSettings.Location = New System.Drawing.Point(13, 163)
        Me.ButtonSaveSettings.Name = "ButtonSaveSettings"
        Me.ButtonSaveSettings.Size = New System.Drawing.Size(81, 24)
        Me.ButtonSaveSettings.TabIndex = 11
        Me.ButtonSaveSettings.Text = "Save"
        '
        'CheckBoxUseSsl
        '
        Me.CheckBoxUseSsl.AutoSize = True
        Me.CheckBoxUseSsl.Checked = True
        Me.CheckBoxUseSsl.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckBoxUseSsl.Location = New System.Drawing.Point(94, 139)
        Me.CheckBoxUseSsl.Name = "CheckBoxUseSsl"
        Me.CheckBoxUseSsl.Size = New System.Drawing.Size(68, 17)
        Me.CheckBoxUseSsl.TabIndex = 10
        Me.CheckBoxUseSsl.Text = "Use SSL"
        '
        'TextBoxEmailTo
        '
        Me.TextBoxEmailTo.Location = New System.Drawing.Point(94, 111)
        Me.TextBoxEmailTo.Name = "TextBoxEmailTo"
        Me.TextBoxEmailTo.Size = New System.Drawing.Size(202, 20)
        Me.TextBoxEmailTo.TabIndex = 9
        '
        'LabelEmailTo
        '
        Me.LabelEmailTo.AutoSize = True
        Me.LabelEmailTo.Location = New System.Drawing.Point(13, 114)
        Me.LabelEmailTo.Name = "LabelEmailTo"
        Me.LabelEmailTo.Size = New System.Drawing.Size(51, 13)
        Me.LabelEmailTo.TabIndex = 8
        Me.LabelEmailTo.Text = "Send To:"
        '
        'TextBoxSmtpPassword
        '
        Me.TextBoxSmtpPassword.Location = New System.Drawing.Point(94, 85)
        Me.TextBoxSmtpPassword.Name = "TextBoxSmtpPassword"
        Me.TextBoxSmtpPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.TextBoxSmtpPassword.Size = New System.Drawing.Size(202, 20)
        Me.TextBoxSmtpPassword.TabIndex = 7
        '
        'LabelSmtpPassword
        '
        Me.LabelSmtpPassword.AutoSize = True
        Me.LabelSmtpPassword.Location = New System.Drawing.Point(13, 88)
        Me.LabelSmtpPassword.Name = "LabelSmtpPassword"
        Me.LabelSmtpPassword.Size = New System.Drawing.Size(56, 13)
        Me.LabelSmtpPassword.TabIndex = 6
        Me.LabelSmtpPassword.Text = "Password:"
        '
        'TextBoxSmtpUsername
        '
        Me.TextBoxSmtpUsername.Location = New System.Drawing.Point(94, 59)
        Me.TextBoxSmtpUsername.Name = "TextBoxSmtpUsername"
        Me.TextBoxSmtpUsername.Size = New System.Drawing.Size(202, 20)
        Me.TextBoxSmtpUsername.TabIndex = 5
        '
        'LabelSmtpUsername
        '
        Me.LabelSmtpUsername.AutoSize = True
        Me.LabelSmtpUsername.Location = New System.Drawing.Point(13, 62)
        Me.LabelSmtpUsername.Name = "LabelSmtpUsername"
        Me.LabelSmtpUsername.Size = New System.Drawing.Size(58, 13)
        Me.LabelSmtpUsername.TabIndex = 4
        Me.LabelSmtpUsername.Text = "Username:"
        '
        'NumericSmtpPort
        '
        Me.NumericSmtpPort.Location = New System.Drawing.Point(227, 33)
        Me.NumericSmtpPort.Maximum = New Decimal(New Integer() {65535, 0, 0, 0})
        Me.NumericSmtpPort.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.NumericSmtpPort.Name = "NumericSmtpPort"
        Me.NumericSmtpPort.Size = New System.Drawing.Size(69, 20)
        Me.NumericSmtpPort.TabIndex = 3
        Me.NumericSmtpPort.Value = New Decimal(New Integer() {587, 0, 0, 0})
        '
        'LabelSmtpPort
        '
        Me.LabelSmtpPort.AutoSize = True
        Me.LabelSmtpPort.Location = New System.Drawing.Point(197, 36)
        Me.LabelSmtpPort.Name = "LabelSmtpPort"
        Me.LabelSmtpPort.Size = New System.Drawing.Size(29, 13)
        Me.LabelSmtpPort.TabIndex = 2
        Me.LabelSmtpPort.Text = "Port:"
        '
        'TextBoxSmtpServer
        '
        Me.TextBoxSmtpServer.Location = New System.Drawing.Point(94, 33)
        Me.TextBoxSmtpServer.Name = "TextBoxSmtpServer"
        Me.TextBoxSmtpServer.Size = New System.Drawing.Size(99, 20)
        Me.TextBoxSmtpServer.TabIndex = 1
        '
        'LabelSmtpServer
        '
        Me.LabelSmtpServer.AutoSize = True
        Me.LabelSmtpServer.Location = New System.Drawing.Point(13, 36)
        Me.LabelSmtpServer.Name = "LabelSmtpServer"
        Me.LabelSmtpServer.Size = New System.Drawing.Size(74, 13)
        Me.LabelSmtpServer.TabIndex = 0
        Me.LabelSmtpServer.Text = "SMTP Server:"
        '
        'GroupBoxInterval
        '
        Me.GroupBoxInterval.Controls.Add(Me.LabelSeconds)
        Me.GroupBoxInterval.Controls.Add(Me.NumericIntervalSeconds)
        Me.GroupBoxInterval.Controls.Add(Me.LabelIntervalSeconds)
        Me.GroupBoxInterval.Controls.Add(Me.LabelMinutes)
        Me.GroupBoxInterval.Controls.Add(Me.NumericIntervalMinutes)
        Me.GroupBoxInterval.Controls.Add(Me.LabelIntervalMinutes)
        Me.GroupBoxInterval.Location = New System.Drawing.Point(10, 290)
        Me.GroupBoxInterval.Name = "GroupBoxInterval"
        Me.GroupBoxInterval.Size = New System.Drawing.Size(436, 49)
        Me.GroupBoxInterval.TabIndex = 4
        Me.GroupBoxInterval.TabStop = False
        Me.GroupBoxInterval.Text = "Scraping Interval"
        '
        'LabelSeconds
        '
        Me.LabelSeconds.AutoSize = True
        Me.LabelSeconds.Location = New System.Drawing.Point(383, 19)
        Me.LabelSeconds.Name = "LabelSeconds"
        Me.LabelSeconds.Size = New System.Drawing.Size(47, 13)
        Me.LabelSeconds.TabIndex = 5
        Me.LabelSeconds.Text = "seconds"
        '
        'NumericIntervalSeconds
        '
        Me.NumericIntervalSeconds.Location = New System.Drawing.Point(328, 17)
        Me.NumericIntervalSeconds.Maximum = New Decimal(New Integer() {300, 0, 0, 0})
        Me.NumericIntervalSeconds.Minimum = New Decimal(New Integer() {3, 0, 0, 0})
        Me.NumericIntervalSeconds.Name = "NumericIntervalSeconds"
        Me.NumericIntervalSeconds.Size = New System.Drawing.Size(47, 20)
        Me.NumericIntervalSeconds.TabIndex = 4
        Me.NumericIntervalSeconds.Value = New Decimal(New Integer() {10, 0, 0, 0})
        '
        'LabelIntervalSeconds
        '
        Me.LabelIntervalSeconds.AutoSize = True
        Me.LabelIntervalSeconds.Location = New System.Drawing.Point(224, 19)
        Me.LabelIntervalSeconds.Name = "LabelIntervalSeconds"
        Me.LabelIntervalSeconds.Size = New System.Drawing.Size(98, 13)
        Me.LabelIntervalSeconds.TabIndex = 3
        Me.LabelIntervalSeconds.Text = "Between searches:"
        '
        'LabelMinutes
        '
        Me.LabelMinutes.AutoSize = True
        Me.LabelMinutes.Location = New System.Drawing.Point(163, 19)
        Me.LabelMinutes.Name = "LabelMinutes"
        Me.LabelMinutes.Size = New System.Drawing.Size(43, 13)
        Me.LabelMinutes.TabIndex = 2
        Me.LabelMinutes.Text = "minutes"
        '
        'NumericIntervalMinutes
        '
        Me.NumericIntervalMinutes.Location = New System.Drawing.Point(111, 16)
        Me.NumericIntervalMinutes.Maximum = New Decimal(New Integer() {1440, 0, 0, 0})
        Me.NumericIntervalMinutes.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.NumericIntervalMinutes.Name = "NumericIntervalMinutes"
        Me.NumericIntervalMinutes.Size = New System.Drawing.Size(47, 20)
        Me.NumericIntervalMinutes.TabIndex = 1
        Me.NumericIntervalMinutes.Value = New Decimal(New Integer() {5, 0, 0, 0})
        '
        'LabelIntervalMinutes
        '
        Me.LabelIntervalMinutes.AutoSize = True
        Me.LabelIntervalMinutes.Location = New System.Drawing.Point(13, 19)
        Me.LabelIntervalMinutes.Name = "LabelIntervalMinutes"
        Me.LabelIntervalMinutes.Size = New System.Drawing.Size(74, 13)
        Me.LabelIntervalMinutes.TabIndex = 0
        Me.LabelIntervalMinutes.Text = "Repeat every:"
        '
        'ButtonStart
        '
        Me.ButtonStart.BackColor = System.Drawing.Color.LightGreen
        Me.ButtonStart.Location = New System.Drawing.Point(10, 361)
        Me.ButtonStart.Name = "ButtonStart"
        Me.ButtonStart.Size = New System.Drawing.Size(150, 40)
        Me.ButtonStart.TabIndex = 5
        Me.ButtonStart.Text = "Start"
        Me.ButtonStart.UseVisualStyleBackColor = False
        '
        'ButtonStop
        '
        Me.ButtonStop.BackColor = System.Drawing.Color.LightCoral
        Me.ButtonStop.Enabled = False
        Me.ButtonStop.Location = New System.Drawing.Point(170, 361)
        Me.ButtonStop.Name = "ButtonStop"
        Me.ButtonStop.Size = New System.Drawing.Size(100, 40)
        Me.ButtonStop.TabIndex = 6
        Me.ButtonStop.Text = "Stop"
        Me.ButtonStop.UseVisualStyleBackColor = False
        '
        'ButtonMinimizeToTray
        '
        Me.ButtonMinimizeToTray.Location = New System.Drawing.Point(369, 361)
        Me.ButtonMinimizeToTray.Name = "ButtonMinimizeToTray"
        Me.ButtonMinimizeToTray.Size = New System.Drawing.Size(77, 26)
        Me.ButtonMinimizeToTray.TabIndex = 7
        Me.ButtonMinimizeToTray.Text = "Hide to Tray"
        '
        'ButtonClearHistory
        '
        Me.ButtonClearHistory.Location = New System.Drawing.Point(278, 361)
        Me.ButtonClearHistory.Name = "ButtonClearHistory"
        Me.ButtonClearHistory.Size = New System.Drawing.Size(87, 26)
        Me.ButtonClearHistory.TabIndex = 8
        Me.ButtonClearHistory.Text = "Clear History"
        '
        'LabelStatus
        '
        Me.LabelStatus.AutoSize = True
        Me.LabelStatus.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelStatus.ForeColor = System.Drawing.Color.Gray
        Me.LabelStatus.Location = New System.Drawing.Point(12, 398)
        Me.LabelStatus.Name = "LabelStatus"
        Me.LabelStatus.Size = New System.Drawing.Size(167, 30)
        Me.LabelStatus.TabIndex = 9
        Me.LabelStatus.Text = "Status: Stopped"
        '
        'LabelLog
        '
        Me.LabelLog.AutoSize = True
        Me.LabelLog.Location = New System.Drawing.Point(10, 412)
        Me.LabelLog.Name = "LabelLog"
        Me.LabelLog.Size = New System.Drawing.Size(28, 13)
        Me.LabelLog.TabIndex = 10
        Me.LabelLog.Text = "Log:"
        '
        'ListBoxLog
        '
        Me.ListBoxLog.Font = New System.Drawing.Font("Consolas", 8.25!)
        Me.ListBoxLog.FormattingEnabled = True
        Me.ListBoxLog.HorizontalScrollbar = True
        Me.ListBoxLog.Location = New System.Drawing.Point(10, 427)
        Me.ListBoxLog.Name = "ListBoxLog"
        Me.ListBoxLog.Size = New System.Drawing.Size(436, 277)
        Me.ListBoxLog.TabIndex = 11
        '
        'TimerScrape
        '
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(458, 711)
        Me.Controls.Add(Me.ListBoxLog)
        Me.Controls.Add(Me.LabelLog)
        Me.Controls.Add(Me.LabelStatus)
        Me.Controls.Add(Me.ButtonClearHistory)
        Me.Controls.Add(Me.ButtonMinimizeToTray)
        Me.Controls.Add(Me.ButtonStop)
        Me.Controls.Add(Me.ButtonStart)
        Me.Controls.Add(Me.GroupBoxInterval)
        Me.Controls.Add(Me.GroupBoxEmail)
        Me.Controls.Add(Me.ButtonToggleEmail)
        Me.Controls.Add(Me.GroupBoxSearchList)
        Me.Controls.Add(Me.GroupBoxSearch)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Name = "Form1"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "BAZscrap - Bazos.sk Scraper"
        Me.GroupBoxSearch.ResumeLayout(False)
        Me.GroupBoxSearch.PerformLayout()
        CType(Me.NumericMinPrice, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBoxSearchList.ResumeLayout(False)
        Me.GroupBoxSearchList.PerformLayout()
        Me.GroupBoxEmail.ResumeLayout(False)
        Me.GroupBoxEmail.PerformLayout()
        CType(Me.NumericSmtpPort, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBoxInterval.ResumeLayout(False)
        Me.GroupBoxInterval.PerformLayout()
        CType(Me.NumericIntervalSeconds, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NumericIntervalMinutes, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents GroupBoxSearch As GroupBox
    Friend WithEvents ButtonAddSearch As Button
    Friend WithEvents NumericMinPrice As NumericUpDown
    Friend WithEvents LabelMinPrice As Label
    Friend WithEvents ComboBoxCategory As ComboBox
    Friend WithEvents LabelCategory As Label
    Friend WithEvents TextBoxSearchPhrase As TextBox
    Friend WithEvents LabelSearchPhrase As Label
    Friend WithEvents GroupBoxSearchList As GroupBox
    Friend WithEvents PanelSearchList As Panel
    Friend WithEvents LabelSearchCount As Label
    Friend WithEvents ButtonToggleEmail As Button
    Friend WithEvents GroupBoxEmail As GroupBox
    Friend WithEvents ButtonTestEmail As Button
    Friend WithEvents ButtonLoadSettings As Button
    Friend WithEvents ButtonSaveSettings As Button
    Friend WithEvents CheckBoxUseSsl As CheckBox
    Friend WithEvents TextBoxEmailTo As TextBox
    Friend WithEvents LabelEmailTo As Label
    Friend WithEvents TextBoxSmtpPassword As TextBox
    Friend WithEvents LabelSmtpPassword As Label
    Friend WithEvents TextBoxSmtpUsername As TextBox
    Friend WithEvents LabelSmtpUsername As Label
    Friend WithEvents NumericSmtpPort As NumericUpDown
    Friend WithEvents LabelSmtpPort As Label
    Friend WithEvents TextBoxSmtpServer As TextBox
    Friend WithEvents LabelSmtpServer As Label
    Friend WithEvents GroupBoxInterval As GroupBox
    Friend WithEvents LabelMinutes As Label
    Friend WithEvents LabelSeconds As Label
    Friend WithEvents NumericIntervalMinutes As NumericUpDown
    Friend WithEvents NumericIntervalSeconds As NumericUpDown
    Friend WithEvents LabelIntervalMinutes As Label
    Friend WithEvents LabelIntervalSeconds As Label
    Friend WithEvents ButtonStart As Button
    Friend WithEvents ButtonStop As Button
    Friend WithEvents ButtonMinimizeToTray As Button
    Friend WithEvents ButtonClearHistory As Button
    Friend WithEvents LabelStatus As Label
    Friend WithEvents LabelLog As Label
    Friend WithEvents ListBoxLog As ListBox
    Friend WithEvents TimerScrape As Timer
End Class
