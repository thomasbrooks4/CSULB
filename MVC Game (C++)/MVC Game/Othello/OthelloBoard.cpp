#include "OthelloBoard.h"
#include "GameExceptions.h"
#include <iostream>
#include <vector>
#define INITIAL_POSITION 3

using namespace std;

OthelloBoard::OthelloBoard() {
   mBoard[INITIAL_POSITION][INITIAL_POSITION + 1] = BLACK;
   mBoard[INITIAL_POSITION + 1][INITIAL_POSITION] = BLACK;
   mBoard[INITIAL_POSITION][INITIAL_POSITION] = WHITE;
   mBoard[INITIAL_POSITION + 1][INITIAL_POSITION + 1] = WHITE;
   
   mValue = 0;
   mNextPlayer = BLACK;
   cout << "1";
   cout << mNextPlayer;
   cout << "2" << endl;
}

void OthelloBoard::GetPossibleMoves(vector<GameMove *> *list) const {
   bool possFlip;
   possFlip = false;
   int count = 0;
   OthelloMove *checker = (OthelloMove*)CreateMove();
   
   for (int x = 0; x < BOARD_SIZE; x++) {
      checker -> mRow = x;
      for (int y = 0; y < BOARD_SIZE; y++) {
         checker -> mCol = y;
         if (mBoard[x][y] == EMPTY) {
            for (int rowD = -1; rowD <= 1; rowD++) {
               for (int colD = -1; colD <= 1; colD++) {
                  if (mBoard[checker -> mRow + rowD][checker -> mCol + colD] ==
                      -mNextPlayer && !(rowD == 0 && colD == 0)){
                     count = 1;
                     while (!possFlip) {
                        if (!InBounds(checker -> mRow + (rowD * count), checker
                         -> mCol + (colD * count)) || mBoard[checker -> mRow +
                         (rowD * count)][checker -> mCol + (colD * count)] ==
                            EMPTY) {
                           count = 0;
                           break;
                        }
                        
                        if (mBoard[checker -> mRow + (rowD * count)][checker ->
                         mCol + (colD * count)] == mNextPlayer)
                           possFlip = true;
                        else
                           count++;
                     }
                     if (possFlip)
                        break;
                  }
               }
               if (possFlip)
                  break;
            }
         }
         if (possFlip) {
            OthelloMove *possible = (OthelloMove*)CreateMove();
            possible -> mRow = checker -> mRow;
            possible -> mCol = checker -> mCol;
            list -> push_back(possible);
         }
         possFlip = false;
      }
   }
   delete checker;
}

void OthelloBoard::ApplyMove(GameMove *m) {
   OthelloMove *move = (OthelloMove*)m;
   if (move->IsPass())
      mPassCount++;
   else {
      mPassCount = 0;
      OthelloMove::FlipSet flips(0, -1, -1);
      mBoard[move -> mRow][move -> mCol] = mNextPlayer;
      mValue += mNextPlayer;
      bool foundFriendly = false;
      
      for (flips.rowDelta = -1; flips.rowDelta <= 1; flips.rowDelta++)
         for (flips.colDelta = -1; flips.colDelta <= 1; flips.colDelta++)
            if (mBoard[move -> mRow + flips.rowDelta][move -> mCol +
             flips.colDelta] == -mNextPlayer) {
               flips.switched = 1;
               
               while (!foundFriendly) {
                  if (!InBounds(move -> mRow + (flips.rowDelta * flips.switched)
                   , move -> mCol + (flips.colDelta * flips.switched)) || mBoard
                   [move -> mRow + (flips.rowDelta * flips.switched)][move ->
                   mCol + (flips.colDelta * flips.switched)] == EMPTY) {
                         flips.switched = 0;
                         break;
                      }
                  
                  if (mBoard[move -> mRow + (flips.rowDelta * flips.switched)]
                      [move -> mCol + (flips.colDelta * flips.switched)] ==
                      mNextPlayer) {
                     foundFriendly = true;
                     flips.switched--;
                     move -> AddFlipSet(flips);
                  }
                  else
                     flips.switched++;
               }
               foundFriendly = false;
               for (int num = flips.switched; num > 0; num--) {
                  mBoard[move -> mRow + (flips.rowDelta * num)][move -> mCol +
                   (flips.colDelta * num) ] = mNextPlayer;
                  mValue += mNextPlayer * 2;
               }
            }
   }
   OthelloMove *storeMove = (OthelloMove*)CreateMove();
   storeMove -> mRow = move -> mRow;
   storeMove -> mCol = move -> mCol;
   storeMove -> mFlips = move -> mFlips;
   mHistory.push_back(storeMove);
   
   mNextPlayer *= -1;
}

void OthelloBoard::UndoLastMove() {
   if (GetMoveCount() == 0)
      throw OthelloException("      Cannot undo anymore.");
   
   OthelloMove *undo = (OthelloMove*)mHistory.back();
   OthelloMove::FlipSet undoFlip(0, -1, -1);
   
   mBoard[undo->mRow][undo->mCol] = EMPTY;
   mValue += mNextPlayer;
   
   for (vector<OthelloMove::FlipSet>::reverse_iterator iter =
        undo->mFlips.rbegin(); iter != undo->mFlips.rend(); ++iter) {
      undoFlip = *iter;
      
      for (int num = undoFlip.switched; num > 0; num--) {
         mBoard[undo -> mRow + (undoFlip.rowDelta * num)][undo -> mCol +
          (undoFlip.colDelta * num) ] = mNextPlayer;
         mValue += mNextPlayer * 2;
      }
   }
   delete undo;
   mHistory.pop_back();
   mNextPlayer *= -1;
}
