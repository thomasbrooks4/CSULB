//
//  functions.h
//  Edit Distance
//
//  Created by Thomas Brooks on 5/5/15.
//  Copyright (c) 2015 CECS 328. All rights reserved.
//

#ifndef Lab10_functions_h
#define Lab10_functions_h

#include <iostream>
#include <string>
#include <vector>

int min(int x, int y, int z);
int editDist(std::string str1, std::string str2, std::vector<std::string> &output);

int min(int x, int y, int z)
{
   return x < y ? (x < z ? x : z) : (y < z ? y : z);
}

int editDist(std::string str1, std::string str2, std::vector<std::string> &output)
{
   int x = (int)str1.length();
   int y = (int)str2.length();
   
   std::vector<std::vector<int>> arr;
   arr.resize(x + 1);
   
   // Initializing column 1
   for(int i = 0; i <= x; ++i)
   {
      std::vector<int> arr2;
      arr2.resize(y + 1);
      
      arr[i] = arr2;
      arr[i][0] = i;
   }
   
   // Initializing row 1
   for(int i = 0; i <= y; ++i)
   {
      arr[0][i] = i;
   }
   
   // Building table
   for(int i = 1; i <= x; ++i)
   {
      for(int j = 1; j <= y; ++j)
      {
         int deletion = arr[i - 1][j] + 1;
         int insertion = arr[i][j - 1] + 1;
         int substitution = arr[i - 1][j - 1] + (int)(str1[i - 1] != str2[j - 1]);
         arr[i][j] = min(deletion, insertion, substitution);
      }
   }
   
   // Backtracking and adding to output
   int a = x, b = y;
   
   while (a > 0 && b > 0)
   {
      std::string temp;
      int deletion = arr[a - 1][b] + 1;
      int insertion = arr[a][b - 1] + 1;
      int substitution = arr[a - 1][b - 1] + (int)(str1[a - 1] != str2[b - 1]);
      
      int operation = min(deletion, insertion, substitution);
      
      if (operation == deletion)
      {
         temp += str1[a - 1];
         temp += " was deleted";
         output.push_back(temp);
         a -= 1;
      }
      else if (operation == insertion)
      {
         temp += str2[b - 1];
         temp += " was inserted";
         output.push_back(temp);
         b -= 1;
      }
      else if (operation == substitution)
      {
         if (str1[a - 1] != str2[b - 1])
         {
            temp += str1[a - 1];
            temp += " substituted with ";
            temp += str2[b - 1];
            output.push_back(temp);
         }
         a -= 1;
         b -= 1;
      }
   }
   
   return arr[x][y];
}

#endif
