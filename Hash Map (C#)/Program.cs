using System;
using System.IO;
using System.Collections.Generic;

namespace Lab3 {
   class MainClass {
      public const int TABLE_SIZE = 509;

      public static int ReadInt () {
         string line = Console.ReadLine (); // Read string from console
         int value;
         if (!int.TryParse(line, out value)) // Try to parse the string as an integer
            Console.WriteLine("Not a valid integer.");
         Console.WriteLine ();
         return value;
      }

      public static void Main (string[] args) {
         bool running = true;

         var reader = new StreamReader(File.OpenRead(@"players_homeruns.csv"));
         List<string> keys = new List<string>();
         List<string> values = new List<string>();
         while (!reader.EndOfStream) {
            var line = reader.ReadLine();
            var split = line.Split(',');

            keys.Add(split[0]);
            values.Add(split[1]);
         }

         HashMap<string, int> hMap = new HashMap<string, int> (TABLE_SIZE);

         for (int i = 0; i < keys.Count; i++)
            hMap.insert (keys [i], Int32.Parse(values [i]));

         while (running) {
            string key;
            int value;

            Console.WriteLine ("---------------------------");
            Console.WriteLine ("Main Menu:");
            Console.WriteLine ();
            Console.WriteLine ("1) Insert");
            Console.WriteLine ("2) Remove");
            Console.WriteLine ("3) Find");
            Console.WriteLine ();
            Console.WriteLine ("0) Quit");
            Console.WriteLine ("---------------------------");
            Console.WriteLine ();

            int choice = ReadInt ();

            switch (choice) {
            case 1:
               Console.WriteLine ("Enter a key to insert");
               key = Console.ReadLine ();
               Console.WriteLine ();

               Console.WriteLine ("Enter a value");
               value = ReadInt ();

               hMap.insert (key, value);
               break;
            case 2:
               Console.WriteLine ("Enter a key to remove");
               key = Console.ReadLine ();
               Console.WriteLine ();

               hMap.remove (key);
               break;
            case 3:
               Console.WriteLine ("Enter a key to find");
               key = Console.ReadLine ();
               Console.WriteLine ();

               Console.WriteLine("Key: " + key + ", Value: " + hMap.find (key));
               break;
            case 0:
               running = false;
               break;
            default:
               Console.WriteLine ("Wrong input!");
               break;
            }
            Console.WriteLine ();
         }
      }
   }
}
