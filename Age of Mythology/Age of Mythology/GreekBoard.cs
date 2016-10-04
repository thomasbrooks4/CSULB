using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Age_of_Mythology
{
    class GreekBoard : Board
    {
        
        public GreekBoard()
        {
            Random r = new Random();
            int num = 0;

            for (int i = 0; i < 16; i++)
            {
                ProductionTile pt = new ProductionTile();

                num = r.Next(0, 15);
                if (num == 0 || num == 1 || num == 2 || num == 3)
                {
                    pt.type = "Hills";
                    pt.displayPicture = Image.FromFile(@"C:\AgeOfMythology\Resources\Tiles\norse\tiles\hills1.png");
                    hillCount++;
                }
                else if (num == 4)
                {
                    pt.type = "Desert";
                    pt.displayPicture = Image.FromFile(@"C:\AgeOfMythology\Resources\Tiles\norse\tiles\desert1.png");
                    desertCount++;
                }
                else if (num == 5 || num == 6)
                {
                    pt.type = "Swamp";
                    pt.displayPicture = Image.FromFile(@"C:\AgeOfMythology\Resources\Tiles\norse\tiles\swamp1.png");
                    swampCount++;
                }
                else if (num == 7 || num == 8 || num == 9)
                {
                    pt.type = "Forest";
                    pt.displayPicture = Image.FromFile(@"C:\AgeOfMythology\Resources\Tiles\norse\tiles\forest1.png");
                    forestCount++;
                }
                else if (num == 10 || num == 11 || num == 12 || num == 13)
                {
                    pt.type = "Fertile";
                    pt.displayPicture = Image.FromFile(@"C:\AgeOfMythology\Resources\Tiles\norse\tiles\fertile1.png");
                    fertileCount++;
                }
                else
                {
                    pt.type = "Mountains";
                    pt.displayPicture = Image.FromFile(@"C:\AgeOfMythology\Resources\Tiles\norse\tiles\mountains1.png");
                    mountainCount++;
                }
                tiles[i] = pt;
            }
        }
    }
}
