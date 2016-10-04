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
    public partial class AgeOfMythologyForm : Form
    {
        
        #region globals

        public char playerBoardChoice = '0';

        public Player player;
        public Player ai1;
        public Player ai2;
        public Player[] players;

        public bool doneInitializing = false;
        public bool buttonClicked = false;

        public List<ResourcePiece> resourcePieces = new List<ResourcePiece>();
        public List<ResourcePiece> resourceMasterList = new List<ResourcePiece>();
        public List<CityPiece> cityMasterList = new List<CityPiece>();
        public List<BattleUnits> battleUnitMasterList = new List<BattleUnits>();

        public PictureBox[] resourceAreaPictureBoxes;
        public PictureBox[] cityAreaPictureBoxes;

        public int currentPlayer = 0;
        public int lastButtonClicked = 0;
        int toggle = 0;

        public List<int> actionsToPerform = new List<int>();
        public List<int> actionsPerformed = new List<int>();

        public InitiationForm iForm;
        private NewGameForm nGForm;
        VictoryCardsForm vcForms = new VictoryCardsForm();
        
        string[] actionCards = new string[39];
        bool turnDone = false;


        #endregion

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="pBoardChoice"></param>
        /// <param name="ngf"></param>
        public AgeOfMythologyForm(char pBoardChoice, NewGameForm ngf)
        {
            InitializeComponent();
            this.playerBoardChoice = pBoardChoice;
            resourceAreaPictureBoxes = new PictureBox[] { pictureBox1, pictureBox2, pictureBox3, pictureBox4, pictureBox5, pictureBox6, pictureBox7, pictureBox8, pictureBox9, pictureBox10, pictureBox11, pictureBox12, pictureBox13, pictureBox14, pictureBox15, pictureBox16 };
            cityAreaPictureBoxes = new PictureBox[] { pictureBox17, pictureBox18, pictureBox19, pictureBox20, pictureBox21, pictureBox22, pictureBox23, pictureBox24, pictureBox25, pictureBox26, pictureBox27, pictureBox28, pictureBox29, pictureBox30, pictureBox31, pictureBox32 };
            this.nGForm = ngf;

            generateResourceMasterList();
            generateCityMasterList();
            generateBattleUnitMasterList();
            loadActionCards();

        }

        #region AgeOfMythologyFormEvents

        private void AgeOfMythologyForm_Load(object sender, EventArgs e)
        {
            this.Location = new Point(0, 0);
            player = new Player(playerBoardChoice);

            ai1 = new Player();
            ai2 = new Player();

            players = new Player[3] { player, ai1, ai2 };

            generatePieces();

            this.iForm = new InitiationForm(resourcePieces);
            iForm.Show();
            displayBoard(players[currentPlayer]);
            endTurnbutton.Enabled = false;
            startTurnButton.Enabled = false;

            //debug purposes only
            //ActionCardForm aCForm2 = new ActionCardForm(players[currentPlayer].age, players[currentPlayer].culture, actionCards);
            //aCForm2.Show();

        }

        private void AgeOfMythologyForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            GC.Collect();
            //Application.Exit();
            nGForm.closeEverything();
        }

        /// <summary>
        /// Resource Initiation form opened and automated
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AgeOfMythologyForm_Activated(object sender, EventArgs e)
        {
            int skipCount = 0;
            getPossibleMoves();
            displayResourceButtons();

            while (!doneInitializing)
            {
                Application.DoEvents();
                buttonClicked = iForm.buttonClicked;
                lastButtonClicked = iForm.lastButtonClicked;
                Thread.Sleep(100);

                if (getPossibleMoves())
                {
                    
                    currentPlayer = ++currentPlayer % 3;
                    displayBoard(players[currentPlayer]);
                    
                    skipCount++;
                }
                else
                {
                    skipCount = 0;

                    if (currentPlayer == 0)
                    {
                        
                        if (buttonClicked)
                        {

                            iForm.buttonClicked = false;
                            enableButtons();
                            moveResourcePiece();
                            displayBoard(players[currentPlayer]);
                            
                            Thread.Sleep(1000);

                            currentPlayer = ++currentPlayer % 3;
                            
                            displayBoard(players[currentPlayer]);
                            if (currentPlayer == 0)
                                label1.Text = "Player 1";
                            else if (currentPlayer == 1)
                                label1.Text = "AI 1";
                            else
                                label1.Text = "AI 2";
                            
                        }
                        
                    }
                    else
                    {
                        for (int i = 0; i < iForm.resourceButtons.Count(); i++)
                        {
                            if (iForm.resourceButtons[i].Visible && iForm.resourceButtons[i].Enabled)
                            {
                                Thread.Sleep(1000);

                                iForm.resourceButtons[i].PerformClick();

                                lastButtonClicked = iForm.lastButtonClicked;
                                iForm.buttonClicked = false;

                                moveResourcePiece();
                                displayBoard(players[currentPlayer]);
                                Thread.Sleep(1000);
                                enableButtons();

                                currentPlayer = ++currentPlayer % 3;
                                displayBoard(players[currentPlayer]);
                                if (currentPlayer == 0)
                                    label1.Text = "Player 1";
                                else if (currentPlayer == 1)
                                    label1.Text = "AI 1";
                                else
                                    label1.Text = "AI 2";
                                i = iForm.resourceButtons.Count() + 1;
                            }
                        }
                    }
                }
                Thread.Sleep(10);
                if (skipCount == 3)
                {
                    doneInitializing = true;
                    startTurnButton.Enabled = true;
                    iForm.Visible = false;
                    iForm.Hide();
                    
                    currentPlayer = 0;
                    Thread.Sleep(1000);
                    displayBoard(players[currentPlayer]);
                }
            }

            for (int i = 0; i < iForm.resourceButtons.Count(); i++)
            {
                iForm.resourceButtons[i].Hide();
            }
        }

        /// <summary>
        /// Toggle what board is displayed button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button19_Click(object sender, EventArgs e)
        {
            toggle ++;
            displayBoard(players[toggle % 3]);
            if (toggle % 3 == 0)
                label1.Text = "Player 1";
            else if (toggle % 3 == 1)
                label1.Text = "AI 1";
            else
                label1.Text = "AI 2";

            label2.Text = "Favor: " + players[toggle%3].resourceCubes[0];
            label3.Text = "Food: " + players[toggle%3].resourceCubes[1];
            label4.Text = "Gold: " + players[toggle%3].resourceCubes[2];
            label5.Text = "Wood: " + players[toggle%3].resourceCubes[3];
        }

        #endregion

        
        #region Initilization and Loading Resources

        /// <summary>
        /// Generates the recourse master list with images
        /// </summary>
        private void generateResourceMasterList()
        {
            for (int i = 0; i < 12; i++)
                resourceMasterList.Add(new ResourcePiece("Fertile", "Food", 2, @"C:\AgeOfMythology\Resources\Pieces\fertile_food_2.png"));
            for (int i = 0; i < 3; i++)
                resourceMasterList.Add(new ResourcePiece("Fertile", "Wood", 1, @"C:\AgeOfMythology\Resources\Pieces\fertile_wood_1.png"));
            for (int i = 0; i < 3; i++)
                resourceMasterList.Add(new ResourcePiece("Fertile", "Favor", 1, @"C:\AgeOfMythology\Resources\Pieces\fertile_favor_1.png"));
            for (int i = 0; i < 3; i++)
                resourceMasterList.Add(new ResourcePiece("Fertile", "Gold", 1, @"C:\AgeOfMythology\Resources\Pieces\fertile_gold_1.png"));

            for (int i = 0; i < 9; i++)
                resourceMasterList.Add(new ResourcePiece("Forest", "Wood", 2, @"C:\AgeOfMythology\Resources\Pieces\forest_wood_2.png"));
            for (int i = 0; i < 2; i++)
                resourceMasterList.Add(new ResourcePiece("Forest", "Food", 1, @"C:\AgeOfMythology\Resources\Pieces\forest_food_1.png"));
            for (int i = 0; i < 2; i++)
                resourceMasterList.Add(new ResourcePiece("Forest", "Gold", 1, @"C:\AgeOfMythology\Resources\Pieces\forest_gold_1.png"));
            for (int i = 0; i < 2; i++)
                resourceMasterList.Add(new ResourcePiece("Forest", "Favor", 1, @"C:\AgeOfMythology\Resources\Pieces\forest_favor_1.png"));

            for (int i = 0; i < 4; i++)
                resourceMasterList.Add(new ResourcePiece("Hills", "Gold", 2, @"C:\AgeOfMythology\Resources\Pieces\hills_gold_2.png"));
            for (int i = 0; i < 4; i++)
                resourceMasterList.Add(new ResourcePiece("Hills", "Food", 1, @"C:\AgeOfMythology\Resources\Pieces\hills_food_1.png"));
            for (int i = 0; i < 4; i++)
                resourceMasterList.Add(new ResourcePiece("Hills", "Wood", 1, @"C:\AgeOfMythology\Resources\Pieces\hills_wood_1.png"));
            for (int i = 0; i < 4; i++)
                resourceMasterList.Add(new ResourcePiece("Hills", "Favor", 1, @"C:\AgeOfMythology\Resources\Pieces\hills_favor_1.png"));

            for (int i = 0; i < 6; i++)
                resourceMasterList.Add(new ResourcePiece("Mountains", "Gold", 2, @"C:\AgeOfMythology\Resources\Pieces\mountains_gold_2.png"));
            for (int i = 0; i < 3; i++)
                resourceMasterList.Add(new ResourcePiece("Mountains", "Wood", 1, @"C:\AgeOfMythology\Resources\Pieces\mountains_wood_1.png"));
            for (int i = 0; i < 3; i++)
                resourceMasterList.Add(new ResourcePiece("Mountains", "Favor", 1, @"C:\AgeOfMythology\Resources\Pieces\mountains_favor_1.png"));

            for (int i = 0; i < 7; i++)
                resourceMasterList.Add(new ResourcePiece("Desert", "Favor", 2, @"C:\AgeOfMythology\Resources\Pieces\desert_favor_2.png"));
            for (int i = 0; i < 7; i++)
                resourceMasterList.Add(new ResourcePiece("Desert", "Gold", 1, @"C:\AgeOfMythology\Resources\Pieces\desert_gold_1.png"));

            for (int i = 0; i < 4; i++)
                resourceMasterList.Add(new ResourcePiece("Swamp", "Wood", 1, @"C:\AgeOfMythology\Resources\Pieces\swamp_wood_1.png"));
            for (int i = 0; i < 4; i++)
                resourceMasterList.Add(new ResourcePiece("Swamp", "Food", 1, @"C:\AgeOfMythology\Resources\Pieces\swamp_food_1.png"));
            for (int i = 0; i < 4; i++)
                resourceMasterList.Add(new ResourcePiece("Swamp", "Favor", 1, @"C:\AgeOfMythology\Resources\Pieces\swamp_favor_1.png"));

        }

        /// <summary>
        /// Generating the central resource list for cities
        /// </summary>
        private void generateCityMasterList()
        {
            // { FAVOR, FOOD, GOLD, WOOD } 
            cityMasterList.Add(new CityPiece("Armory", "C:\\AgeOfMythology\\Resources\\buildings\\armory.png", new int[] { 0, 0, 2, 3 }));
            cityMasterList.Add(new CityPiece("Gold Mint", "C:\\AgeOfMythology\\Resources\\buildings\\gold_mint.png", new int[] { 0, 3, 0, 2 }));
            cityMasterList.Add(new CityPiece("Granary", "C:\\AgeOfMythology\\Resources\\buildings\\granary.png", new int[] { 0, 0, 3, 2 }));
            cityMasterList.Add(new CityPiece("Great Temple", "C:\\AgeOfMythology\\Resources\\buildings\\great_temple.png", new int[] { 4, 4, 4, 4 }));
            cityMasterList.Add(new CityPiece("House", "C:\\AgeOfMythology\\Resources\\buildings\\house.png", new int[] { 0, 2, 0, 2 }));
            cityMasterList.Add(new CityPiece("Market", "C:\\AgeOfMythology\\Resources\\buildings\\market.png", new int[] { 2, 0, 3, 0} ));
            cityMasterList.Add(new CityPiece("Monument", "C:\\AgeOfMythology\\Resources\\buildings\\monument.png", new int[] { 0, 3, 2, 0 } ));
            cityMasterList.Add(new CityPiece("Quarry", "C:\\AgeOfMythology\\Resources\\buildings\\quarry.png", new int[] { 0, 4, 1, 0 }));
            cityMasterList.Add(new CityPiece("Siege Workshop", "C:\\AgeOfMythology\\Resources\\buildings\\siege_workshop.png", new int[] { 0, 0, 2, 3 }));
            cityMasterList.Add(new CityPiece("Storehouse", "C:\\AgeOfMythology\\Resources\\buildings\\storehouse.png", new int[] { 2, 2, 2, 2 }));
            cityMasterList.Add(new CityPiece("Tower", "C:\\AgeOfMythology\\Resources\\buildings\\tower.png", new int[] { 0, 0, 3, 3 }));
            cityMasterList.Add(new CityPiece("Wall", "C:\\AgeOfMythology\\Resources\\buildings\\wall.png", new int[] { 0, 0, 3, 3 }));
            cityMasterList.Add(new CityPiece("Wonder", "C:\\AgeOfMythology\\Resources\\buildings\\wonder.png", new int[] { 10, 10, 10, 10 }));
            cityMasterList.Add(new CityPiece("Wood Workshop", "C:\\AgeOfMythology\\Resources\\buildings\\wood_workshop.png", new int[] { 0, 2, 3, 0 }));
        }

        /// <summary>
        /// generate evil list of evil
        /// </summary>
        private void generateBattleUnitMasterList()
        {
            //FAVOR, FOOD, GOLD, WOOD
            //egypt
            battleUnitMasterList.Add(new BattleUnits(new int[] { 1, 0, 3, 0 }, "Anubite", 5, "C:\\AgeOfMythology\\Resources\\Battle Units\\e_batt_anubite.png"));
            battleUnitMasterList.Add(new BattleUnits(new int[] { 0, 0, 1, 1 }, "Chariot Archer", 3, "C:\\AgeOfMythology\\Resources\\Battle Units\\e_batt_chariotArcher.png"));
            battleUnitMasterList.Add(new BattleUnits(new int[] { 0, 2, 1, 0 }, "Elephant", 3, "C:\\AgeOfMythology\\Resources\\Battle Units\\e_batt_elephant.png"));
            battleUnitMasterList.Add(new BattleUnits(new int[] { 2, 0, 3, 0 }, "Mummy", 5, "C:\\AgeOfMythology\\Resources\\Battle Units\\e_batt_mummy.png"));
            battleUnitMasterList.Add(new BattleUnits(new int[] { 0, 3, 3, 0 }, "Pharoh", 6, "C:\\AgeOfMythology\\Resources\\Battle Units\\e_batt_pharaoh.png"));
            battleUnitMasterList.Add(new BattleUnits(new int[] { 0, 2, 4, 0 }, "Priest", 4, "C:\\AgeOfMythology\\Resources\\Battle Units\\e_batt_priest.png"));
            battleUnitMasterList.Add(new BattleUnits(new int[] { 0, 4, 2, 0 }, "Scorpian Man", 5, "C:\\AgeOfMythology\\Resources\\Battle Units\\e_batt_scorpionMan.png"));
            battleUnitMasterList.Add(new BattleUnits(new int[] { 4, 0, 4, 0 }, "Son of Osiris", 8, "C:\\AgeOfMythology\\Resources\\Battle Units\\e_batt_sonOfOsiris.png"));
            battleUnitMasterList.Add(new BattleUnits(new int[] { 2, 0, 2, 0 }, "Sphinx", 5, "C:\\AgeOfMythology\\Resources\\Battle Units\\e_batt_sphinx.png"));
            battleUnitMasterList.Add(new BattleUnits(new int[] { 2, 2, 0, 0 }, "Wadjet", 5, "C:\\AgeOfMythology\\Resources\\Battle Units\\e_batt_wadjet.png"));

            //greek
            battleUnitMasterList.Add(new BattleUnits(new int[] { 0, 1, 0, 3 }, "Centaur", 5, "C:\\AgeOfMythology\\Resources\\Battle Units\\g_batt_centaur.png"));
            battleUnitMasterList.Add(new BattleUnits(new int[] { 0, 3, 3, 0 }, "Classical Greek Hero", 5, "C:\\AgeOfMythology\\Resources\\Battle Units\\g_batt_classicalHero.png"));
            battleUnitMasterList.Add(new BattleUnits(new int[] { 3, 3, 0, 0 }, "Cyclops", 6, "C:\\AgeOfMythology\\Resources\\Battle Units\\g_batt_cyclops.png"));
            battleUnitMasterList.Add(new BattleUnits(new int[] { 0, 3, 4, 0 }, "Heroic Greek Hero", 6, "C:\\AgeOfMythology\\Resources\\Battle Units\\g_batt_heroicHero.png"));
            battleUnitMasterList.Add(new BattleUnits(new int[] { 0, 1, 1, 0 }, "Hippoken", 3, "C:\\AgeOfMythology\\Resources\\Battle Units\\g_batt_hippokon.png"));
            battleUnitMasterList.Add(new BattleUnits(new int[] { 0, 1, 0, 1 }, "Hoplite", 3, "C:\\AgeOfMythology\\Resources\\Battle Units\\g_batt_hoplite.png"));
            battleUnitMasterList.Add(new BattleUnits(new int[] { 2, 0, 2, 0 }, "Hydra", 6, "C:\\AgeOfMythology\\Resources\\Battle Units\\g_batt_hydra.png"));
            battleUnitMasterList.Add(new BattleUnits(new int[] { 3, 1, 0, 0 }, "Medusa", 5, "C:\\AgeOfMythology\\Resources\\Battle Units\\g_batt_madusa.png"));
            battleUnitMasterList.Add(new BattleUnits(new int[] { 2, 2, 0, 0 }, "Manticore", 5, "C:\\AgeOfMythology\\Resources\\Battle Units\\g_batt_manticore.png"));
            battleUnitMasterList.Add(new BattleUnits(new int[] { 0, 2, 0, 2 }, "Minotaur", 5, "C:\\AgeOfMythology\\Resources\\Battle Units\\g_batt_minotaur.png"));
            battleUnitMasterList.Add(new BattleUnits(new int[] { 4, 0, 4, 0 }, "Mythical Greek Hero", 5, "C:\\AgeOfMythology\\Resources\\Battle Units\\g_batt_mythicalHero.png"));
            battleUnitMasterList.Add(new BattleUnits(new int[] { 0, 1, 0, 1 }, "Toxotes", 3, "C:\\AgeOfMythology\\Resources\\Battle Units\\g_batt_toxotes.png"));

            //norse
            battleUnitMasterList.Add(new BattleUnits(new int[] { 0, 3, 3, 0 }, "Classical Norse Hero", 5, "C:\\AgeOfMythology\\Resources\\Battle Units\\n_batt_classicalHero.png"));
            battleUnitMasterList.Add(new BattleUnits(new int[] { 0, 2, 2, 0 }, "“Dwarf", 4, "C:\\AgeOfMythology\\Resources\\Battle Units\\n_batt_dwarf.png"));
            battleUnitMasterList.Add(new BattleUnits(new int[] { 2, 4, 0, 0 }, "Frost Giant", 7, "C:\\AgeOfMythology\\Resources\\Battle Units\\n_batt_frostGiant.png"));
            battleUnitMasterList.Add(new BattleUnits(new int[] { 0, 3, 3, 0 }, "Heroic Norse Hero", 6, "C:\\AgeOfMythology\\Resources\\Battle Units\\n_batt_heroicHero.png"));
            battleUnitMasterList.Add(new BattleUnits(new int[] { 0, 1, 2, 0 }, "Huskarl", 3, "C:\\AgeOfMythology\\Resources\\Battle Units\\n_batt_huskarl.png"));
            battleUnitMasterList.Add(new BattleUnits(new int[] { 0, 1, 1, 0 }, "Jarl", 3, "C:\\AgeOfMythology\\Resources\\Battle Units\\n_batt_jarl.png"));
            battleUnitMasterList.Add(new BattleUnits(new int[] { 4, 4, 0, 0 }, "Mythic Norse Hero", 8, "C:\\AgeOfMythology\\Resources\\Battle Units\\n_batt_mythicHero.png"));
            battleUnitMasterList.Add(new BattleUnits(new int[] { 1, 0, 4, 0 }, "Nidhogg", 7, "C:\\AgeOfMythology\\Resources\\Battle Units\\n_batt_nidhogg.png"));
            battleUnitMasterList.Add(new BattleUnits(new int[] { 0, 1, 0, 1 }, "Throwing Axeman", 3, "C:\\AgeOfMythology\\Resources\\Battle Units\\n_batt_throwingAxeman.png"));
            battleUnitMasterList.Add(new BattleUnits(new int[] { 0, 3, 0, 2 }, "Troll", 6, "C:\\AgeOfMythology\\Resources\\Battle Units\\n_batt_troll.png"));
            battleUnitMasterList.Add(new BattleUnits(new int[] { 3, 0, 1, 0 }, "Valkyrie", 5, "C:\\AgeOfMythology\\Resources\\Battle Units\\n_batt_valkyrie.png"));

        }

        /// <summary>
        /// Generates the random recources to be put into the list
        /// </summary>
        private void generatePieces()
        {
            Random r = new Random();
            int num = 0;
            for (int i = 0; i < 18; i++)
            {
                num = r.Next(0, resourceMasterList.Count() - 1);
                resourcePieces.Add(resourceMasterList[num]);
                resourceMasterList.RemoveAt(num);
            }
        }

        

        /// <summary>
        /// loads the action card images into the program (strings)
        /// </summary>
        private void loadActionCards()
        {
            actionCards[0] = "C:\\AgeOfMythology\\Resources\\Action Cards\\e_perm_attack.png";
            actionCards[1] = "C:\\AgeOfMythology\\Resources\\Action Cards\\e_perm_build.png";
            actionCards[2] = "C:\\AgeOfMythology\\Resources\\Action Cards\\e_perm_explore.png";
            actionCards[3] = "C:\\AgeOfMythology\\Resources\\Action Cards\\e_perm_gather.png";
            actionCards[4] = "C:\\AgeOfMythology\\Resources\\Action Cards\\e_perm_next.png";
            actionCards[5] = "C:\\AgeOfMythology\\Resources\\Action Cards\\e_perm_recruit.png";
            actionCards[6] = "C:\\AgeOfMythology\\Resources\\Action Cards\\e_perm_trade.png";

            actionCards[7] = "C:\\AgeOfMythology\\Resources\\Action Cards\\g_perm_attack.png";
            actionCards[8] = "C:\\AgeOfMythology\\Resources\\Action Cards\\g_perm_build.png";
            actionCards[9] = "C:\\AgeOfMythology\\Resources\\Action Cards\\g_perm_explore.png";
            actionCards[10] = "C:\\AgeOfMythology\\Resources\\Action Cards\\g_perm_gather.png";
            actionCards[11] = "C:\\AgeOfMythology\\Resources\\Action Cards\\g_perm_next.png";
            actionCards[12] = "C:\\AgeOfMythology\\Resources\\Action Cards\\g_perm_recruit.png";
            actionCards[13] = "C:\\AgeOfMythology\\Resources\\Action Cards\\g_perm_trade.png";

            actionCards[14] = "C:\\AgeOfMythology\\Resources\\Action Cards\\n_perm_attack.png";
            actionCards[15] = "C:\\AgeOfMythology\\Resources\\Action Cards\\n_perm_build.png";
            actionCards[16] = "C:\\AgeOfMythology\\Resources\\Action Cards\\n_perm_explore.png";
            actionCards[17] = "C:\\AgeOfMythology\\Resources\\Action Cards\\n_perm_gather.png";
            actionCards[18] = "C:\\AgeOfMythology\\Resources\\Action Cards\\n_perm_next.png";
            actionCards[19] = "C:\\AgeOfMythology\\Resources\\Action Cards\\n_perm_recruit.png";
            actionCards[20] = "C:\\AgeOfMythology\\Resources\\Action Cards\\n_perm_trade.png";

            //god cards
            actionCards[21] = "C:\\AgeOfMythology\\Resources\\Action Cards\\e_god_build.png";
            actionCards[22] = "C:\\AgeOfMythology\\Resources\\Action Cards\\e_god_explore.png";
            actionCards[23] = "C:\\AgeOfMythology\\Resources\\Action Cards\\e_god_gather.png";
            actionCards[24] = "C:\\AgeOfMythology\\Resources\\Action Cards\\e_god_next.png";
            actionCards[25] = "C:\\AgeOfMythology\\Resources\\Action Cards\\e_god_recruit.png";
            actionCards[26] = "C:\\AgeOfMythology\\Resources\\Action Cards\\e_god_trade.png";

            actionCards[27] = "C:\\AgeOfMythology\\Resources\\Action Cards\\g_god_build.png";
            actionCards[28] = "C:\\AgeOfMythology\\Resources\\Action Cards\\g_god_explore.png";
            actionCards[29] = "C:\\AgeOfMythology\\Resources\\Action Cards\\g_god_gather.png";
            actionCards[30] = "C:\\AgeOfMythology\\Resources\\Action Cards\\g_god_next.png";
            actionCards[31] = "C:\\AgeOfMythology\\Resources\\Action Cards\\g_god_recruit.png";
            actionCards[32] = "C:\\AgeOfMythology\\Resources\\Action Cards\\g_god_trade.png";

            actionCards[33] = "C:\\AgeOfMythology\\Resources\\Action Cards\\n_god_build.png";
            actionCards[34] = "C:\\AgeOfMythology\\Resources\\Action Cards\\n_god_explore.png";
            actionCards[35] = "C:\\AgeOfMythology\\Resources\\Action Cards\\n_god_gather.png";
            actionCards[36] = "C:\\AgeOfMythology\\Resources\\Action Cards\\n_god_next.png";
            actionCards[37] = "C:\\AgeOfMythology\\Resources\\Action Cards\\n_god_recruit.png";
            actionCards[38] = "C:\\AgeOfMythology\\Resources\\Action Cards\\n_god_trade.png";
        }

        #endregion


        #region private methods
        public void displayBoard(Player cPlayer)
        {
            // RESOURCE AREA
            for (int i = 0; i < 16; i++)
            {
                resourceAreaPictureBoxes[i].BackgroundImage = cPlayer.resourceArea.tiles[i].displayPicture;
                resourceAreaPictureBoxes[i].Image = cPlayer.resourceArea.tiles[i].overlayPicture;
            }

            //CITY AREA
            this.CityAreaPanel.BackgroundImage = cPlayer.cityArea.staticBackgroundImage;
            if (cPlayer.culture == 'n')
                this.HoldingAreaPanel.BackgroundImage = Image.FromFile(@"C:\Users\Thomas\OneDrive\Projects\CSULB\AgeOfMythology\Resources\BackgroundImages\n_holding.png");
            else if (cPlayer.culture == 'e')
                this.HoldingAreaPanel.BackgroundImage = Image.FromFile(@"C:\Users\Thomas\OneDrive\Projects\CSULB\AgeOfMythology\Resources\BackgroundImages\e_holding.png");
            else
                this.HoldingAreaPanel.BackgroundImage = Image.FromFile(@"C:\Users\Thomas\OneDrive\Projects\CSULB\AgeOfMythology\Resources\BackgroundImages\g_holding.png");

            for (int i = 0; i < 16; i++)
            {
                if (cPlayer.cityArea.tiles[i].overlayPicture != null)
                    cityAreaPictureBoxes[i].Image = cPlayer.cityArea.tiles[i].overlayPicture;
            }

            if (currentPlayer == 0)
                label1.Text = "Player 1";
            else if (currentPlayer == 1)
                label1.Text = "AI 1";
            else
                label1.Text = "AI 2";

            label2.Text = "Favor: " + players[currentPlayer].resourceCubes[0];
            label3.Text = "Food: " + players[currentPlayer].resourceCubes[1];
            label4.Text = "Gold: " + players[currentPlayer].resourceCubes[2];
            label5.Text = "Wood: " + players[currentPlayer].resourceCubes[3];

            this.Refresh();
        }

        /// <summary>
        /// Displays the recourse buttons Image and sets the style of the buttons
        /// </summary>
        private void displayResourceButtons()
        {
            for (int i = 0; i < 18; i++)
            {

                iForm.resourceButtons[i].Image = Image.FromFile(resourcePieces[i].picture);
                iForm.resourceButtons[i].FlatStyle = FlatStyle.Flat;
            }
            //button1.Refresh();
        }

        /// <summary>
        /// Places the last picked recource tile onto the current players recource board
        /// </summary>
        private void moveResourcePiece()
        {
            for (int i = 0; i < players[currentPlayer].resourceArea.tiles.Length; i++)
            {
                if (players[currentPlayer].resourceArea.tiles[i].type.Equals(resourcePieces[lastButtonClicked - 1].terrainType) && !players[currentPlayer].resourceArea.tiles[i].isFilled)
                {
                    players[currentPlayer].resourceArea.tiles[i].isFilled = true;

                    players[currentPlayer].resourceArea.tiles[i].overlayPicture = ResizeImage(Image.FromFile(resourcePieces[lastButtonClicked - 1].picture), 50, 50);

                    if (resourcePieces[lastButtonClicked - 1].terrainType.Equals("Mountains"))
                        players[currentPlayer].resourceArea.mountainCount--;
                    else if (resourcePieces[lastButtonClicked - 1].terrainType.Equals("Hills"))
                        players[currentPlayer].resourceArea.hillCount--;
                    else if (resourcePieces[lastButtonClicked - 1].terrainType.Equals("Fertile"))
                        players[currentPlayer].resourceArea.fertileCount--;
                    else if (resourcePieces[lastButtonClicked - 1].terrainType.Equals("Swamp"))
                        players[currentPlayer].resourceArea.swampCount--;
                    else if (resourcePieces[lastButtonClicked - 1].terrainType.Equals("Desert"))
                        players[currentPlayer].resourceArea.desertCount--;
                    else if (resourcePieces[lastButtonClicked - 1].terrainType.Equals("Forest"))
                        players[currentPlayer].resourceArea.forestCount--;
                    else
                    { }
                    i = 19;
                }

            }
            players[currentPlayer].resourcePiecesList.Add(resourcePieces[lastButtonClicked - 1]);
        }

        /// <summary>
        /// Enables all the recource buttons
        /// </summary>
        private void enableButtons()
        {
            Thread.Sleep(100);
            for (int i = 0; i < iForm.resourceButtons.Count(); i++)
            {
                if (iForm.resourceButtons[i].Visible)
                {
                    iForm.resourceButtons[i].Enabled = true;
                }
            }
        }

        /// <summary>
        /// enables the appropriate recource buttons to be able to click during initialization
        /// </summary>
        /// <returns></returns>
        private bool getPossibleMoves()
        {
            Thread.Sleep(100);
            bool noPossibleMoves = true;
            for (int i = 0; i < iForm.resourceButtons.Count(); i++)
            {
                if (iForm.resourceButtons[i].Visible)
                {

                    if (resourcePieces[i].terrainType.Equals("Mountains") && players[currentPlayer].resourceArea.mountainCount == 0)
                        iForm.resourceButtons[i].Enabled = false;
                    else if (resourcePieces[i].terrainType.Equals("Hills") && players[currentPlayer].resourceArea.hillCount == 0)
                        iForm.resourceButtons[i].Enabled = false;
                    else if (resourcePieces[i].terrainType.Equals("Fertile") && players[currentPlayer].resourceArea.fertileCount == 0)
                        iForm.resourceButtons[i].Enabled = false;
                    else if (resourcePieces[i].terrainType.Equals("Swamp") && players[currentPlayer].resourceArea.swampCount == 0)
                        iForm.resourceButtons[i].Enabled = false;
                    else if (resourcePieces[i].terrainType.Equals("Desert") && players[currentPlayer].resourceArea.desertCount == 0)
                        iForm.resourceButtons[i].Enabled = false;
                    else if (resourcePieces[i].terrainType.Equals("Forest") && players[currentPlayer].resourceArea.forestCount == 0)
                        iForm.resourceButtons[i].Enabled = false;
                    else
                    { }

                }
                if (iForm.resourceButtons[i].Enabled && iForm.resourceButtons[i].Visible)
                {
                    noPossibleMoves = false;
                }


            }

            return noPossibleMoves;
        }

        /// <summary>
        /// Resize an image.
        /// </summary>
        /// <param name="img"></param>
        /// <param name="minsize"></param>
        /// <returns></returns>
        public static Image ResizeImage(Image img, int h, int w)
        {
            var size = img.Size;
            size.Height = h;
            size.Width = w;
            return new Bitmap(img, size);
        }
        #endregion
        

        #region Start and End turn buttons

        private void startTurnButton_Click(object sender, EventArgs e)
        {
            startTurnButton.Enabled = false;
            int totalMoves = 3;

            vcForms.Show();
            
            while (!vcForms.victoryCardsDone)
            {
                Application.DoEvents();
                Thread.Sleep(100);
            }
            vcForms.victoryCardsDone = false;

            ActionCardForm aCForm = new ActionCardForm(players[currentPlayer].age, players[currentPlayer].culture, actionCards, ref actionsToPerform);
            aCForm.Show();

            while (!aCForm.actionFormDone)
            {
                Application.DoEvents();
                Thread.Sleep(100);
            }
            aCForm.actionFormDone = false;
            DisplayHandForm dHandForm = new DisplayHandForm(actionsToPerform, ref players[currentPlayer], totalMoves, actionCards, cityMasterList, battleUnitMasterList, ref resourceMasterList, currentPlayer, ref players);
            dHandForm.Show();

            endTurnbutton.Enabled = true;

            while (!turnDone)
            {
                Application.DoEvents();
                Thread.Sleep(100);
                displayBoard(players[currentPlayer]);
            }

            

             
        }

        private void endTurnbutton_Click(object sender, EventArgs e)
        {
            turnDone = true;
            //spoilage for player
            spoilage();
            displayBoard(players[currentPlayer]);
            MessageBox.Show("Turn Over!");
            startTurnButton.Enabled = true;
            endTurnbutton.Enabled = false;

            currentPlayer++;
            //AI 1 victory cube menu
            
            vcForms.Show();
            while (!vcForms.victoryCardsDone)
            {
                Application.DoEvents();
                Thread.Sleep(100);
            }
            vcForms.victoryCardsDone = false;
            //AI 1 turn
            Random r = new Random();
            DisplayHandForm dHandForm = new DisplayHandForm(actionsToPerform, ref players[currentPlayer], 4, actionCards, cityMasterList, battleUnitMasterList, ref resourceMasterList, currentPlayer, ref players);
            
            int firstAction = r.Next(0, 4);

            int secondAction = r.Next(0, 4);
            while (secondAction == firstAction)
                secondAction = r.Next(0, 4);
            int thirdAction = r.Next(0, 4);
            while (thirdAction == firstAction || thirdAction == secondAction)
                thirdAction = r.Next(0, 4);

            dHandForm.performAction(firstAction + 1);
            dHandForm.performAction(secondAction + 1);
            dHandForm.performAction(thirdAction + 1);

            //spoilage for AI 1
            spoilage();

            currentPlayer++;

            firstAction = r.Next(0, 4);

            secondAction = r.Next(0, 4);
            while (secondAction == firstAction)
                secondAction = r.Next(0, 4);
            thirdAction = r.Next(0, 4);
            while (thirdAction == firstAction || thirdAction == secondAction)
                thirdAction = r.Next(0, 4);
            //AI 2 victory cube menu
            vcForms.victoryCardsDone = false;
            vcForms.Show();
            while (!vcForms.victoryCardsDone)
            {
                Application.DoEvents();
                Thread.Sleep(100);
            }
            //AI2 turn
            dHandForm.performAction(firstAction + 1);
            dHandForm.performAction(secondAction + 1);
            dHandForm.performAction(thirdAction + 1);

            //spoilage for AI 2
            spoilage();

            currentPlayer = (currentPlayer + 1) % 3;
            vcForms.victoryCardsDone = false;
        }

        private void spoilage()
        {
            actionsPerformed.Clear();
            actionsToPerform.Clear();
            
            bool spoil = true;
            for (int i = 0; i < players[currentPlayer].cityPiecesList.Count(); i++)
            {
                if (players[currentPlayer].cityPiecesList[i].buildingType.Equals("Storehouse"))
                    spoil = false;
            }
            for (int i = 0; i < 4; i++)
            {
                
                if (players[currentPlayer].resourceCubes[i] > 5 && spoil)
                    players[currentPlayer].resourceCubes[i] = 5;
                else if (players[currentPlayer].resourceCubes[i] > 8 && spoil)
                    players[currentPlayer].resourceCubes[i] = 8;

            }
        }
        #endregion


    }
}
