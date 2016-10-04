using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Age_of_Mythology
{
    public partial class NewGameForm : Form
    {
        public char returnValue = '0';
        protected string test = "test";
        public NewGameForm()
        {
            InitializeComponent();
        }

        private void NewGameForm_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
                returnValue = 'n';
            else if (radioButton2.Checked)
                returnValue = 'e';
            else
                returnValue = 'g';
            this.Hide();
            //this.WindowState = FormWindowState.Minimized;
            AgeOfMythologyForm gameForm = new AgeOfMythologyForm(returnValue, this);
            gameForm.Show();
            
        }
        public void closeEverything()
        {
            
            GC.Collect();
            if (System.Windows.Forms.Application.MessageLoop)
            {
                // WinForms app
                System.Windows.Forms.Application.Exit();
            }
            else
            {
                // Console app
                System.Environment.Exit(1);
            }
        }
    }
}
