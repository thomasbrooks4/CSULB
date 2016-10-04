//
//  Node.h
//  Breadth First Traversal
//
//  Created by Thomas Brooks on 5/4/15.
//  Copyright (c) 2015 CECS 328. All rights reserved.
//

#ifndef __Breadth_First_Traversal__Node__
#define __Breadth_First_Traversal__Node__

#include <stdio.h>
#include <vector>

class Node
{
private:
   bool v;
   int value;
   
public:
   int length;
   std::vector<int> adjacencies;
   
   Node();
   
   Node(int x);
   
   void addAdj(int x);
   
   void addLength();
   
   int getAdjValue(int i);
   
   int getAdjCount();
   
   int getLength();
   
   void visit();
   
   bool visited();
   
   int getValue();
};

#endif /* defined(__Breadth_First_Traversal__Node__) */
