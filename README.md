BAZscrap

BAZscrap is a Visual Basic Windows Forms application for monitoring listings on Bazos.sk, a Slovak classifieds site similar to Craigslist. It helps you automatically track new listings and price changes for selected search terms and categories.

Features:
- Multi-search: Monitor up to 10 search phrases at once
- Price tracking with change alerts
- Notifications for every new listing
- Customizable scraping intervals (minutes between full searches, seconds between requests)
- Email notifications with configurable SMTP settings
- System tray support to run in the background
- Persistent storage of settings, searches, and seen listings

Files

settings.txt – Stores email configuration and scraping intervals
searches.txt – Stores configured search phrases and categories
seen_listings.txt – Keeps track of seen listing IDs and prices

Installation

Open the project in Visual Studio: BAZscrap.vbproj, Make sure Windows Forms and .NET Framework dependencies are installed, Build and run the project
OR
Download precompiled exe like wild animal you are... I mean - download the precompiled .exe and run it directly.

Usage
- Enter your search phrases and select categories
- Set scraping intervals and email settings
- Start the scraper
- Receive notifications for new listings or price changes

Notes
Settings and history are stored in plain text files for simplicity but can be dangerous...

