#include "OthelloView.h"
#include <iostream>

using namespace std;

void OthelloView::PrintBoard(ostream &s) const {
   for (int currRow = 0; currRow <= BOARD_SIZE; currRow++) {
      if (currRow == 0) {
         s << "-";
         for (int topRow = 0; topRow <= BOARD_SIZE - 1; topRow++)
            s << " " << topRow;
         s << endl;
      }
      else {
         s << currRow-1;
         for (int boardCol = 0; boardCol <= BOARD_SIZE - 1; boardCol++) {
            if (mOthelloBoard -> mBoard[currRow - 1][boardCol] == 0)
               s << " .";
            else {
               if (mOthelloBoard -> mBoard[currRow - 1][boardCol] == 1)
                  s << " B";
               else
                  s << " W";
            }
         }
         s << endl;
      }
   }
}