//
//  main.cpp
//  Edit Distance
//
//  Created by Thomas Brooks on 5/5/15.
//  Copyright (c) 2015 CECS 328. All rights reserved.
//

#include <iostream>
#include <string>
#include "functions.h"

int main(int argc, const char * argv[]) {
   bool running = true;
   std::string str1, str2, userInput;
   std::vector<std::string> output;
   
   output.clear();
   
   while (running)
   {
      std::cout << "Enter in the first string." << std::endl;
      std::getline(std::cin, str1);
      std::cout << std::endl;
      
      std::cout << "Enter in the second string." << std::endl;
      std::getline(std::cin, str2);
      std::cout << std::endl;
      
      int count = editDist(str1, str2, output);
      
      std::cout << "The edit distance between the two strings is: " << count << std::endl;
      std::cout << std::endl;
      
      std::cout << "The edits made to get " << str1 << " to " << str2 << " were:" << std::endl;
      std::string temp;
      for (int i = (int)output.size() - 1; i >= 0; i--)
      {
         temp += output[i];
         temp += ", ";
      }
      temp.pop_back();
      temp.pop_back();
      std::cout << temp << std::endl;
      std::cout << std::endl;
      
      std::cout << "Would you like to quit?" << std::endl;
      std::cout << "1) Yes" << std::endl;
      std::cout << "2) No" << std::endl;
      std::getline(std::cin, userInput);
      int choice = atoi(userInput.c_str());
      std::cout << std::endl;
      
      if (choice == 1)
      {
         running = false;
      }
      else
         output.clear();
   }
   
   
   return 0;
}
