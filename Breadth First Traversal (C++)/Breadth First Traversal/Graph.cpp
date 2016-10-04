//
//  Graph.cpp
//  Breadth First Traversal
//
//  Created by Thomas Brooks on 5/4/15.
//  Copyright (c) 2015 CECS 328. All rights reserved.
//

#include "Graph.h"

Graph::Graph()
{
   
}

void Graph::scale(int size)
{
   g.resize(size);
}

void Graph::enterEdge(int x, int y)
{
   if (g[x].getValue() != x)
   {
      Node a(x);
      g[x] = a;
   }
   if (g[y].getValue() != y)
   {
      Node b(y);
      g[y] = b;
   }
   
   g[x].addAdj(y);
   g[y].addAdj(x);
   
   std::vector<int>::iterator it;
   it = std::unique (g[x].adjacencies.begin(), g[x].adjacencies.end());
   
   g[x].adjacencies.resize(std::distance(g[x].adjacencies.begin(),it));
   
   std::sort(g[x].adjacencies.begin(), g[x].adjacencies.end());
   std::sort(g[y].adjacencies.begin(), g[y].adjacencies.end());
}

void Graph::breadthFirst(int x)
{
   output.push_back(g[x].getValue());
   g[x].visit();
   
   for (int i = 0; i < g[x].getAdjCount(); i++)
   {
      queue.push_back(g[x].getAdjValue(i));
      g[g[x].getAdjValue(i)].length = 1;
      g[g[x].getAdjValue(i)].visit();
   }
   
   while (!queue.empty())
   {
      int index = queue.front();
      g[index].visit();
      output.push_back(index);
      queue.erase(queue.begin());
      
      for(int i = 0; i < g[index].getAdjCount(); i++)
      {
         int adj = g[index].adjacencies[i];
         
         if(!g[adj].visited())
         {
            g[adj].visit();
            queue.push_back(adj);
            g[adj].length = g[index].length + 1;
         }
      }
      
      //unique
      std::vector<int>::iterator it;
      it = std::unique (queue.begin(), queue.end());
      
      queue.resize(std::distance(queue.begin(),it));
   }
   
   std::vector<int>::iterator it;
   it = std::unique (output.begin(), output.end());
   
   output.resize(std::distance(output.begin(),it));
}

void Graph::display()
{
   for (int i : output)
   {
      std::cout << i << " ";
   }
   std::cout << std::endl;
}

void Graph::displayDists(int index)
{
   for (Node n : g)
   {
      std::cout << "The distance from " << n.getValue() << " is ";
      if (n.getLength() == 0)
      {
         std::cout << "INF" << std::endl;
      }
      else
      {
         std::cout << n.getLength() << std::endl;
      }
   }
   std::cout << std::endl;
}
