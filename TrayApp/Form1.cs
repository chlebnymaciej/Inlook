using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TrayApp
{
    public partial class Form1 : Form
    {
        public string Title { get; set; }
        public string myText { get; set; }
        public Form1()
        {
            InitializeComponent();
            myText = "myTextmyTextmyTextmyText";
            Title = "placeholder";
            showTray();
        }
        public void showTray()
        {
            notifyIcon1.Visible = true;
            notifyIcon1.BalloonTipText = myText;
            notifyIcon1.BalloonTipTitle = Title;
            notifyIcon1.Icon = SystemIcons.Application;
            notifyIcon1.ShowBalloonTip(1);
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            showTray();
        }
    }
}
