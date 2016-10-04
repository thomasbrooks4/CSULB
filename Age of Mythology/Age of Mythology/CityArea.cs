using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Age_of_Mythology
{
    public class CityArea : Board
    {
        public Image staticBackgroundImage;

        public CityArea(char type)
        {
            if (type == 'n')
                staticBackgroundImage = Image.FromFile(@"C:\Users\Thomas\OneDrive\Projects\CSULB\AgeOfMythology\Resources\BackgroundImages\n_city.png");//fill in these imgaes
            else if (type == 'e')
                staticBackgroundImage = Image.FromFile(@"C:\Users\Thomas\OneDrive\Projects\CSULB\AgeOfMythology\Resources\BackgroundImages\e_city.png");
            else
                staticBackgroundImage = Image.FromFile(@"C:\Users\Thomas\OneDrive\Projects\CSULB\AgeOfMythology\Resources\BackgroundImages\g_city.png");

            generateCityTiles();
        }

        public Image getBackgroundImage()
        {
            return staticBackgroundImage;
        }

        public void generateCityTiles()
        {
            for (int i = 0; i < 16; i++)
            {
                tiles[i] = new CityTile();
            }
        }
    }
}
