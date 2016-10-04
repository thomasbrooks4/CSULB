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
    public partial class ActionCardForm : Form
    {
        // action 1 = attack
        // action 2 = build
        // action 3 = explore
        // action 4 = gather
        // action 5 = next age
        // action 6 = recruit
        // action 7 = trade

        public int allowedActionCards;
        public char currentPlayerCulture;
        string[] actionCardImages;
        Button[] actionButtons;
        public List<int> actionsPicked;
        int buttonsClicked = 0;
        public bool actionFormDone = false;
        
        //debug purposes only
        public ActionCardForm(int age, char culture, string[] aCardImgs)
        {
            InitializeComponent();
            this.allowedActionCards = age + 3;
            this.currentPlayerCulture = culture;
            actionButtons = new Button[] { button1, button2, button3, button4, button5, button6, button7 };
            actionCardImages = aCardImgs;
            actionsPicked = new List<int>();

        }

        public ActionCardForm(int age, char culture, string[] aCardImgs, ref List<int> aToPerform)
        {
            InitializeComponent();
            this.allowedActionCards = age + 3;
            this.currentPlayerCulture = culture;
            actionButtons = new Button[] { button1, button2, button3, button4, button5, button6, button7 };
            actionCardImages = aCardImgs;
            actionsPicked = aToPerform;
            
        }

        private void donePicking()
        {
            if (buttonsClicked == allowedActionCards)
            {
                actionFormDone = true;
                this.Close();
            }
        }

        private void ActionCardForm_Load(object sender, EventArgs e)
        {
            if (currentPlayerCulture == 'n')
            {
                int bIndx = 0;
                for (int i = 14; i < 21; i++)
                {
                    actionButtons[bIndx].BackgroundImage = Image.FromFile(actionCardImages[i]);
                    bIndx++;
                }
            }
            else if (currentPlayerCulture == 'e')
            {
                for (int i = 0; i < 7; i++)
                {
                    actionButtons[i].BackgroundImage = Image.FromFile(actionCardImages[i]);
                }
            }
            else
            {
                int bIndx = 0;
                for (int i = 7; i < 14; i++)
                {
                    actionButtons[bIndx].BackgroundImage = Image.FromFile(actionCardImages[i]);
                    bIndx++;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            button1.Visible = false;
            actionsPicked.Add(1);
            buttonsClicked++;
            donePicking();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button2.Enabled = false;
            button2.Visible = false;
            actionsPicked.Add(2);
            buttonsClicked++;
            donePicking();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            button3.Enabled = false;
            button3.Visible = false;
            actionsPicked.Add(3);
            buttonsClicked++;
            donePicking();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            button4.Enabled = false;
            button4.Visible = false;
            actionsPicked.Add(4);
            buttonsClicked++;
            donePicking();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            button5.Enabled = false;
            button5.Visible = false;
            actionsPicked.Add(5);
            buttonsClicked++;
            donePicking();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            button6.Enabled = false;
            button6.Visible = false;
            actionsPicked.Add(6);
            buttonsClicked++;
            donePicking();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            button7.Enabled = false;
            button7.Visible = false;
            actionsPicked.Add(7);
            buttonsClicked++;
            donePicking();
        }
    }
}
