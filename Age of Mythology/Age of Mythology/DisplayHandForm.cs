using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Age_of_Mythology
{
    public partial class DisplayHandForm : Form
    {
        // action 1 = attack
        // action 2 = build
        // action 3 = explore
        // action 4 = gather
        // action 5 = next age
        // action 6 = recruit
        // action 7 = trade


        public List<int> actionsToPerform;
        public Player player;
        int totalMoves;
        public BuildForm bForm; 
        public char currentPlayerCulture;
        string[] actionCardImages;
        public Button[] displayFormButtons;
        public bool doneInitializing = false;
        List<CityPiece> cMasterList;
        public bool handFormDone = false;
        List<BattleUnits> battleUnitMasterList;
        List<ResourcePiece> rMasterList;
        List<ResourcePiece> resourcePieces = new List<ResourcePiece>();
        InitiationForm iForm;
        public int CPL;
        Player[] pArray;


        public DisplayHandForm(List<int> actions, ref Player p, int m, string[] aCardImgs, List<CityPiece> cMaster, List<BattleUnits> bMaster, ref List<ResourcePiece> rml, int cpl, ref Player[] players)
        {
            InitializeComponent();
            this.actionsToPerform = actions;
            this.player = p;
            this.totalMoves = 3;
            displayFormButtons = new Button[] { button1, button2, button3, button4, button5, button6, button7, button8 };
            actionCardImages = aCardImgs;
            currentPlayerCulture = p.culture;
            cMasterList = cMaster;
            battleUnitMasterList = bMaster;
            //bForm = new BuildForm(ref player, cMasterList);
            rMasterList = rml;
            CPL = cpl;
            pArray = players;
            //iForm = new InitiationForm(ref player, cpl, ref rMasterList);
        }

        #region dynamically load images onto buttons
        private void DisplayHandForm_Load(object sender, EventArgs e)
        {
            for (int i = actionsToPerform.Count() + 1; i < 8; i++)
            {
                displayFormButtons[i].Visible = false;
            }
            this.Size = new Size((160 * (actionsToPerform.Count() + 1) + 6 * (actionsToPerform.Count() + 1) + 16 ) ,296);

            if (currentPlayerCulture == 'n')
            {
                for (int i = 0; i < actionsToPerform.Count; i++)
                {
                    displayFormButtons[i + 1].Image = Image.FromFile(displayCardImages(actionsToPerform[i], 14));
                }
            }
            else if (currentPlayerCulture == 'g')
            {
                for (int i = 0; i < actionsToPerform.Count; i++)
                {
                    displayFormButtons[i + 1].Image = Image.FromFile(displayCardImages(actionsToPerform[i], 7));
                }
            }
            else //egyptian
            {
                for (int i = 0; i < actionsToPerform.Count; i++)
                {
                    displayFormButtons[i + 1 ].Image = Image.FromFile(displayCardImages(actionsToPerform[i], 0));
                }
            }

            addGodCard();
        }

        private string displayCardImages(int action, int cultureOffset)
        {
            int imgIndx = 0;

            if (action == 1)
                imgIndx = 0;//attack image
            else if (action == 2)
                imgIndx = 1;//build image
            else if (action == 3)
                imgIndx = 2;//explore
            else if (action == 4)
                imgIndx = 3;//gather
            else if (action == 5)
                imgIndx = 4;//next age
            else if (action == 6)
                imgIndx = 5;//recruit
            else
                imgIndx = 6;//trade

            return actionCardImages[imgIndx + cultureOffset];
        }
        //PASS BUTTON

        private void addGodCard()
        {
            for (int i = 0; i < actionsToPerform.Count; i++)
            {
                Random rand = new Random();
                int r = rand.Next(1, 2);// THIS IS WHERE YOU CHANGE % for god cards

                if (r <= 2)
                {
                    actionsToPerform[i] = actionsToPerform[i] * -1;
                    if (currentPlayerCulture == 'e')
                        displayFormButtons[i + 1].Image = Image.FromFile(displayCardImages(Math.Abs(actionsToPerform[i]), 20));
                    else if (currentPlayerCulture == 'g')
                        displayFormButtons[i + 1].Image = Image.FromFile(displayCardImages(Math.Abs(actionsToPerform[i]), 26));
                    else
                        displayFormButtons[i + 1].Image = Image.FromFile(displayCardImages(Math.Abs(actionsToPerform[i]), 32));
                }
            }
        }

        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            turnOver();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            performAction(actionsToPerform[0]);
            button2.Enabled = false;
            button2.Visible = false;
            turnOver();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            performAction(actionsToPerform[1]);
            button3.Enabled = false;
            button3.Visible = false;
            turnOver();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            performAction(actionsToPerform[2]);
            button4.Enabled = false;
            button4.Visible = false;
            turnOver();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            performAction(actionsToPerform[3]);
            button5.Enabled = false;
            button5.Visible = false;
            turnOver();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            performAction(actionsToPerform[4]);
            button6.Enabled = false;
            button6.Visible = false;
            turnOver();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            performAction(actionsToPerform[5]);
            button7.Enabled = false;
            button7.Visible = false;
            turnOver();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            performAction(actionsToPerform[6]);
            button8.Enabled = false;
            button8.Visible = false;
            turnOver();
        }

        //dont forget to add favor costs
        public void performAction(int indexToPerform)
        {
            if (CPL == 0)
            {
                #region ATTACK
                if (indexToPerform == 1)
                {
                    //attack hasn't been implemented yet
                }
                #endregion

                //done ?             
                #region BUILD
                else if (Math.Abs(indexToPerform) == 2)
                {
                    if (indexToPerform >= 0)
                    {
                        bForm = new BuildForm(ref player, cMasterList);
                        bForm.Show();
                    }
                    else if (player.resourceCubes[0] >= 1)
                    {
                        bForm = new BuildForm(ref player, cMasterList, true, ref pArray);
                        bForm.Show();
                        player.resourceCubes[0]--;
                    }
                }
                #endregion

                //done ?
                #region EXPLORE / INITIATION
                else if (Math.Abs(indexToPerform) == 3)
                {
                    if (indexToPerform >= 0)
                    {
                        iForm = new InitiationForm(ref player, CPL, ref rMasterList);
                        iForm.Show();
                    }
                    else if (player.resourceCubes[0] >= 1)
                    {
                        iForm = new InitiationForm(ref player, CPL, ref rMasterList, true);
                        iForm.Show();
                        player.resourceCubes[0]--;
                    };//god card
                }
                #endregion

                //done ?
                #region GATHER
                else if (Math.Abs(indexToPerform) == 4)
                {
                    if (indexToPerform >= 0)
                    {
                        GatherForm gForm = new GatherForm(ref player.resourceCubes, player.resourcePiecesList);
                        gForm.Show();
                    }
                    else if (player.resourceCubes[0] >= 1)
                    {
                        GatherForm gForm = new GatherForm(ref player.resourceCubes, player.resourcePiecesList, true, currentPlayerCulture);
                        gForm.Show();
                        player.resourceCubes[0]--;
                    }//done ?

                }
                #endregion

                //done ?
                #region NEXT AGE
                else if (Math.Abs(indexToPerform) == 5)
                {
                    if (indexToPerform >= 0)
                    {
                        NextAgeForm nAForm = new NextAgeForm(ref player, ref player.resourceCubes);
                        nAForm.Show();
                    }
                    else if (player.resourceCubes[0] >= 1)
                    {
                        if (player.culture == 'n')
                            totalMoves++;

                        NextAgeForm nAForm = new NextAgeForm(ref player, ref player.resourceCubes, true, ref pArray);
                        nAForm.Show();
                        player.resourceCubes[0]--;
                    };
                }
                #endregion

                // done ? 
                #region RECRUIT
                else if (Math.Abs(indexToPerform) == 6)
                {
                    if (indexToPerform >= 0)
                    {
                        RecruitForm rForm = new RecruitForm(ref player, battleUnitMasterList);
                        rForm.Show();
                    }
                    else if (player.resourceCubes[0] >= 1)
                    {
                        RecruitForm rForm = new RecruitForm(ref player, battleUnitMasterList, true);
                        rForm.Show();
                    };
                    player.resourceCubes[0]--;
                }
                #endregion

                //done ?
                #region TRADE
                else if (Math.Abs(indexToPerform) == 7)
                {
                    if (indexToPerform >= 0)
                    {
                        TradeForm tForm = new TradeForm(ref player);
                        tForm.Show();
                    }
                    else if (player.resourceCubes[0] >= 1)
                    {
                        TradeForm tForm = new TradeForm(ref player, true);
                        tForm.Show();
                        player.resourceCubes[0]--;
                    }; // done ?
                }
                #endregion
            }
            else
            {
                #region ATTACK
                if (indexToPerform == 1)
                {
                    //attack hasn't been implemented yet
                }
                #endregion

                //done ?             
                #region BUILD
                else if (Math.Abs(indexToPerform) == 2)
                {
                    bForm = new BuildForm(ref player, cMasterList);
                    List<String> aiBuildings = bForm.getPossibleBuildings(player.resourceCubes);
                    if (aiBuildings.Count() > 0)
                    {
                        bForm.comboBox1.Text = aiBuildings[0];
                        bForm.build();
                        bForm.Close();
                    }
                    MessageBox.Show("AI" + CPL + " completed build action.");
                }
                #endregion

                //done ?
                #region EXPLORE / INITIATION
                else if (Math.Abs(indexToPerform) == 3)
                {
                    //do nothing
                    MessageBox.Show("AI" + CPL + " completed explore action.");
                }
                #endregion

                //done ?
                #region GATHER
                else if (Math.Abs(indexToPerform) == 4)
                {
                    GatherForm gForm = new GatherForm(ref player.resourceCubes, player.resourcePiecesList);
                    gForm.getPossible();
                    if (gForm.resourceGatherPossibilities.Count() > 0)
                    {
                        gForm.comboBox1.SelectedText = gForm.resourceGatherPossibilities[0];
                    }
                    gForm.Close();
                    MessageBox.Show("AI" + CPL + " completed gather action.");
                }
                #endregion

                //done ?
                #region NEXT AGE
                else if (Math.Abs(indexToPerform) == 5)
                {
                    NextAgeForm nAForm = new NextAgeForm(ref player, ref player.resourceCubes);
                    if (player.resourceCubes[0] >= 4 && player.resourceCubes[1] >= 4 && player.resourceCubes[2] >= 4 && player.resourceCubes[3] >= 4)
                    {
                        player.age++;
                    }
                    nAForm.Close();
                    MessageBox.Show("AI" + CPL + " completed next age action.");
                }
                #endregion

                // done ? 
                #region RECRUIT
                else if (Math.Abs(indexToPerform) == 6)
                {
                    RecruitForm rForm = new RecruitForm(ref player, battleUnitMasterList);
                    for (int i = 0; i < rForm.recruitButtons.Length; i++)
                    {
                        if (rForm.recruitButtons[i].Enabled)
                        {
                            rForm.subtractCost(i);
                            break;
                        }
                    }
                    rForm.Close();
                    MessageBox.Show("AI" + CPL + " completed recruit action.");
                }
                #endregion

                //done ?
                #region TRADE
                else if (Math.Abs(indexToPerform) == 7)
                {
                    int tradeAmount = 0;

                    while (tradeAmount < 3)
                    {
                        if (player.resourceCubes[0] > 1)
                        {
                            player.resourceCubes[0]--;
                            tradeAmount++;
                        }
                        else if (player.resourceCubes[1] > 1)
                        {
                            player.resourceCubes[1]--;
                            tradeAmount++;
                        }
                        else if (player.resourceCubes[2] > 1)
                        {
                            player.resourceCubes[2]--;
                            tradeAmount++;
                        }
                        else if (player.resourceCubes[3] > 1)
                        {
                            player.resourceCubes[3]--;
                            tradeAmount++;
                        }
                        else
                            break;
                    }
                    
                    player.resourceCubes[3]++;
                    MessageBox.Show("AI" + CPL + " completed trade action.");
                }
                #endregion

                else
                    MessageBox.Show("AI" + CPL + " passed.");
            }
        }

        public void turnOver()
        {
            totalMoves--;
            if (totalMoves == 0)
            {
                handFormDone = true;
                this.Close();
                
            }
        }



       
    }
}
