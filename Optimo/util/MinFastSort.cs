//
//  MinFastSort_settings.cs
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
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.using System;

using System.Collections;
using System.Collections.Generic;
//using jmetal.core;
//using jmetal.operators.comparator;
using System;

namespace Optimo
{
  internal static class MinFastSort
  {

    static public void execute (double[] x, int[] idx, int n, int m)
    {
      // <pex>
      if (x == (double[])null)
        throw new ArgumentNullException("x");
      if (x.Length == 0)
        throw new ArgumentException("x.Length == 0", "x");
      // </pex>
      for (int i = 0; i < m; i++) {
        for (int j = i + 1; j < n; j++) {
          if (x[i] > x[j]) {
            double temp = x[i];
            x[i] = x[j];
            x[j] = temp;
            int id = idx[i];
            idx[i] = idx[j];
            idx[j] = id;
          }
          // if
        }
      }
      // for
    }
  }
}
