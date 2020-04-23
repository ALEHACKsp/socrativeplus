using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace client
{
    public partial class TextInputForm : Form
    {
        public string outputText;
        
        public TextInputForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            outputText = textBox1.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
