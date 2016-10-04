using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Age_of_Mythology
{
    public class BattleUnits
    {
        public int[] cost;
        public string type;
        public int diceAmount;
        public string picture;

        public BattleUnits(int[] c, string t, int d, string fp)
        {
            cost = c;
            type = t;
            diceAmount = d;
            picture = fp;
        }

        public BattleUnits(string t)
        {
            this.type = t;
        }
    }
}
