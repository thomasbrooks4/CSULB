using System;
using System.Collections.Generic;

namespace Lab3 {
   public class HashMap <KeyType, ValueType> {
      public const int TABLE_SIZE = 509;

      private class Node {
         public KeyType mKey;
         public ValueType mValue;
         public Node mNext;
      }

      private Node[] mTable;
      private int mCount;

      public HashMap(int tableSize) {
         mTable = new Node[tableSize];
      }

      public int count() {
         return mCount;
      }

      public bool containsKey(KeyType key) {
         int hashCode = Math.Abs (key.GetHashCode ()) % TABLE_SIZE;

         if (mTable [hashCode] != null) {
            Node next = mTable[hashCode];

            // While the next's mNext does not equals null (has nNext) set the next node to mNext
            while (next.mNext != null) {
               // If the next key is equal to the current key
               if (next.mKey.Equals (key)) {
                  return true;
               }

               next = next.mNext;
            }
         }

         return false;
      }

      public List<KeyType> keySet() {
         List<KeyType> keys = new List<KeyType>();

         for (int i = 0; i < TABLE_SIZE; i++) {
            if (mTable [i] != null) {
               Node next = mTable [i];

               while (next.mNext != null) {
                  keys.Add (next.mKey);

                  next = next.mNext;
               }
               keys.Add (next.mKey);
            }
         }

         return keys;
      }

      /// <summary>
      /// Inserts given key/value into hash table; Options:
      /// 1) Index at hash code is empty
      ///   a) Insert new node
      /// 2) Index at hash code has an element
      ///   a) Search for last node
      ///      i) While searching if key is equal to current node key then update value
      ///      ii) If we reach the end of the table, set current end node's next value to new node
      /// </summary>
      /// <param name="key">Key.</param>
      /// <param name="value">Value.</param>
      public void insert(KeyType key, ValueType value) {
         int hashCode = Math.Abs(key.GetHashCode ()) % TABLE_SIZE;

         Node n = new Node ();
         n.mKey = key;
         n.mValue = value;

         // If the index at hash code is empty create a new node
         if (mTable [hashCode] == null)
            mTable [hashCode] = n;
         // Table is not empty, search for last element
         else {
            // Follow Node.mNext() till it equals null

            // Don't set current node to new node if value is just updated
            bool updated = false;

            // Set initial search node to first node
            Node next = mTable[hashCode];

            // While the next's mNext does not equals null (has nNext) set the next node to mNext
            while (next.mNext != null) {
               // If the next key is equal to key we are trying to insert
               if (next.mKey.Equals (key)) {
                  next.mValue = value;
                  updated = true;
                  break;
               }

               next = next.mNext;
            }
            if (next.mKey.Equals (key)) {
               next.mValue = value;
               updated = true;
            }
            // next is now the last node, insert n to the beginning
            if (!updated) {
               next = mTable[hashCode];
               mTable[hashCode] = n;
               n.mNext = next;
            }
         }
         mCount++;
      }

      /// <summary>
      /// Find the value of a specified key in the map
      /// </summary>
      /// <param name="key">Key.</param>
      public ValueType find(KeyType key) {
         int hashCode = Math.Abs(key.GetHashCode()) % TABLE_SIZE;

         // If the index in the hash map is not empty
         if (mTable [hashCode] != null) {
            Node next = mTable [hashCode];
            while (next.mNext != null) {
               // If the next key is equal to key we return the value of the current node 
               if (next.mKey.Equals (key))
                  return next.mValue;

               next = next.mNext;
            }
            if (next.mKey.Equals (key))
               return next.mValue;
         }

         Console.WriteLine ("The node is not in the map!");
         Console.WriteLine ();

         return default(ValueType);
      }

      /// <summary>
      /// Remove the specified Node
      /// </summary>
      /// <param name="key">Key.</param>
      public void remove(KeyType key) {
         int hashCode = Math.Abs(key.GetHashCode()) % TABLE_SIZE;
         bool removed = false;

         // If the index in the hash map is not empty
         if (!mTable [hashCode].Equals (null)) {
            Node next = mTable [hashCode];

            while (next.mNext != null) {
               // If the next key is equal to key we return the value of the current node 
               if (next.mNext.mKey.Equals (key)) {
                  // Set the next node to be the one after the one we are removing
                  next.mNext = next.mNext.mNext;

                  removed = true;
                  break;
               }
               next = next.mNext;
            }
            if (next.mKey.Equals (key)) {
               // Set the next node to be the one after the one we are removing
               next.mNext = next.mNext.mNext;

               removed = true;
            }

            if (mCount > 0 && removed)
               mCount--;
         }
         else {
            Console.WriteLine ("The node is not in the map!");
            Console.WriteLine ();
         }
      }
   }
}