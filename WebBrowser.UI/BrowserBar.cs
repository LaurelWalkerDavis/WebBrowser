using System;
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
        public BrowserBar()
        {
            InitializeComponent();
        }

        //public static bool goodURL(string testURL)
        //{
        //    if (Uri.IsWellFormedUriString(p_strValue, UriKind.RelativeOrAbsolute))
        //    {
        //        Uri l_strUri = new Uri(p_strValue);
        //        return (l_strUri.Scheme == Uri.UriSchemeHttp || l_strUri.Scheme == Uri.UriSchemeHttps);
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //    return (!string.IsNullOrEmpty(testURL)) && (Uri.IsWellFormedUriString(testURL, UriKind.Absolute));

        //}

        public static bool CheckURLValid(string source) => Uri.TryCreate(source, UriKind.Absolute, out Uri uriResult) && uriResult.Scheme == Uri.UriSchemeHttps;

        private void exitWebBrowserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Created by Laurel Walker Davis.\nAuburn University Student\nllw0008@auburn.edu");
        }

        private void addressBox_Click(object sender, EventArgs e)
        {

        }

        private void goButton_Click(object sender, EventArgs e)
        {
            string URL = addressBox.Text;
            //tabPage1.Text = URL;
            webBrowser1.Navigate(URL);
        }

        private void addressBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string URL = addressBox.Text;
                //tabPage1.Text = URL;
                webBrowser1.Navigate(URL);      
            }
        }

        private void refreshButton_Click(object sender, EventArgs e)
        {
            webBrowser1.Refresh();
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            webBrowser1.GoBack();
            addressBox.Text = webBrowser1.Url.ToString();
        }

        private void forwardButton_Click(object sender, EventArgs e)
        {
            webBrowser1.GoForward();
            addressBox.Text = webBrowser1.Url.ToString();
        }

        private void bookmkButton_Click(object sender, EventArgs e)
        {
            
            string inputURL = webBrowser1.Url.AbsoluteUri.ToString();

            TitleScraper titleScrape = new TitleScraper(inputURL);
            titleScrape.Scrape();
            string title = titleScrape.Title.ToString();

            BookmarkItem bookmark = new BookmarkItem();
            bookmark.URL = inputURL;
            bookmark.Title = title;

            BookmarksManager.AddBookmarksItem(bookmark);
        }
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
    
}
