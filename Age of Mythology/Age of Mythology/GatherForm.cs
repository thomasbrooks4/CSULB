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
    public partial class GatherForm : Form
    {
        // 0 = favor
        // 1 = food
        // 2 = gold
        // 3 = wood

        int[] rCubes;
        public List<string> resourceGatherPossibilities = new List<string>();
        public List<string> terrainGatherPossibilities = new List<string>();
        List<ResourcePiece> rPieces;
        bool godMode = false;
        char playerCult;

        public GatherForm(ref int[] resources, List<ResourcePiece> rp, bool godModeActivated, char pCult)
        {
            InitializeComponent();
            rCubes = resources;
            rPieces = rp;
            godMode = true;
            playerCult = pCult;
        }

        public GatherForm(ref int[] resources, List<ResourcePiece> rp)
        {
            InitializeComponent();
            rCubes = resources;
            rPieces = rp;
        }

        private void GatherForm_Load(object sender, EventArgs e)
        {
            getPossible();
        }

        //terrain gather
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            comboBox1.Items.AddRange(terrainGatherPossibilities.Distinct().ToArray());
            
        }

        //resource gather
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            comboBox1.Items.AddRange(resourceGatherPossibilities.Distinct().ToArray());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //resource
            if (comboBox1.SelectedItem != "")
            {
                if (radioButton1.Checked) //RESOURCE GATHER
                {

                    if (godMode)
                    {
                        if (playerCult == 'g')
                        {
                            addResource("Food", 5);
                        }
                        else if (playerCult == 'n')
                        {
                            addResource("Gold", 5);
                        }
                    }

                    foreach (ResourcePiece rpiece in rPieces)
                    {
                        if (rpiece.resourceType.Equals(comboBox1.SelectedItem))
                        {
                            if (godMode)
                            {
                                if (playerCult == 'e')
                                {
                                    if (rpiece.resourceType.Equals("Food"))
                                    {
                                        addResource("Food", 2);
                                    }
                                }
                            }
                            addResource(rpiece.resourceType, rpiece.resourceAmount);
                        }
                    }
                }
                else //TERRAIN GATHER
                {
                    if (godMode)
                    {
                        if (playerCult == 'g')
                        {
                            addResource("Food", 5);
                        }
                        else if (playerCult == 'n')
                        {
                            addResource("Gold", 5);
                        }
                    }

                    foreach (ResourcePiece rpiece in rPieces)
                    {
                        if (rpiece.terrainType.Equals(comboBox1.SelectedItem))
                        {
                            if (godMode)
                            {
                                if (playerCult == 'e')
                                {
                                    if (rpiece.resourceType.Equals("Food"))
                                    {
                                        addResource("Food", 2);
                                    }
                                }
                            }
                            addResource(rpiece.resourceType, rpiece.resourceAmount);
                        }
                    }
                }
                MessageBox.Show("Resources Gathered!");
                this.Close();
            }
        }


        private void addResource(string resourceType, int value)
        {
            if (resourceType.Equals("Favor"))
            {
                rCubes[0] += value;
            }
            else if (resourceType.Equals("Food"))
            {
                rCubes[1] += value;
            }
            else if (resourceType.Equals("Gold"))
            {
                rCubes[2] += value;
            }
            else
            {
                rCubes[3] += value;
            }
        }

        public void getPossible()
        {
            foreach (ResourcePiece rpiece in rPieces)
            {
                resourceGatherPossibilities.Add(rpiece.resourceType);
                terrainGatherPossibilities.Add(rpiece.terrainType);
            }
        }

        public void aiGather()
        {
            foreach (ResourcePiece rpiece in rPieces)
            {
                addResource(rpiece.resourceType, rpiece.resourceAmount);
            }
        }
    }
}
