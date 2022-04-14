﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WebBrowser.Logic;
using System.Net;
using System.IO;


namespace WebBrowser.UI
{
    public partial class BrowserBar : UserControl
    {

        private string previousUrl;

        public BrowserBar()
        {
            InitializeComponent();
        }

        private void exitWebBrowserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Created by Laurel Walker Davis.\nAuburn University Student\nllw0008@auburn.edu");
        }

        private void goButton_Click(object sender, EventArgs e)
        {
            string url = addressBox.Text;
            DateTime date = DateTime.Now;

            webBrowser1.Navigate(url);
        }

        private void addressBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string url = addressBox.Text;
                DateTime date = DateTime.Now;

                webBrowser1.Navigate(addressBox.Text);
            }
        }

        private void refreshButton_Click(object sender, EventArgs e)
        {
            webBrowser1.Refresh();
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            webBrowser1.GoBack();
        }

        private void forwardButton_Click(object sender, EventArgs e)
        {
            webBrowser1.GoForward();
        }

        private void bookmkButton_Click(object sender, EventArgs e)
        {
            string inputURL = webBrowser1.Url.AbsoluteUri.ToString();
            //TitleScraper titleScrape = new TitleScraper(inputURL);
            //titleScrape.Scrape();
            //string title = titleScrape.Title.ToString();
            string title = webBrowser1.DocumentTitle;

            BookmarkItem bookmark = new BookmarkItem();
            bookmark.URL = inputURL;
            bookmark.Title = title;

            BookmarksManager.AddBookmarksItem(bookmark);
        }

        private void addHistory(string url, string title, DateTime date)
        {
            HistoryItem history = new HistoryItem();
            history.URL = url;

            if (string.IsNullOrWhiteSpace(title))
            {
                history.Title = "Untitled";
            }
            else
            {
                history.Title = title;
            }

            history.Date = date;

            HistoryManager.AddHistoryItem(history);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            pageLoadProgressBar.Increment(1);
        }


        public class TitleScraper
        {
            private string url;

            public TitleScraper(string url)
            {
                this.url = url;
            }

            public string Title { get; set; }

            public void Scrape()
            {
                WebRequest request = WebRequest.Create(url);
                WebResponse response = request.GetResponse();
                Stream data = response.GetResponseStream();
                StreamReader sr = new StreamReader(data);
                string html = sr.ReadToEnd();
                string regex = @"(?<=<title.*>)([\s\S]*)(?=</title>)";
                System.Text.RegularExpressions.Regex ex = new System.Text.RegularExpressions.Regex(regex, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                Title = ex.Match(html).Value.Trim();
            }

        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            statusLabel.Text = "Done";
            pageLoadProgressBar.Visible = false;
            timer1.Stop();

            string url = webBrowser1.Url.ToString();

            DateTime date = DateTime.Now;
            //string inputURL = webBrowser1.Url.AbsoluteUri.ToString();
            //TitleScraper titleScrape = new TitleScraper(inputURL);
            //titleScrape.Scrape();
            //string title = titleScrape.Title.ToString();
            string title = webBrowser1.DocumentTitle;
            if (previousUrl != url)
            {
                addHistory(url, title, date);
            }
            previousUrl = url;
            addressBox.Text = webBrowser1.Url.ToString();
        }


        private void webBrowser1_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            pageLoadProgressBar.Visible = true;
            statusLabel.Text = "Loading...";
            timer1.Start();
        }
    }
}
