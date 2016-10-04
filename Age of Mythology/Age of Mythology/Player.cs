using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Age_of_Mythology
{
    public class Player
    {
        public Board resourceArea;
        public CityArea cityArea;
        public List<BattleUnits> army = new List<BattleUnits>();
        public char culture;
        public int age = 1;
        // 0 = favor
        // 1 = food
        // 2 = gold
        // 3 = wood
        public int[] resourceCubes = new int[4];
        public List<ResourcePiece> resourcePiecesList = new List<ResourcePiece>();
        public List<CityPiece> cityPiecesList = new List<CityPiece>();

        /// <summary>
        /// NON PLAYER CONSTUCTOR
        /// </summary>
        /// <param name="boardChoice"> what kind of board the player chose</param>
        public Player(char boardChoice)
        {
            culture = boardChoice;
            if (boardChoice == 'n')
                resourceArea = new NorseBoard();
            else if (boardChoice == 'e')
                resourceArea = new EgyptianBoard();
            else
                resourceArea = new GreekBoard();

            for (int i = 0; i < 4; i++)
            {
                resourceCubes[i] = 5;
            }
                cityArea = new CityArea(boardChoice);
        }

        
        /// <summary>
        /// AI PLAYER CONSTUCTOR
        /// </summary>
        public Player()
        {
            Random r = new Random();
            int AIChoice = r.Next(0, 2);

            if (AIChoice == 0)
            {
                resourceArea = new NorseBoard();
                cityArea = new CityArea('n');
            }
            else if (AIChoice == 1)
            {
                resourceArea = new EgyptianBoard();
                cityArea = new CityArea('e');
            }
            else
            {
                resourceArea = new GreekBoard();
                cityArea = new CityArea('g');
            }

            for (int i = 0; i < 4; i++)
            {
                resourceCubes[i] = 5;
            }
            
        }

        public void incrementAge()
        {
            this.age++;
        }

    }
}
