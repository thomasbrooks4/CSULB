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
    public partial class VictoryCardsForm : Form
    {
        Button[] victoryCardButtons;
        string[] buttonPictures = {"C:\\AgeOfMythology\\Resources\\Victory Cards\\perm_army.png",
                                   "C:\\AgeOfMythology\\Resources\\Victory Cards\\perm_battle.png",
                                   "C:\\AgeOfMythology\\Resources\\Victory Cards\\perm_buildings.png",
                                   "C:\\AgeOfMythology\\Resources\\Victory Cards\\perm_wonder.png",
                                   "C:\\AgeOfMythology\\Resources\\Victory Cards\\cubes.png"
                                  };
        int[] cubes = new int[4];
        int total_Cubes = 30;
       
        public bool victoryCardsDone = false;

        public VictoryCardsForm()
        {
            InitializeComponent();
            
        }

        private void VictoryCardsForm_Load(object sender, EventArgs e)
        {
           victoryCardButtons = new Button[] {button1, button2, button3, button4};
           for (int i = 0; i < 4; i++)
           {
               victoryCardButtons[i].BackgroundImage = Image.FromFile(buttonPictures[i]);
               //victoryCardButtons[i].Image = Image.FromFile(buttonPictures[4]);
               victoryCardButtons[i].Text = cubes[i] + "";
           }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            cubes[0]++;
            total_Cubes--;
            button1.Text = cubes[0] + "";
            playDone();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            cubes[1]++;
            total_Cubes--;
            button2.Text = cubes[1] + "";
            playDone();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            cubes[2]++;
            total_Cubes--;
            button3.Text = cubes[2] + "";
            playDone();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            cubes[3]++;
            total_Cubes--;
            button4.Text = cubes[3] + "";
            playDone();
        }

        private void playDone()
        {
            Random r = new Random();
            cubes[r.Next(0, 3)]++;
            cubes[r.Next(0, 3)]++;
            MessageBox.Show("Remaining players are choosing...");
            MessageBox.Show("All players have chosen!");
            this.Hide();
            victoryCardsDone = true;
        }

        private void VictoryCardsForm_VisibleChanged(object sender, EventArgs e)
        {
            button1.Text = cubes[0] + "";
            button2.Text = cubes[1] + "";
            button3.Text = cubes[2] + "";
            button4.Text = cubes[3] + "";
        }
    }
}
