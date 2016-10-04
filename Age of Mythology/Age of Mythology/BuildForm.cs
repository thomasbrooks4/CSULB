using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Age_of_Mythology
{
    public partial class BuildForm : Form
    {
        Player player;
        List<CityPiece> cMList;
        public bool buildFormDone = false;
        int buildCount = 0;
        int maxBuildCount;
        Player[] pArray;
        List<String> buildingsToDestroy = new List<String>();

        public BuildForm(ref Player p, List<CityPiece> cityMasterList)
        {
            InitializeComponent();
            player = p;
            cMList = cityMasterList;
            maxBuildCount = 1;
            this.Size = new Size(203, 165);
        }

        public BuildForm(ref Player p, List<CityPiece> cityMasterList, bool godModeActivated, ref Player[] players)
        {

            InitializeComponent();
            player = p;
            cMList = cityMasterList;
            maxBuildCount = 1;
            

            pArray = players;
            if (player.culture != 'g')
                this.Size = new Size(379, 165);

            if (p.culture == 'e')
            {
                maxBuildCount = 3;
                getAllBuildings();
            }
            else if (p.culture == 'g')
            {
                maxBuildCount = 3;
                for (int i = 0; i < player.cityArea.tiles.Length; i++)
                {
                    if (!player.cityArea.tiles[i].isFilled)
                    {
                        CityPiece cp = (cMList.Find(CityPiece => CityPiece.buildingType.Equals("House")));
                        player.cityArea.tiles[i].overlayPicture = ResizeImage(Image.FromFile(cp.picture), 50, 50);
                        player.cityArea.tiles[i].isFilled = true;
                        player.cityPiecesList.Add(cp);

                        break;
                    }
                }
            }
            else //n
            {
                maxBuildCount = 4;
            }
        }
        private void buildButton_Click(object sender, EventArgs e)
        {
            //add the building to the city area
            build();
            
            //subtract cost from players cubes

            //re-calculate which buildings can be built
            
        }

        private void doneButton_Click(object sender, EventArgs e)
        {
            buildFormDone = true;
            this.Close();
        }

        private void BuildForm_Load(object sender, EventArgs e)
        {
            string[] comboBoxItems = getPossibleBuildings(player.resourceCubes).ToArray();
            comboBox1.Items.AddRange(comboBoxItems);
        }

        public List<string> getPossibleBuildings(int[] cubes)
        {
            List<String> whatBuildingsCanBeBuilt = new List<String>();
            comboBox1.Items.Clear();
            comboBox1.SelectedText = "";

            foreach (CityPiece cPiece in cMList)
            {
                if (cubes[0] >= cPiece.cost[0] && cubes[1] >= cPiece.cost[1] && cubes[2] >= cPiece.cost[2] && cubes[3] >= cPiece.cost[3])
                {
                    int houseCount = player.cityPiecesList.FindAll(CityPiece => CityPiece.buildingType.Equals("House")).Count();
                    if (cPiece.buildingType.Equals("House") && houseCount < 10)
                    {
                        whatBuildingsCanBeBuilt.Add(cPiece.buildingType);
                    }
                    else if (!player.cityPiecesList.Contains(cPiece))
                    {
                        if (cPiece.buildingType.Equals("Wonder") && player.age == 4)
                            whatBuildingsCanBeBuilt.Add(cPiece.buildingType);
                        else if (!cPiece.buildingType.Equals("Wonder"))
                            whatBuildingsCanBeBuilt.Add(cPiece.buildingType);
                    }
                    else
                    {
                        //do nothing
                    }

                }
            }

            if (whatBuildingsCanBeBuilt.Count() > 0)
                return whatBuildingsCanBeBuilt;
            else
            {
                buildButton.Enabled = false;
                MessageBox.Show("Nothing more can be built, please press done.");
                return whatBuildingsCanBeBuilt;
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

        public void getAllBuildings()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int k = 0; k < pArray[i].cityPiecesList.Count(); k++)
                {
                    string btd = i + "," + pArray[i].cityPiecesList[k].buildingType + "," + k;
                    buildingsToDestroy.Add(btd);
                }
            }
            
            foreach (String s in buildingsToDestroy)
            {
                string[] split = Regex.Split(",",s);
                comboBox2.Items.Add("Player: " + split[0] + 1 + " - " + split[1]);
            }
        }

        //destroy building
        private void button1_Click(object sender, EventArgs e)
        {

            if (comboBox2.SelectedText != "")
            {
                string[] split = Regex.Split(",",buildingsToDestroy[comboBox2.SelectedIndex]);
                int pInd = Convert.ToInt16(split[0]);
                int bInd = Convert.ToInt16(split[2]);

                pArray[pInd].cityPiecesList.RemoveAt(bInd);
                pArray[pInd].cityArea.tiles[bInd].overlayPicture = null;
                pArray[pInd].cityArea.tiles[bInd].isFilled = false;
                
            }
            button1.Enabled = false;
        }

        public void build()
        {
            for (int i = 0; i < player.cityArea.tiles.Length; i++)
            {
                if (!player.cityArea.tiles[i].isFilled)
                {
                    CityPiece cp = (cMList.Find(CityPiece => CityPiece.buildingType.Equals(comboBox1.Text)));
                    player.cityArea.tiles[i].overlayPicture = ResizeImage(Image.FromFile(cp.picture), 50, 50);
                    player.cityArea.tiles[i].isFilled = true;
                    player.cityPiecesList.Add(cp);
                    player.resourceCubes[0] -= cp.cost[0];
                    player.resourceCubes[1] -= cp.cost[1];
                    player.resourceCubes[2] -= cp.cost[2];
                    player.resourceCubes[3] -= cp.cost[3];
                    string[] comboBoxItems = getPossibleBuildings(player.resourceCubes).ToArray();
                    comboBox1.Items.AddRange(comboBoxItems);
                    buildCount++;

                    if (buildCount >= maxBuildCount)
                    {
                        buildFormDone = true;
                        this.Close();
                    }
                    break;
                }
                else
                {
                    //do nothing
                }
            }
        }
    }
}
