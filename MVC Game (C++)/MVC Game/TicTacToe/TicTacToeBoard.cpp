#include "TicTacToeBoard.h"
#include "GameExceptions.h"
#include <iostream>
#include <vector>

using namespace std;

TicTacToeBoard::TicTacToeBoard() {
   mValue = 0;
   mNextPlayer = X;
}

void TicTacToeBoard::GetPossibleMoves(vector<GameMove *> *list) const {
   for (int x = 0; x < BOARD_SIZE; x++)
      for (int y = 0; y < BOARD_SIZE; y++)
         if (mBoard[x][y] == EMPTY) {
            TicTacToeMove *possible = (TicTacToeMove*)CreateMove();
            possible -> mRow = x;
            possible -> mCol = y;
            list->push_back(possible);
         }
}

void TicTacToeBoard::ApplyMove(GameMove *m) {
   TicTacToeMove *move = (TicTacToeMove*)m;
   mValue = 0;
   
   if (move->IsPass())
      mPassCount++;
   else
      mBoard[move -> mRow][move -> mCol] = mNextPlayer;
   
   TicTacToeMove *storeMove = (TicTacToeMove*)CreateMove();
   storeMove -> mRow = move -> mRow;
   storeMove -> mCol = move -> mCol;
   mHistory.push_back(storeMove);
   
   for (int x = 0; x < BOARD_SIZE; x++)
      for (int y = 0; y < BOARD_SIZE; y++)
         if (mBoard[x][y] != EMPTY && mBoard[x][y] == mNextPlayer)
            for (int rowDelta = -1; rowDelta <= 1; rowDelta++)
               for (int colDelta = -1; colDelta <= 1; colDelta++)
                  if (mBoard[x + rowDelta][y + colDelta] == mNextPlayer &&
                   !(rowDelta == 0 && colDelta == 0) )
                     if ((mBoard[x + (rowDelta * 2)][y + (colDelta * 2)] ==
                      mNextPlayer) && (InBounds(x + (rowDelta * 2), y +
                                                (colDelta * 2)))) {
                        mValue += mNextPlayer;
                     }
   mNextPlayer *= -1;
}

void TicTacToeBoard::UndoLastMove() {
   if (GetMoveCount() == 0)
      throw TicTacToeException("      Cannot undo anymore.");
   
   TicTacToeMove *undo = (TicTacToeMove*)mHistory.back();
   mBoard[undo->mRow][undo->mCol] = EMPTY;
   
   delete undo;
   mHistory.pop_back();
   mNextPlayer *= -1;
}
