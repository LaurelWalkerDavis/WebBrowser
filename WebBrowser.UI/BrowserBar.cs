using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WebBrowser.UI
{
    public partial class BrowserBar : UserControl
    {
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
    }
}
