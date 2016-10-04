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
    public partial class NextAgeForm : Form
    {

        Player player;
        int[] rCubes;
        Player[] players;
        bool godMode = false;

        public NextAgeForm(ref Player p, ref int[] resources)
        {
            InitializeComponent();

            rCubes = resources;
            player = p;
        }

        public NextAgeForm(ref Player p, ref int[] resources, bool godModeActivated, ref Player[]ps)
        {
            InitializeComponent();
            players = ps;
            rCubes = resources;
            player = p;
            godMode = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Confirm button
            switch(player.age) 
            {
                // First age
                case 1:
                    if (rCubes[0] >= 4 && rCubes[1] >= 4 && rCubes[2] >= 4 && rCubes[3] >= 4)
                    {
                        player.incrementAge();

                        rCubes[0] -= 4;
                        rCubes[1] -= 4;
                        rCubes[2] -= 4;
                        rCubes[3] -= 4;

                        if (player.culture == 'g' && godMode)
                            player.army.Add(new BattleUnits("Toxotes"));
                        else if (player.culture == 'e' && godMode)
                        {
                            for (int i = 0; i < 3; i++)
                            {
                                if (players[i] != player)
                                {
                                    for (int k = 0; k < players[i].resourceArea.tiles.Length; k++)
                                    {
                                        if (players[i].resourceArea.tiles[k].type.Equals("Food"))
                                        {
                                            players[i].resourceArea.tiles[k].isFilled = false;
                                            players[i].resourceArea.tiles[k].overlayPicture = null;

                                            for (int n = 0; n < players[i].resourcePiecesList.Count(); n++)
                                            {
                                                if (players[i].resourcePiecesList[n].resourceType.Equals("Food"))
                                                {
                                                    players[i].resourcePiecesList.RemoveAt(n);
                                                    break;
                                                }
                                            }

                                            break;
                                        }
                                    }
                                }
                                
                            }
                        }

                        MessageBox.Show("You have progressed into the next age!");
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("You do not have the required resources to progress to the next age.");
                        this.Close();
                    }
                    break;
                // Repeat for each age 5, 6, 7(can't next age at final age)
            }
        }

        private void NextAgeForm_Load(object sender, EventArgs e)
        {

        }
    }
}
