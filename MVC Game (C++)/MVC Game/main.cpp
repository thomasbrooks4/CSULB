#include "GameExceptions.h"
#include "GameBoard.h"
#include "GameMove.h"
#include "GameView.h"
#include "OthelloBoard.h"
#include "OthelloView.h"
#include "OthelloMove.h"
#include "TicTacToeBoard.h"
#include "TicTacToeMove.h"
#include "TicTacToeView.h"
#include <iostream>
#include <string>
#include <sstream>
#include <vector>
#include <iterator>

using namespace std;


int main(int argc, char* argv[]) {
   // Initialization
   GameBoard *board; // the state of the game board
   GameView *v; // a View for outputting the board via operator<<
   string userInput; // a string to hold the user's command choice
   vector<GameMove *> possMoves; // a holder for possible moves
   bool moveValidated  = false;
   
   cout << " What game do you want to play? 1) Othello; 2)Tic Tac Toe;" << endl;
   int choice;
   cin >> choice;
   cin.get();
   
   if (choice == 1) {
      board = new OthelloBoard();
      v = new OthelloView(board);
   }
   else {
      board = new TicTacToeBoard();
      v = new TicTacToeView(board);
   }
   
   // Main loop
   do {
      // Print the game board
      
      // Print all possible moves
      
      // Ask to input a command
      
      // Command loop:
      // move (r,c)
      // undo n
      // showValue
      // showHistory
      // quit
      
      int count = 0;
      
      v -> PrintBoard(cout);
      cout << endl;
      
      if (board -> GetNextPlayer() == 1) {
         if (choice == 1)
            cout << "Black";
         else
            cout << "X";
      }
      else{
         if (choice == 1)
            cout << "White";
         else
            cout << "0";
      }
      cout <<"'s move" << endl;
      
      board -> GetPossibleMoves(&possMoves);
      cout << "Possible moves:" << endl;
      
      if (possMoves.size() == 0)
         cout << "pass" << endl;
      else {
         count = 0;
         
         for (GameMove *m : possMoves){
            count++;
            if (count % 13 == 0)
               cout << endl;
            cout << (string)*m << " ";
            delete m;
         }
         cout << endl;
      }
      possMoves.clear();
      
      do {
         moveValidated = false;
         
         cout << "Enter a command: ";
         getline(cin, userInput);
         cout << endl;
         
         try {
            if (userInput.substr(0, 5) == "move ") {
               GameMove *move = board -> CreateMove();
               *move = userInput.substr(5, userInput.length());
               
               board -> GetPossibleMoves(&possMoves);
               
               if (possMoves.size() != 0) {
                  for (GameMove *m : possMoves) {
                     if (*move == (*m))
                        moveValidated = true;
                     delete m;
                  }
               }
               else
                  if ((string)*move == "pass")
                     moveValidated = true;
               possMoves.clear();
               
               if (moveValidated) {
                  board -> ApplyMove(move);
                  delete move;
               }
               else {
                  delete move;
                  throw GameException("Invalid move. Please try again.");
               }
            }
            else if (userInput.substr(0, 5) == "undo ") {
               int undo;
               istringstream is(userInput.substr(5, userInput.length()));
               
               if(is >> undo) {
                  for (int numOfUndos = undo; numOfUndos > 0; numOfUndos--) {
                     board -> UndoLastMove();
                  }
                  moveValidated = true;
               }
               else
                  throw GameException("Invalid move. Please try again.");
            }
            else if (userInput.substr() == "showValue") {
               cout << "Board value: " << board -> GetValue()
               << endl;
            }
            else if (userInput.substr() == "showHistory") {
               vector <GameMove *> history = *board -> GetMoveHistory();
               count = board -> GetNextPlayer();
               
               for (vector<GameMove *>::const_reverse_iterator iter =
                    history.rbegin(); iter != history.rend(); ++iter) {
                  count *= -1;
                  if (count == 1) {
                     if (choice == 1)
                        cout << "Black: ";
                     else
                        cout << "X: ";
                  }
                  else {
                     if (choice == 1)
                        cout << "White: ";
                     else
                        cout << "0: ";
                  }
                  cout << (string)**iter << " " << endl;
               }
            }
            else if (userInput.substr() == "quit")
               goto quit;
            else
               throw GameException("Invalid move. Please try again.");
         }
         catch (exception &e) {
            cout << "/-----------------------------/" << endl;
            cout << e.what() << endl;
            cout << "/-----------------------------/" << endl;
         }
      } while (!moveValidated);
   } while (!(board -> IsFinished()));
   
quit:
   for (GameMove *m : *board -> GetMoveHistory())
      delete m;
   
   if (choice == 2)
      v -> PrintBoard(cout);
   cout << endl;
   
   if(board -> GetValue() > 0) {
      if (choice == 1)
         cout << "Black wins!" << endl;
      else
         cout << "X wins!" << endl;
   }
   else if(board -> GetValue() < 0) {
      if (choice == 1)
         cout << "White wins!" << endl;
      else
         cout << "0 wins!" << endl;
   }
   else
      cout << "We have a tie!" << endl;
   
   cout << endl;
   cout << "Thanks for playing!" << endl;
}