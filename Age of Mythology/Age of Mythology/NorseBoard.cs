using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Age_of_Mythology
{
    class NorseBoard : Board
    {
        public NorseBoard()
        {
            Random r = new Random();
            int num = 0;

            for (int i = 0; i < 16; i++)
            {
                ProductionTile pt = new ProductionTile();

                num = r.Next(0, 11);
                if (num == 0 || num == 1 || num == 2 || num == 3)
                {
                    pt.type = "Mountains";
                    pt.displayPicture = Image.FromFile(@"C:\Users\Thomas\OneDrive\Projects\CSULB\AgeOfMythology\Resources\Tiles\norse\tiles\mountains1.png");
                    mountainCount++;
                }
                else if (num == 4)
                {
                    pt.type = "Desert";
                    pt.displayPicture = Image.FromFile(@"C:\Users\Thomas\OneDrive\Projects\CSULB\AgeOfMythology\Resources\Tiles\norse\tiles\desert1.png");
                    desertCount++;
                }
                else if (num == 4 || num == 5)
                {
                    pt.type = "Swamp";
                    pt.displayPicture = Image.FromFile(@"C:\Users\Thomas\OneDrive\Projects\CSULB\AgeOfMythology\Resources\Tiles\norse\tiles\swamp1.png");
                    swampCount++;
                }
                else if (num == 6 || num == 7 || num == 8)
                {
                    pt.type = "Forest";
                    pt.displayPicture = Image.FromFile(@"C:\Users\Thomas\OneDrive\Projects\CSULB\AgeOfMythology\Resources\Tiles\norse\tiles\forest1.png");
                    forestCount++;
                }
                else if (num == 9 || num == 10 || num == 11 || num == 12)
                {
                    pt.type = "Fertile";
                    pt.displayPicture = Image.FromFile(@"C:\Users\Thomas\OneDrive\Projects\CSULB\AgeOfMythology\Resources\Tiles\norse\tiles\fertile1.png");
                    fertileCount++;
                }
                else
                {
                    pt.type = "Hills";
                    pt.displayPicture = Image.FromFile(@"C:\Users\Thomas\OneDrive\Projects\CSULB\Users\Thomas\OneDrive\Projects\CSULB\AgeOfMythology\Resources\Tiles\norse\tiles\hills1.png");
                    hillCount++;
                }
                tiles[i] = pt;
            }

        }
    }
}
