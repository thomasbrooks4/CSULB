//
//  main.c
//  Lab2
//
//  Created by Thomas Brooks on 9/20/16.
//  Copyright © 2016 CECS 424. All rights reserved.
//

#include <stdio.h>

struct Block {
   int block_size;   // # of bytes in the data section
   struct Block *next_block;  // Next block that the block is pointing to
};

int overhead_size = sizeof(struct Block);
int void_size = sizeof(void*);

struct Block *free_head;

void my_initialize_heap(int size) {
   free_head = malloc(size);
   free_head->block_size = size - overhead_size;
   free_head->next_block = NULL;
}

void* my_alloc(int size) {
   // Create iterator pointers to walk the free list and have a follow iterator
   // in case we need to redirect pointers
   struct Block *block_itr = free_head;
   struct Block *follow_itr = free_head;
   
   if (size % void_size != 0)
      size += void_size - (size % void_size);
   
   // While we still have blocks in the free list
   while (block_itr != NULL) {
      // If the block size is greater than the size we want to allocate
      if (block_itr->block_size >= size) {
         // If there is room remaining to fit another block after allocation
         // we have to split
         if (block_itr->block_size - size > void_size + overhead_size) {
            // Creates a new block at the back of the block location
            struct Block *new_block = block_itr + (block_itr->block_size - size
               + overhead_size);
            // Initialize new block
            new_block->block_size = size;
            new_block->next_block = NULL;
            // Resize current block after allocating new block
            block_itr->block_size -= new_block->block_size + overhead_size;
            
            return new_block + overhead_size;
         }
         // If we can't split the block
         else {
            // If the block is the free head, the next block will be the head
            if (block_itr == free_head){
               free_head = free_head->next_block;
               
               return block_itr + overhead_size;
            }
            // If the block isn't the free head, we need to use the follow
            // iterator to point the previous block to the current block's
            // next block, then change the current's block next block to NULL
            else {
             follow_itr->next_block = block_itr->next_block;
             block_itr->next_block = NULL;
             
             return block_itr + overhead_size;
            }
         }
      }
      
      // If the current block isn't at the head iterate both
      if (block_itr != free_head) {
         block_itr = block_itr->next_block;
         follow_itr = follow_itr->next_block;
      }
      // If the current block is at the head
      else
         block_itr = block_itr->next_block;
   }
   
   return NULL;
}

void my_free(void *data) {
   // Create a temporary block to store the free head block
   struct Block *temp_block = free_head;
   // Set freehead to the next block of what should be deallocated
   free_head = ((struct Block *)data - overhead_size)->next_block;
   // Set the temporary's next block to the start of the deallocated space
   temp_block->next_block = ((struct Block *)data - overhead_size);
}

int main(int argc, const char * argv[]) {
//   my_initialize_heap(1000);
//   
//   // Test 1
//   void* a = my_alloc(sizeof(int));
//   print("%p\n", a);
//   
//   my_free(a);
//   
//   void* b = my_alloc(sizeof(int));
//   print("%p\n", b);
//   
//   // Test 2
//   void* c = my_alloc(sizeof(int));
//   print("%p\n", c);
//   
//   void* d = my_alloc(sizeof(int));
//   print("%p\n", d);
//   
//   // Test 3
//   void* e = my_alloc(sizeof(int));
//   print("%p\n", e);
//   
//   void* f = my_alloc(sizeof(int));
//   print("%p\n", f);
//   
//   void* g = my_alloc(sizeof(int));
//   print("%p\n", g);
//   
//   my_free(f);
//   
//   void* h = my_alloc(sizeof(double));
//   print("%p\n", h);
//   
//   void* i = my_alloc(sizeof(int));
//   print("%p\n", i);
//   
//   // Test 4
//   void* j = my_alloc(sizeof(char));
//   print("%p\n", j);
//   
//   void* k = my_alloc(sizeof(int));
//   print("%p\n", k);
//   
//   // Test 5
//   void* l = my_alloc(sizeof(int[100]));
//   print("%p\n", l);
//   
//   void* m = my_alloc(sizeof(int));
//   print("%p\n", m);
   
   int n, varSum;
   int sum = 0;
   float mean, var, stdDev;
   
   printf("Enter in the amount of intergers for the array:\n");
   scanf("%d", &n);
   printf("\n");
   
   // Initializae heap to size specified plus overhead
   my_initialize_heap(sizeof(int[n]) + overhead_size);
   
   // Allocate for each integer and add to the sum
   for (int i = 1; i <= n; i++) {
      int input;
      printf("Enter an interger:\n");
      scanf("%d", &input);
      printf("\n");
      sum += input;
      my_alloc(input);
   }
   
   // Find the mean of the integers
   mean = sum / (float)n;
   
   // Calculate the sum within the variance
   for (int i = 1; i <= n; i++) {
      varSum += pow((i - mean), 2);
   }
   
   // Finish calculating the variance
   var = varSum / (float)n;
   // Finish calculating the standard deviation
   stdDev = sqrt(var);
   
   printf("For %d integers, the standard deviation is: %f", n, stdDev);
   printf("\n");
   
   return 0;
}
