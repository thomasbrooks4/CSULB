//
//  Graph.h
//  Breadth First Traversal
//
//  Created by Thomas Brooks on 5/4/15.
//  Copyright (c) 2015 CECS 328. All rights reserved.
//

#ifndef __Breadth_First_Traversal__Graph__
#define __Breadth_First_Traversal__Graph__

#include <stdio.h>
#include <iostream>
#include <vector>
#include <algorithm>
#include "Node.h"

class Graph
{
private:
   std::vector<Node> g;
   std::vector<int> queue, output, distances;
   
public:
   Graph();
   
   void scale(int size);
   
   void enterEdge(int x, int y);
      
   void breadthFirst(int x);
   
   void display();
   
   void displayDists(int index);
};

#endif /* defined(__Breadth_First_Traversal__Graph__) */
