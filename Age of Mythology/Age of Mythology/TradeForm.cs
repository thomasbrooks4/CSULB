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
    public partial class TradeForm : Form
    {
        int playerTotal;
        int bankTotal;
        NumericUpDown[] playerNumbers;
        NumericUpDown[] bankNumbers;
        Player player;
        int tradeFee;

        public TradeForm(ref Player p)
        {
            tradeFee = 2;
            InitializeComponent();
            playerTotal = 0;
            bankTotal = 0;
            player = p;

            playerNumbers = new NumericUpDown[] { numericUpDown1, numericUpDown2, numericUpDown3, numericUpDown4 };
            bankNumbers = new NumericUpDown[] { numericUpDown5, numericUpDown6, numericUpDown7, numericUpDown8 };

            for (int i = 0; i < 4; i++)
            {
                playerNumbers[i].Minimum = 0;
                playerNumbers[i].Maximum = player.resourceCubes[i];
                bankNumbers[i].Minimum = 0;
            }
        }

        public TradeForm(ref Player p, bool godModeActivated)
        {
            tradeFee = -4;
            InitializeComponent();
            playerTotal = 0;
            bankTotal = 0;
            player = p;

            playerNumbers = new NumericUpDown[] { numericUpDown1, numericUpDown2, numericUpDown3, numericUpDown4 };
            bankNumbers = new NumericUpDown[] { numericUpDown5, numericUpDown6, numericUpDown7, numericUpDown8 };

            for (int i = 0; i < 4; i++)
            {
                playerNumbers[i].Minimum = 0;
                playerNumbers[i].Maximum = player.resourceCubes[i];
                bankNumbers[i].Minimum = 0;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (playerTotal == bankTotal + tradeFee)
            {
                for (int i = 0; i < 4; i++)
                {
                    player.resourceCubes[i] += (int)bankNumbers[i].Value;
                    player.resourceCubes[i] -= (int)playerNumbers[i].Value;
                }

                MessageBox.Show("Trade successful!");
                this.Close();
            }
            else if (playerTotal == 0)
            {
                MessageBox.Show("Trade Cancelled");
                this.Close();
            }
            else
            {
                MessageBox.Show("Please make sure your total is 2 more of any resource type than the bank to cover the trade fee.\n or if you are god trading, the bank total needs to be +4 of your total");
            }
        }


        //PLAYER NUMERIC UP DOWNS
        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            playerUpdate();
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            playerUpdate();
        }

        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {
            playerUpdate();
        }

        private void numericUpDown4_ValueChanged(object sender, EventArgs e)
        {
            playerUpdate();
        }


        //BANK NUMERIC UP DOWNS
        private void numericUpDown5_ValueChanged(object sender, EventArgs e)
        {
            bankUpdate();
        }

        private void numericUpDown7_ValueChanged(object sender, EventArgs e)
        {
            bankUpdate();
        }

        private void numericUpDown8_ValueChanged(object sender, EventArgs e)
        {
            bankUpdate();
        }

        private void numericUpDown6_ValueChanged(object sender, EventArgs e)
        {
            bankUpdate();
        }

        public void playerUpdate()
        {
            playerTotal = 0;

            for (int i = 0; i < playerNumbers.Length; i++)
                playerTotal += (int)playerNumbers[i].Value;
            playerTotalLabel.Text = "Total: " + playerTotal;
        }

        public void bankUpdate()
        {
            bankTotal = 0;

            for (int i = 0; i < bankNumbers.Length; i++)
                bankTotal += (int)bankNumbers[i].Value;
            
            bankTotalLabel.Text = "Total: " + bankTotal;
        }

        private void TradeForm_Load(object sender, EventArgs e)
        {

        }

        
    }
}
