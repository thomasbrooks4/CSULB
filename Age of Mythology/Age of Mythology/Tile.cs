using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Age_of_Mythology
{
    public class Tile
    {
        public Image displayPicture;
        public Image overlayPicture;
        public string type;
        public bool isFilled = false;
        public ResourcePiece rPiece;
        public CityPiece cPiece;
    }
}
