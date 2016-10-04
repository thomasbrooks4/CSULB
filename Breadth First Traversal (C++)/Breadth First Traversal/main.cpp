//
//  main.cpp
//  Breadth First Traversal
//
//  Created by Thomas Brooks on 5/4/15.
//  Copyright (c) 2015 CECS 328. All rights reserved.
//

#include <iostream>
#include <sstream>
#include "Graph.h"

int main(int argc, const char * argv[]) {
   bool running = true;
   std::string userInput;
   
   while (running)
   {
      int size = 1;
      Graph g;
      g.scale(size + 1);
      
      std::cout << "How many edges do you want to add?" << std::endl;
      std::getline(std::cin, userInput);
      std::cout << std::endl;
      int edges = atoi(userInput.c_str());
      
      for (int i = 0; i < edges; i++)
      {
         int node1, node2;
         std::cout << "Enter an edge. Example: (a,b)" << std::endl;
         std::getline(std::cin, userInput);
         
         char temp;
         std::istringstream is(userInput);
         is >> temp >> node1 >> temp >> node2 >> temp;
         
         if (node1 > size)
         {
            size = node1;
            g.scale(size + 1);
         }
         else if (node2 > size)
         {
            size = node2;
            g.scale(size + 1);
         }
         
         g.enterEdge(node1, node2);
      }
      
      std::cout << "Where do you want to start traversing from?" << std::endl;
      std::getline(std::cin, userInput);
      std::cout << std::endl;
      int firstIndex = atoi(userInput.c_str());
      
      std::cout << "Breadth first traversal from " << firstIndex << std::endl;
      std::cout << std::endl;
      g.breadthFirst(firstIndex);
      g.display();
      std::cout << std::endl;
      
      g.displayDists(firstIndex);
      
      std::cout << "Would you like to quit?" << std::endl;
      std::cout << "1) Yes" << std::endl;
      std::cout << "2) No" << std::endl;
      std::getline(std::cin, userInput);
      std::cout << std::endl;
      int choice = atoi(userInput.c_str());
      
      if (choice == 1)
      {
         running = false;
      }
   }
   
   return 0;
}
