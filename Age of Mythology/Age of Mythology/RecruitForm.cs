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
    public partial class RecruitForm : Form
    {
        Player player;
        public Button[] recruitButtons;
        List<BattleUnits> bMasterList;
        int totalAllowed = 2;
        bool done = false;

        public RecruitForm(ref Player p, List<BattleUnits> bmaster)
        {
            InitializeComponent();
            player = p;
            recruitButtons = new Button[] { button1, button2, button3, button4, button5, button6, button7, button8, button9, button10, button12, button13 };
            bMasterList = bmaster;
            comboBox1.Visible = false;
            button14.Visible = false;
        }

        public RecruitForm(ref Player p, List<BattleUnits> bmaster, bool godModeActivated)
        {
            InitializeComponent();
            player = p;
            recruitButtons = new Button[] { button1, button2, button3, button4, button5, button6, button7, button8, button9, button10, button12, button13 };
            bMasterList = bmaster;
            if (player.culture == 'n')
                totalAllowed = 3;
            else
                totalAllowed = 4;
        }

        private void RecruitForm_Load(object sender, EventArgs e)
        {
            // Load photos to options screen
            
              if (player.culture == 'e') 
              {
                  button12.Visible = false;
                  button13.Visible = false;

                  for (int i = 0; i < 10; i++)
                  {
                      recruitButtons[i].Image = Image.FromFile(bMasterList[i].picture);
                  }
              }
              else if (player.culture == 'g') 
              {
                  for (int i = 10; i < 22; i++)
                  {
                      recruitButtons[i-10].Image = Image.FromFile(bMasterList[i].picture);
                  }
              }
              else 
              {
                  button13.Visible = false;
                  for (int i = 22; i < 33; i++)
                  {
                      recruitButtons[i - 22].Image = Image.FromFile(bMasterList[i].picture);
                  }
                  //load norse images
              }

              getPossibleUnits();
              godMode();
        }

        public void getPossibleUnits() 
        {

            if (player.culture == 'e')
            {
                for (int i = 0; i < 10; i++)
                {
                    if (player.resourceCubes[0] >= bMasterList[i].cost[0] && player.resourceCubes[1] >= bMasterList[i].cost[1] && player.resourceCubes[2] >= bMasterList[i].cost[2] && player.resourceCubes[3] >= bMasterList[i].cost[3])
                    {
                        recruitButtons[i].Enabled = true;
                    }
                    else
                    {
                        recruitButtons[i].Enabled = false;
                       //recruitButtons[i].Visible = false;
                    }
                }
            }
            else if (player.culture == 'g')
            {
                for (int i = 10; i < 22; i++)
                {
                    if (player.resourceCubes[0] >= bMasterList[i].cost[0] && player.resourceCubes[1] >= bMasterList[i].cost[1] && player.resourceCubes[2] >= bMasterList[i].cost[2] && player.resourceCubes[3] >= bMasterList[i].cost[3])
                    {
                        recruitButtons[i-10].Enabled = true;
                    }
                    else
                    {
                        recruitButtons[i-10].Enabled = false;
                        //recruitButtons[i-10].Visible = false;
                    }
                }
            }
            else
            {
                for (int i = 22; i < 33; i++)
                {
                    if (player.resourceCubes[0] >= bMasterList[i].cost[0] && player.resourceCubes[1] >= bMasterList[i].cost[1] && player.resourceCubes[2] >= bMasterList[i].cost[2] && player.resourceCubes[3] >= bMasterList[i].cost[3])
                    {
                        recruitButtons[i - 22].Enabled = true;
                    }
                    else
                    {
                        recruitButtons[i - 22].Enabled = false;
                        //recruitButtons[i-22].Visible = false;
                    }
                }
                //load norse images
            }
            done = true;

            for (int i = 0; i < recruitButtons.Length; i++)
            {
                if (recruitButtons[i].Enabled)
                    done = false;
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            subtractCost(0);
            getPossibleUnits();
            checkDone();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            subtractCost(1);
            getPossibleUnits();
            checkDone();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            subtractCost(2);
            getPossibleUnits();
            checkDone();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            subtractCost(3);
            getPossibleUnits();
            checkDone();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            subtractCost(4);
            getPossibleUnits();
            checkDone();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            subtractCost(5);
            getPossibleUnits();
            checkDone();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            subtractCost(6);
            getPossibleUnits();
            checkDone();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            subtractCost(7);
            getPossibleUnits();
            checkDone();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            subtractCost(8);
            getPossibleUnits();
            checkDone();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            subtractCost(9);
            getPossibleUnits();
            checkDone();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            subtractCost(10);
            getPossibleUnits();
            checkDone();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            subtractCost(11);
            getPossibleUnits();
            checkDone();
        }

        public void subtractCost(int button)
        {
            

            if (player.culture == 'e')
            {
                player.resourceCubes[0] -= bMasterList[button].cost[0];
                player.resourceCubes[1] -= bMasterList[button].cost[1];
                player.resourceCubes[2] -= bMasterList[button].cost[2];
                player.resourceCubes[3] -= bMasterList[button].cost[3];
                player.army.Add(new BattleUnits(bMasterList[button].type));

            }
            else if (player.culture == 'g')
            {
                player.resourceCubes[0] -= bMasterList[button + 10].cost[0];
                player.resourceCubes[1] -= bMasterList[button + 10].cost[1];
                player.resourceCubes[2] -= bMasterList[button + 10].cost[2];
                player.resourceCubes[3] -= bMasterList[button + 10].cost[3];
                player.army.Add(new BattleUnits(bMasterList[button + 10].type));
            }
            else
            {
                player.resourceCubes[0] -= bMasterList[button + 22].cost[0];
                player.resourceCubes[1] -= bMasterList[button + 22].cost[1];
                player.resourceCubes[2] -= bMasterList[button + 22].cost[2];
                player.resourceCubes[3] -= bMasterList[button + 22].cost[3];
                player.army.Add(new BattleUnits(bMasterList[button + 22].type));
            }
        }

        private void checkDone()
        {
            totalAllowed--;
            if (totalAllowed == 0 || done)
            {
                MessageBox.Show("Recruitment complete.");
                this.Close();
            }
        }

        private void godMode()
        {
            if (player.culture == 'e')
            {
                Random rand = new Random();
                int rUnit = rand.Next(0, 3);
                player.army.Add(new BattleUnits(bMasterList[rUnit].type));
            }
            else if (player.culture == 'g')
            {
                this.comboBox1.Visible = false;
                this.button14.Visible = false;
                player.army.Add(new BattleUnits(bMasterList[21].type));
                player.army.Add(new BattleUnits(bMasterList[21].type));
            }
            else if (player.culture == 'n')
            {
                comboBox1.Visible = true;
                button14.Visible = true;

                comboBox1.Items.Add("Dwarf");
                comboBox1.Items.Add("Huskarl");
                comboBox1.Items.Add("Jarl");
                comboBox1.Items.Add("Throwing Axeman");
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem != "")
            {
                player.army.Add(new BattleUnits(comboBox1.SelectedItem.ToString()));
                player.army.Add(new BattleUnits(comboBox1.SelectedItem.ToString()));
            }
            MessageBox.Show("2 extra units added");
            button14.Enabled = false;
        }

    }
}
