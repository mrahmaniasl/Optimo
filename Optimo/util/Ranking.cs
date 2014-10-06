using System;
using System.Collections;
using System.Collections.Generic;
//using jmetal.core;
//using jmetal.operators.comparator;

namespace Optimo
{
  internal class Ranking
  {
    // The solution set to be ranked
    private SolutionSet solutionSet_;

    // An array containing all the fronts found during the search
    private SolutionSet[] ranking_;

    // Comparator for dominance checking
    private IComparer dominance_ = new DominanceComparator ();
    public Ranking (SolutionSet solutionSet)
    {
      // <pex>
      if (solutionSet == (SolutionSet)null)
        throw new ArgumentNullException("solutionSet");
      // </pex>
      solutionSet_ = solutionSet;
      
      // dominateMe[i] contains the number of solutions dominating i
      int[] dominateMe = new int[solutionSet_.size ()];
      
      // iDominate[k] contains the list of solutions dominated by k
      List<int>[] iDominate = new List<int>[solutionSet_.size ()];
      
      // front[i] contains the list of individuals belonging to the front i
      //List<int>[] front = new List<int>[solutionSet_.size () + 1];
      
      List<List<int>> front2 = new List<List<int>> ();
      
      // flagDominate is an auxiliar variable
      int flagDominate;
      
      // Initialize the fronts
      //for (int i = 0; i < front.Length; i++)
      //front[i] = new List<int> ();
      
      front2.Add (new List<int> ());
      
      // Fast non dominated sorting algorithm
      for (int p = 0; p < solutionSet_.size (); p++) {
        // Initialice the list of individuals that i dominate and the number
        // of individuals that dominate me
        iDominate[p] = new List<int> ();
        dominateMe[p] = 0;
        // For all q individuals , calculate if p dominates q or vice versa
        for (int q = 0; q < solutionSet_.size (); q++) {
          //flagDominate =constraint_.compare(solutionSet.get(p),solutionSet.get(q));
          //if (flagDominate == 0) {
          flagDominate = dominance_.Compare (solutionSet[p], solutionSet[q]);
          //}
          
          if (flagDominate == -1) {
            iDominate[p].Add (q);
          } else if (flagDominate == 1) {
            dominateMe[p]++;
          }
        }
        
        // If nobody dominates p, p belongs to the first front
        if (dominateMe[p] == 0) {
          //front[0].Add (p);
          front2[0].Add (p);
          solutionSet[p].rank_ = 0;
        }
      }
      
      //Obtain the rest of fronts
      int currentRank = 0;
      //Iterator<Integer> it1, it2 ; // Iterators
      //while (front[currentRank].Capacity != 0) {
      
      while (front2[currentRank].Count != 0) {
        currentRank++;
        front2.Add (new List<int> ());
        //front[currentRank].Clear ();
        //foreach (int p in front[currentRank - 1]) {
        foreach (int p in front2[currentRank - 1]) {
          foreach (int q in iDominate[p]) {
            dominateMe[q]--;
            if (dominateMe[q] == 0) {
              solutionSet_[q].rank_ = currentRank;
              //  front[currentRank].Add (q);
              front2[currentRank].Add (q);
            }
          }
        }
      }
      
      ranking_ = new SolutionSet[currentRank];
      for (int i = 0; i < currentRank; i++) {
        //ranking_[i] = new SolutionSet (front[i].Capacity);
        ranking_[i] = new SolutionSet (front2[i].Count);
        //foreach (int elem in front[i])
        foreach (int elem in front2[i]) {
          ranking_[i].add (solutionSet_[elem]);
        }
        // foreach
      }
      // for
      
    }
    // Ranking

    /// <summary>
    /// Returns a give subfront
    /// </summary>
    /// <param name="rank">
    /// A <see cref="System.Int32"/>
    /// </param>
    /// <returns>
    /// A <see cref="SolutionSet"/>
    /// </returns>
    public SolutionSet getSubfront (int rank)
    {
      // <pex>
      if ((uint)rank >= (uint)(this.ranking_.Length))
        throw new ArgumentException("complex reason");
      // </pex>
      return ranking_[rank];
    }
    // getSubFront


    /// <summary>
    /// Prints the ranking into a string
    /// </summary>
    /// <returns>
    /// A <see cref="System.String"/>
    /// </returns>
    public override string ToString ()
    {
      string str = "POPULATION TO RANK (" + solutionSet_.size () + ")\n";
      
      for (int i = 0; i < solutionSet_.size (); i++) {
        str += "" + i + ": " + solutionSet_[i] + "\n";
      }
      
      int l = ranking_.GetLength (0);
      str += "Number of ranks: " + l + "\n";
      
      for (int rank = 0; rank < l; rank++) {
        str += "-- Rank: " + rank + "\n";
        for (int sol = 0; sol < ranking_[rank].size (); sol++) {
          str += ranking_[rank][sol] + "\n";
        }
      }
      
      return str;
    }


  }
  // Ranking
}
