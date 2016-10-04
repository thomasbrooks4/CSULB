#include "OthelloMove.h"
#include <sstream>
#include <string>

using namespace std;

int OthelloMove::mOutstanding = 0;

OthelloMove::OthelloMove()
: mRow(-1), mCol(-1) {
   
}

OthelloMove::OthelloMove(int row, int col)
: mRow(row), mCol(col) {
   
}

OthelloMove::OthelloMove(const OthelloMove &other)
: mRow(other.mRow), mCol(other.mCol) {
}

OthelloMove& OthelloMove::operator=(const OthelloMove &other) {
   mRow = other.mRow;
   mCol = other.mCol;
   
   return *this;
}

GameMove& OthelloMove::operator=(const string &rhs) {
   if (rhs != "pass") {
      char temp;
      istringstream is(rhs);
      is >> temp >> mRow >> temp >> mCol >> temp;
   }
   
   return *this;
}

bool OthelloMove::Equals(const GameMove &other) const {
   return ((string)*this == (string)other);
}

OthelloMove::operator string() const {
   ostringstream os;
   
   if (IsPass())
      os << "pass";
   else
      os << "(" << mRow << "," << mCol << ")";
   
   return os.str();
}
