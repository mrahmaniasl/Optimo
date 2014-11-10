//
//  CrowdingArchive.cs
//
//  Author:
//       Antonio J. Nebro <antonio@lcc.uma.es>
//
//  Copyright (c) 2011 Antonio J. Nebro
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU Lesser General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU Lesser General Public License for more details.
// 
//  You should have received a copy of the GNU Lesser General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System;
using System.Collections;
using System.Collections.Generic;
//using jmetal.core;
//using jmetal.operators.comparator;

namespace Optimo_Combined
{
  internal class CrowdingArchive : SolutionSet
  {
    public int maxSize_ { get; set; }
    public int objectives_ { get; set; }

    private IComparer dominance_;
    private IComparer equals_;
    private IComparer crowdingDistance_;

    //private Distance distance_;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="maxSize"> The maximum size of the archive
    /// A <see cref="System.Int32"/>
    /// </param>
    /// <param name="numberOfObjectives">
    /// A <see cref="System.Int32"/>
    /// </param>
    public CrowdingArchive (int maxSize, int numberOfObjectives) : base(maxSize)
    {
      maxSize_ = maxSize;
      objectives_ = numberOfObjectives;
      dominance_ = new DominanceComparator ();
      equals_ = new EqualSolutions ();
      crowdingDistance_ = new CrowdingDistanceComparator ();
      //distance_ = new Distance ();
    }

    /// <summary>
    /// Adds a <code>Solution</code> to the archive. If the <code>Solution</code>
    /// is dominated by any member of the archive, then it is discarded. If the
    /// <code>Solution</code> dominates some members of the archive, these are
    /// removed. If the archive is full and the <code>Solution</code> has to be
    /// inserted, the solutions are sorted by crowding distance and the one having
    /// the minimum crowding distance value.
    /// </summary>
    /// <param name="solution"> The solution to be added
    /// A <see cref="Solution"/>
    /// </param>
    /// <returns> true if the <code>Solution</code> has been inserted, false otherwise.
    /// A <see cref="System.Boolean"/>
    /// </returns>
    public bool Add (Solution solution)
    {
      int flag = 0;
      int i = 0;
      Solution aux;
      //Store an solution temporally
      while (i < solutionList_.Count) {
        aux = solutionList_[i];
        
        flag = dominance_.Compare (solution, aux);
        if (flag == 1) {
          // The solution to add is dominated
          return false;
          // Discard the new solution
        } else if (flag == -1) {
          // A solution in the archive is dominated
          solutionList_.RemoveAt (i);
          // Remove it from the population
        } else {
          if (equals_.Compare (aux, solution) == 0) {
            // There is an equal solution
            // in the population
            return false;
            // Discard the new solution
          }
          // if
          i++;
        }
      }
      // Insert the solution into the archive
      solutionList_.Add (solution);
      if (solutionList_.Count > maxSize_) {
        // The archive is full
        Distance.crowdingDistanceAssignment (this, objectives_);
        solutionList_.Sort (crowdingDistance_.Compare) ;
        //Remove the last
        solutionList_.RemoveAt (maxSize_);
      }
      return true;
    }
    // add
  }
}
