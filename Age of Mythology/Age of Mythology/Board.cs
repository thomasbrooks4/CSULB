using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Age_of_Mythology
{
    public class Board
    {
        public int hillCount = 0;
        public int mountainCount = 0;
        public int swampCount = 0;
        public int fertileCount = 0;
        public int desertCount = 0;
        public int forestCount = 0;

        public Tile[] tiles = new Tile[16];
    }
}
