//  Developed based on CrowdingDistanceComparator.cs, which was created by 
//  Antonio J. Nebro <antonio@lcc.uma.es>
//
//  Development team:
//       Mohammad Rahmani Asl <mrah@tamu.edu>
//       Alexander Stoupine <astoupine1@tamu.edu> 
//       Wei Yan <wyan@tamu.edu>
//       at BIM-SIM Group, College of Architecture, Texas A&M University.
//
//  Below is the original comment header of CrowdingDistanceComparator.cs
//  CrowdingDistanceComparator.cs
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

namespace Optimo
{
  internal class CrowdingDistanceComparator : IComparer
  {
    int IComparer.Compare (object x, object y)
    {
      double distance1 = ((Solution)x).crowdingDistance_;
      double distance2 = ((Solution)y).crowdingDistance_;
      if (distance1 > distance2)
        return -1;
      else if (distance1 < distance2)
        return 1;
      else
      return 0;    
    }
  }
}
