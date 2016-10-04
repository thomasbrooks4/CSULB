using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;


namespace Age_of_Mythology
{
    public class ResourcePiece
    {
        public string terrainType;
        public string resourceType;
        public int resourceAmount;
        public string picture;

        public ResourcePiece(string terrainT, string resourceT, int resourceA, string filePath)
        {
            this.terrainType = terrainT;
            this.resourceType = resourceT;
            this.resourceAmount = resourceA;
            this.picture = filePath;
        }
    }
}
