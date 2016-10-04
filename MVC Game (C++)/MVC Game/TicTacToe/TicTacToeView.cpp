#include "TicTacToeView.h"
#include <iostream>

using namespace std;

void TicTacToeView::PrintBoard(ostream &s) const {
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
            if (mTicTacToeBoard -> mBoard[currRow - 1][boardCol] == 0)
               s << " .";
            else {
               if (mTicTacToeBoard -> mBoard[currRow - 1][boardCol] == 1)
                  s << " X";
               else
                  s << " O";
            }
         }
         s << endl;
      }
   }
}