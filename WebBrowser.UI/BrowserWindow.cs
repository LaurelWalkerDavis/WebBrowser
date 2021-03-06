using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WebBrowser.UI
{
    public partial class BrowserWindow : Form
    {
        public BrowserWindow()
        {
            InitializeComponent();
            browserBar1.Dock = DockStyle.Fill;
        }

        private void exitWebBrowserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Created by Laurel Walker Davis.\nAuburn University Student\nllw0008@auburn.edu");
        }

        private void newTabToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BrowserBar newBrwBar = new BrowserBar();
            newBrwBar.Dock = DockStyle.Fill;

            TabPage newTab = new TabPage("Tab " + (tabControl1.TabPages.Count + 1));
            newTab.Controls.Add(newBrwBar);            
            tabControl1.TabPages.Add(newTab);
            tabControl1.SelectedTab = newTab;
        }

        private void closeCurrentTabMenuItem_Click(object sender, EventArgs e)
        {
            tabControl1.TabPages.Remove(tabControl1.SelectedTab);
        }
    }
}
