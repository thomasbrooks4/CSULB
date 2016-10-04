using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Age_of_Mythology
{
    public class CityPiece
    {
        public string buildingType;
        public string picture;
        public int[] cost;

        public CityPiece(string type, string img, int[] c)
        {
            this.buildingType = type;
            this.picture = img;
            cost = c;
        }
    }
}
