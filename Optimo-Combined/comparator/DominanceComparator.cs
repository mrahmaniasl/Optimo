using System;
using System.Collections;
//using jmetal.core;

namespace Optimo_Combined
{
  internal class DominanceComparator : IComparer
  {
    int IComparer.Compare (object x, object y)
    {
      int dominate1;
      // dominate1 indicates if some objective of solution1
      // dominates the same objective in solution2. dominate2
      int dominate2;
      // Complementary of dominates1
      int flag;
      //stores the result of the comparison


      Solution solution1 = (Solution)x;
      Solution solution2 = (Solution)y;

      dominate1 = 0;
      dominate2 = 0;
 
      // Dominance Test
      double value1, value2;
      for (int i = 0; i < solution1.numberOfObjectives_; i++) {
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

      if (dominate1 == dominate2) {
        return 0; //No one dominate the other
      }
      if (dominate1 == 1) {
        return -1; // solution1 dominate
      }
      return 1;    // solution2 dominate
    }
  }
}
