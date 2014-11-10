//
//  DominanceAndCrowdingDistanceComparator.cs
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

namespace Optimo_MOEAD
{
  internal class DominanceAndCrowdingDistanceComparator : IComparer
  {
    static IComparer dominance = new DominanceComparator() ;
    static IComparer crowding  = new CrowdingDistanceComparator() ;
     
    int IComparer.Compare (object x, object y)
    {
      int result;

      result = dominance.Compare (x, y);
      if (result == 0)
        result = crowding.Compare (x, y);

      return result ;
    } 
  }
}
