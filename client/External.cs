using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace client
{
    [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
    [System.Runtime.InteropServices.ComVisibleAttribute(true)]
    public class External
    {
        private MainForm _form;
        private ListBox _log;

        public int answerCount;

        public External(MainForm form, ListBox log)
        {
            _form = form;
            _log = log;
        }

        public void WriteLog(string text)
        {
            if (_form.InvokeRequired)
            {
                Action<string> m = WriteLog;
                _form.Invoke(m, text);
                return;
            }

            string time = $"[{DateTime.Now.ToString("HH:mm:ss")}] ";
            _log.Items.Add(time + text);
        }

        public string GetName()
        {
            TextInputForm input = new TextInputForm();
            
            DialogResult diag = input.ShowDialog();         
            if (diag == DialogResult.OK)
            {
                return input.outputText;
            } else
            {
                return "Nespecifikované";
            }
        }

        public void SetAnswerCount(int number)
        {
            answerCount = number;
        }
    }
}
