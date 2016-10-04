//
//  Node.cpp
//  Breadth First Traversal
//
//  Created by Thomas Brooks on 5/4/15.
//  Copyright (c) 2015 CECS 328. All rights reserved.
//

#include "Node.h"

Node::Node()
{
   
}

Node::Node(int x)
   :value(x)
{
   v = false;
   length = 0;
}

void Node::addAdj(int x)
{
   adjacencies.push_back(x);
}

void Node::addLength()
{
   length++;
}

int Node::getAdjValue(int i)
{
   return adjacencies[i];
}

int Node::getAdjCount()
{
   return (int)adjacencies.size();
}

int Node::getLength()
{
   return length;
}

void Node::visit()
{
   v = true;
}

bool Node::visited()
{
   return v;
}

int Node::getValue()
{
   return value;
}
