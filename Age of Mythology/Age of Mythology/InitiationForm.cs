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
    public partial class InitiationForm : Form
    {
        public List<ResourcePiece> resourcePieces = new List<ResourcePiece>();
        public Button[] resourceButtons;
        public bool buttonClicked;
        public int lastButtonClicked = 0;
        bool doneInitializing = false;
        List<ResourcePiece> rMasterList;
        Player player;
        int currentPlayer;
        bool godMode = false;
        int godBoost = 0;
        
        /// <summary>
        /// Constructor
        /// </summary>
        public InitiationForm(List<ResourcePiece> rPieces)
        {
            InitializeComponent();
            this.resourcePieces = rPieces;
            resourceButtons = new Button[] { button1, button2, button3, button4, button5, button6, button7, button8, button9, button10, button11, button12, button13, button14, button15, button16, button17, button18 };
            
        }


        public InitiationForm(ref Player p, int cp, ref List<ResourcePiece> rml)
        {
            InitializeComponent();
            resourceButtons = new Button[] { button1, button2, button3, button4, button5, button6, button7, button8, button9, button10, button11, button12, button13, button14, button15, button16, button17, button18 };
            
            player = p;
            currentPlayer = cp;
            rMasterList = rml;
            generatePieces();
            displayResourceButtons();

            for (int i = 0; i < 14; i++)
            {
                resourceButtons[i + 4].Enabled = false;
                resourceButtons[i + 4].Visible = false;
            }
        }


        public InitiationForm(ref Player p, int cp, ref List<ResourcePiece> rml, bool godModeActivated)
        {
            InitializeComponent();
            resourceButtons = new Button[] { button1, button2, button3, button4, button5, button6, button7, button8, button9, button10, button11, button12, button13, button14, button15, button16, button17, button18 };
            godMode = true;
            player = p;
            currentPlayer = cp;
            rMasterList = rml;
            generatePieces();
            displayResourceButtons();

            for (int i = 0; i < 12; i++)
            {
                resourceButtons[i + 6].Enabled = false;
                resourceButtons[i + 6].Visible = false;
            }

            
        }


        /// <summary>
        /// Form load event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InitiationForm_Load(object sender, EventArgs e)
        {
            
            this.Location = new Point(850, 0);
        }



        #region click events

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Visible = false;
            buttonClicked = true;
            lastButtonClicked = 1;
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button2.Visible = false;
            buttonClicked = true;
            lastButtonClicked = 2;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            button3.Visible = false;
            buttonClicked = true;
            lastButtonClicked = 3;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            button4.Visible = false;
            buttonClicked = true;
            lastButtonClicked = 4;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            button5.Visible = false;
            buttonClicked = true;
            lastButtonClicked = 5;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            button6.Visible = false;
            buttonClicked = true;
            lastButtonClicked = 6;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            button7.Visible = false;
            buttonClicked = true;
            lastButtonClicked = 7;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            button8.Visible = false;
            buttonClicked = true;
            lastButtonClicked = 8;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            button9.Visible = false;
            buttonClicked = true;
            lastButtonClicked = 9;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            button10.Visible = false;
            buttonClicked = true;
            lastButtonClicked = 10;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            button11.Visible = false;
            buttonClicked = true;
            lastButtonClicked = 11;
        }

        private void button12_Click(object sender, EventArgs e)
        {
            button12.Visible = false;
            buttonClicked = true;
            lastButtonClicked = 12;
        }

        private void button13_Click(object sender, EventArgs e)
        {
            button13.Visible = false;
            buttonClicked = true;
            lastButtonClicked = 13;
        }

        private void button14_Click(object sender, EventArgs e)
        {
            button14.Visible = false;
            buttonClicked = true;
            lastButtonClicked = 14;
        }

        private void button15_Click(object sender, EventArgs e)
        {
            button15.Visible = false;
            buttonClicked = true;
            lastButtonClicked = 15;
        }

        private void button16_Click_1(object sender, EventArgs e)
        {
            button16.Visible = false;
            buttonClicked = true;
            lastButtonClicked = 16;
        }

        private void button17_Click_1(object sender, EventArgs e)
        {
            button17.Visible = false;
            buttonClicked = true;
            lastButtonClicked = 17;
        }

        private void button18_Click_1(object sender, EventArgs e)
        {
            button18.Visible = false;
            buttonClicked = true;
            lastButtonClicked = 18;
        }

        #endregion

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }





        /// <summary>
        /// enables the appropriate recource buttons to be able to click during initialization
        /// </summary>
        /// <returns></returns>
        private bool getPossibleMoves()
        {
            Thread.Sleep(100);
            bool noPossibleMoves = true;
            for (int i = 0; i < 4; i++)
            {
                if (this.resourceButtons[i].Visible)
                {

                    if (resourcePieces[i].terrainType.Equals("Mountains") && player.resourceArea.mountainCount == 0)
                        this.resourceButtons[i].Enabled = false;
                    else if (resourcePieces[i].terrainType.Equals("Hills") && player.resourceArea.hillCount == 0)
                        this.resourceButtons[i].Enabled = false;
                    else if (resourcePieces[i].terrainType.Equals("Fertile") && player.resourceArea.fertileCount == 0)
                        this.resourceButtons[i].Enabled = false;
                    else if (resourcePieces[i].terrainType.Equals("Swamp") && player.resourceArea.swampCount == 0)
                        this.resourceButtons[i].Enabled = false;
                    else if (resourcePieces[i].terrainType.Equals("Desert") && player.resourceArea.desertCount == 0)
                        this.resourceButtons[i].Enabled = false;
                    else if (resourcePieces[i].terrainType.Equals("Forest") && player.resourceArea.forestCount == 0)
                        this.resourceButtons[i].Enabled = false;
                    else
                    { }

                }
                if (this.resourceButtons[i].Enabled && this.resourceButtons[i].Visible)
                {
                    noPossibleMoves = false;
                }


            }

            return noPossibleMoves;
        }

        private void moveResourcePiece()
        {
            for (int i = 0; i < player.resourceArea.tiles.Length; i++)
            {
                if (player.resourceArea.tiles[i].type.Equals(resourcePieces[lastButtonClicked - 1].terrainType) && !player.resourceArea.tiles[i].isFilled)
                {
                    player.resourceArea.tiles[i].isFilled = true;

                    player.resourceArea.tiles[i].overlayPicture = ResizeImage(Image.FromFile(resourcePieces[lastButtonClicked - 1].picture), 50, 50);

                    if (resourcePieces[lastButtonClicked - 1].terrainType.Equals("Mountains"))
                        player.resourceArea.mountainCount--;
                    else if (resourcePieces[lastButtonClicked - 1].terrainType.Equals("Hills"))
                        player.resourceArea.hillCount--;
                    else if (resourcePieces[lastButtonClicked - 1].terrainType.Equals("Fertile"))
                        player.resourceArea.fertileCount--;
                    else if (resourcePieces[lastButtonClicked - 1].terrainType.Equals("Swamp"))
                        player.resourceArea.swampCount--;
                    else if (resourcePieces[lastButtonClicked - 1].terrainType.Equals("Desert"))
                        player.resourceArea.desertCount--;
                    else if (resourcePieces[lastButtonClicked - 1].terrainType.Equals("Forest"))
                        player.resourceArea.forestCount--;
                    else
                    { }
                    i = 19;
                }

            }
            player.resourcePiecesList.Add(resourcePieces[lastButtonClicked - 1]);
        }

        /// <summary>
        /// Enables all the recource buttons
        /// </summary>
        private void enableButtons()
        {
            Thread.Sleep(100);
            for (int i = 0; i < this.resourceButtons.Count(); i++)
            {
                if (this.resourceButtons[i].Visible)
                {
                    this.resourceButtons[i].Enabled = true;
                }
            }


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

        /// <summary>
        /// Displays the recourse buttons Image and sets the style of the buttons
        /// </summary>
        private void displayResourceButtons()
        {
            for (int i = 0; i < 4; i++)
            {

                this.resourceButtons[i].Image = Image.FromFile(resourcePieces[i].picture);
                this.resourceButtons[i].FlatStyle = FlatStyle.Flat;
            
            
            }

            if (godMode && player.culture != 'n')
            {
                this.resourceButtons[4].Image = Image.FromFile(resourcePieces[4].picture);
                this.resourceButtons[4].FlatStyle = FlatStyle.Flat;
                this.resourceButtons[5].Image = Image.FromFile(resourcePieces[5].picture);
                this.resourceButtons[5].FlatStyle = FlatStyle.Flat;
            }
            //button1.Refresh();
        }

        private void generatePieces()
        {
            Random r = new Random();
            int num = 0;
            for (int i = 0; i < 4; i++)
            {
                num = r.Next(0, rMasterList.Count() - 1);
                resourcePieces.Add(rMasterList[num]);
                rMasterList.RemoveAt(num);
            }
            if (godMode && player.culture != 'n')
            {
                num = r.Next(0, rMasterList.Count() - 1);
                resourcePieces.Add(rMasterList[num]);
                rMasterList.RemoveAt(num);

                num = r.Next(0, rMasterList.Count() - 1);
                resourcePieces.Add(rMasterList[num]);
                rMasterList.RemoveAt(num);
            }

        }

        private void loop()
        {
            int skipCount = 0;
            getPossibleMoves();

            while (!doneInitializing)
            {
                Application.DoEvents();
                
                Thread.Sleep(100);

                if (getPossibleMoves())
                {
                    if (!godMode)
                        currentPlayer = ++currentPlayer % 3;
                    else
                    {
                        if (godBoost < 2)
                            godBoost++;
                        else if (godBoost >= 2)
                            currentPlayer = ++currentPlayer % 3;
                    }
                    skipCount++;
                }
                else
                {
                    skipCount = 0;

                    if (currentPlayer == 0)
                    {

                        if (buttonClicked)
                        {

                            this.buttonClicked = false;
                            enableButtons();
                            moveResourcePiece();


                            Thread.Sleep(1000);

                            currentPlayer = ++currentPlayer % 3;

                        }

                    }
                    else
                    {
                        for (int i = 0; i < this.resourceButtons.Count(); i++)
                        {
                            if (this.resourceButtons[i].Visible && this.resourceButtons[i].Enabled)
                            {
                                Thread.Sleep(1000);

                                this.resourceButtons[i].PerformClick();

                                
                                this.buttonClicked = false;

                                moveResourcePiece();

                                Thread.Sleep(1000);
                                enableButtons();

                                currentPlayer = ++currentPlayer % 3;

                            }
                        }
                    }
                }
                Thread.Sleep(10);
                if (skipCount == 3)
                {
                    doneInitializing = true;

                    this.Visible = false;
                    this.Close();
                
                }
            }
        }

        private void button19_Click(object sender, EventArgs e)
        {
            loop();
        }
    }
}
