using System;
using System.Collections;
using System.Collections.Generic;

namespace Lab5 {
   public class WeightedGraph{
      public class WeightedNode {
         int mIndex;
         public ArrayList mNeighbors = new ArrayList();

         public WeightedNode (int index) {
            mIndex = index;
         }

         public int getIndex() {
            return mIndex;
         }

         public ArrayList getNeighbors() {
            return mNeighbors;
         }
      }

      public class WeightedEdge {
         private WeightedNode mFirst, mSecond;
         private double mWeight;

         public WeightedEdge (WeightedNode first, WeightedNode second, double weight) {
            mFirst = first;
            mSecond = second;
            mWeight = weight;
         }

         public int compareTo(Object o) {
            WeightedEdge e = (WeightedEdge)o;
            return mWeight.CompareTo(e.mWeight);
         }

         public WeightedNode getFirst() {
            return mFirst;
         }

         public WeightedNode getSecond() {
            return mSecond;
         }

         public double getWeight() {
            return mWeight;
         }
      }

      private ArrayList mVerticies = new ArrayList();

      public WeightedGraph(int numberOfVerticies) {
         for (int i = 0; i < numberOfVerticies; i++) {
            mVerticies.Add (new WeightedNode (i));
         }
      }

      public void addEdge(int firstVertex, int secondVertex, double weight) {
         WeightedNode first = (WeightedNode)mVerticies[firstVertex];
         WeightedNode second = (WeightedNode)mVerticies [secondVertex];

         WeightedEdge edge = new WeightedEdge (first, second, weight);
         first.getNeighbors().Add (edge);
         second.getNeighbors().Add (edge);
      }

      public void printGraph() {
         foreach (WeightedNode n in mVerticies) {
            foreach (WeightedEdge e in n.mNeighbors) {
               Console.WriteLine ("From " + e.getFirst ().getIndex () + " to " + e.getSecond ().getIndex () + ": " + e.getWeight ());
            }
         }
      }

      public WeightedGraph getMinimumSpanningTree() {
         WeightedGraph mst = new WeightedGraph (6);

         ArrayList marked = new ArrayList ();
         bool added = false;
         int count = 0;

         while (!added) {
            WeightedNode first = (WeightedNode)mVerticies[count];

            if (first.mNeighbors.Count > 0) {
               WeightedEdge edge = (WeightedEdge)first.mNeighbors [0];
               mst.addEdge (first.getIndex (), edge.getSecond().getIndex (), edge.getWeight ());
               marked.Add (first.getIndex ());
               marked.Add (edge.getSecond ().getIndex ());
               added = true;
            }
         }
         count = 1;

         while (count < (mVerticies.Count - 1)) {
            added = false;
            foreach (int i in marked) {
               WeightedNode n = (WeightedNode)mVerticies [i];
               foreach (WeightedEdge e in n.mNeighbors) {
                  if (n.getIndex () == e.getFirst ().getIndex ()) {
                     if (!marked.Contains (e.getSecond ().getIndex ())) {
                        marked.Add (e.getSecond ().getIndex ());
                        mst.addEdge (e.getFirst ().getIndex (), e.getSecond ().getIndex (), e.getWeight ());
                        count++;
                        added = true;
                     }
                  } else {
                     if (!marked.Contains (e.getFirst ().getIndex ())) {
                        marked.Add (e.getFirst ().getIndex ());
                        mst.addEdge (e.getFirst ().getIndex (), e.getSecond ().getIndex (), e.getWeight ());
                        count++;
                        added = true;
                     }
                  }
                  if (added)
                     break;
               }
               if (added)
                  break;
            }
         }
         return mst;
      }

      public DijkstraDistance[] getShortestPathsFrom(int source) {
         var vertexes = new List<DijkstraDistance> ();

         DijkstraDistance[] distances = new DijkstraDistance[mVerticies.Count];
         distances[source] = new DijkstraDistance(source, 0);

         for (int i = 0; i < distances.Length; i++) {
            if (i != source)
               distances[i] = new DijkstraDistance(i, Int32.MaxValue);
            vertexes.Add(distances[i]);
         }

         while (vertexes.Count > 0) {
            vertexes.Sort ();
            DijkstraDistance smallest = vertexes [0];

            vertexes.Remove (smallest);

            WeightedNode n = (WeightedNode)mVerticies [smallest.mVertex];

            double dist;

            foreach (WeightedEdge e in n.getNeighbors()) {
               if (n == e.getFirst ()) {
                  dist = distances [n.getIndex ()].mCurrentDistance + e.getWeight ();

                  if (dist < distances [e.getSecond ().getIndex()].mCurrentDistance)
                     distances [e.getSecond ().getIndex()].mCurrentDistance = dist;
               } else {
                  dist = distances [n.getIndex ()].mCurrentDistance + e.getWeight ();

                  if (dist < distances [e.getFirst ().getIndex()].mCurrentDistance)
                     distances [e.getFirst ().getIndex()].mCurrentDistance = dist;
               }
            }
         }

         return distances;
      }

      public class DijkstraDistance : IComparable {
         public int mVertex;
         public double mCurrentDistance;

         public DijkstraDistance(int vertex, double distance) {
            mVertex = vertex;
            mCurrentDistance = distance;
         }

         #region IComparable implementation

         public int CompareTo (object obj)
         {
            DijkstraDistance x = (DijkstraDistance)obj;
            if (mCurrentDistance > x.mCurrentDistance)
               return 1;
            if (mCurrentDistance < x.mCurrentDistance)
               return -1;
            else
               return 0;
         }

         #endregion

         public int Compare (object a, object b) {
         DijkstraDistance x = (DijkstraDistance)a;
         DijkstraDistance y = (DijkstraDistance)b;
         if (x.mCurrentDistance > y.mCurrentDistance)
            return 1;
         if (x.mCurrentDistance < y.mCurrentDistance)
            return -1;
         else
            return 0;
      }
      }

      public static void Main(string[] args) {
         WeightedGraph g = new WeightedGraph (6);

         g.addEdge (0, 1, 1);
         g.addEdge (0, 2, 3);
         g.addEdge (1, 2, 1);
         g.addEdge (1, 3, 1);
         g.addEdge (1, 4, 4);
         g.addEdge (2, 3, 1);
         g.addEdge (2, 5, 2);
         g.addEdge (3, 4, 1);
         g.addEdge (3, 5, 3);
         g.addEdge (4, 5, 2);
         //g.printGraph ();

         DijkstraDistance[] distances = g.getShortestPathsFrom (0);
         for (int i = 0; i < distances.Length; i++) {
            Console.WriteLine ("Distance from 0 to " + i + ": " + distances [i].mCurrentDistance);
         }

         WeightedGraph mst = g.getMinimumSpanningTree ();
         Console.WriteLine ("Minimum spanning tree:");
         mst.printGraph ();

      }
   }
}

