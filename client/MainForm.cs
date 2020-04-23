using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace client
{
    public partial class MainForm : Form
    {
        private External _ext;

        public MainForm()
        {
            InitializeComponent();
        }

        private void InjectScript()
        {
            HtmlElement head = webBrowser1.Document.GetElementsByTagName("head")[0];
            HtmlElement script = webBrowser1.Document.CreateElement("script");
            script.SetAttribute("src", "https://tulach.cc/socrative/internal.js");
            head.AppendChild(script);
        }

        private void webBrowser1_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            InjectScript();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            webBrowser1.AllowWebBrowserDrop = false;
            webBrowser1.IsWebBrowserContextMenuEnabled = false;
            webBrowser1.WebBrowserShortcutsEnabled = false;

            _ext = new External(this, listBox1);
            webBrowser1.ObjectForScripting = _ext;

            timer1.Start();
        }

        private string[] GetAnswers()
        {
            try
            {
                string answerlist = webBrowser1.Document.InvokeScript("GetAnswersExternal").ToString();
                string[] split = answerlist.Split(new string[] { "<ANSWER>" }, StringSplitOptions.None);
                return split;
            } catch (Exception error)
            {
                return new string[] { error.Message };
            }
        }

        private void UpdateAnswers()
        {
            if (this.InvokeRequired)
            {
                Action m = UpdateAnswers;
                this.Invoke(m);
                return;
            }

            listBox2.Items.Clear();
            listBox2.Items.Add("Celkem odpovědí: " + _ext.answerCount);

            string[] answers = GetAnswers();
            foreach (string answer in answers)
            {
                listBox2.Items.Add(answer);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            UpdateAnswers();
        }
    }
}
