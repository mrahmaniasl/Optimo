//
//  RandomPermutation.cs
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
using System;

namespace Optimo_Combined
{
  internal static class RandomPermutation
  {
    static public void execute (int[] perm, int size)
    {
      // <pex>
      if (perm == (int[])null)
        throw new ArgumentNullException("perm");
      if (perm.Length < 2)
        throw new ArgumentException("perm.Length < 2", "perm");
      // </pex>
      int[] index = new int[size];
      bool[] flag = new bool[size];

      for (int n = 0; n < size; n++) {
        index[n] = n;
        flag[n] = true;
      }
      
      int num = 0;
      while (num < size) {
        int start = PseudoRandom.Instance ().Next (0, size - 1);
        //int start = int(size*nd_uni(&rnd_uni_init));
        while (true) {
          if (flag[start]) {
            perm[num] = index[start];
            flag[start] = false;
            num++;
            break;
          }
          if (start == (size - 1)) {
            start = 0;
          } else {
            start++;
          }
        }
      }
      // while
    } // randomPermutation

  } // RandomPermutation
}
