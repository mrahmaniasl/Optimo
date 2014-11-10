//
//  EqualSolution.cs
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
//using jmetal.core;

namespace Optimo_MOEAD
{
  internal class EqualSolutions : IComparer
  {
    int IComparer.Compare (object x, object y)
    {
      if (x == null)
        return 1; else if (y == null)
        return -1;
      
      int dominate1;
      // dominate1 indicates if some objective of solution1 
      // dominates the same objective in solution2. dominate2
      int dominate2;
      // is the complementary of dominate1.
      
      dominate1 = 0;
      dominate2 = 0;
      
      Solution solution1 = (Solution)x;
      Solution solution2 = (Solution)y;
      
      int flag;
      double value1, value2;
      for (int i = 0; i < solution1.numberOfObjectives_; i++) {
        IComparer objectiveComparator = new ObjectiveComparator (i);
        flag = objectiveComparator.Compare (solution1, solution2);
        value1 = solution1.objective_[i];
        value2 = solution2.objective_[i];
        
        if (value1 < value2) {
          flag = -1;
        } else if (value1 > value2) {
          flag = 1;
        } else {
          flag = 0;
        }
        
        if (flag == -1) {
          dominate1 = 1;
        }
        
        if (flag == 1) {
          dominate2 = 1;
        }
      }
      
      if (dominate1 == 0 && dominate2 == 0) {
        return 0;
        //No one dominate the other
      }
      
      if (dominate1 == 1) {
        return -1;
        // solution1 dominate
      } else if (dominate2 == 1) {
        return 1;
        // solution2 dominate
      }
      return 2;
    }
    // compare
  }
}

