#include "TicTacToeMove.h"
#include <sstream>
#include <string>

using namespace std;

int TicTacToeMove::mOutstanding = 0;

TicTacToeMove::TicTacToeMove()
: mRow(-1), mCol(-1) {
   
}

TicTacToeMove::TicTacToeMove(int row, int col)
: mRow(row), mCol(col) {
   
}

TicTacToeMove::TicTacToeMove(const TicTacToeMove &other)
: mRow(other.mRow), mCol(other.mCol) {
}

TicTacToeMove& TicTacToeMove::operator=(const TicTacToeMove &other) {
   mRow = other.mRow;
   mCol = other.mCol;
   
   return *this;
}

GameMove& TicTacToeMove::operator=(const string &rhs) {
   if (rhs != "pass") {
      char temp;
      istringstream is(rhs);
      is >> temp >> mRow >> temp >> mCol >> temp;
   }
   
   return *this;
}

bool TicTacToeMove::Equals(const GameMove &other) const {
   return ((string)*this == (string)other);
}

TicTacToeMove::operator string() const {
   ostringstream os;
   
   if (IsPass())
      os << "pass";
   else
      os << "(" << mRow << "," << mCol << ")";
   
   return os.str();
}
