//  Developed based on Settings.cs, which was created by 
//  Antonio J. Nebro <antonio@lcc.uma.es>
//
//  Development team:
//       Mohammad Rahmani Asl <mrah@tamu.edu>
//       Alexander Stoupine <astoupine1@tamu.edu> 
//       Wei Yan <wyan@tamu.edu>
//       at BIM-SIM Group, College of Architecture, Texas A&M University.
//
//  Below is the original comment header of Settings.cs
//
//  Settings.cs
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
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using jmetal.core;

namespace Optimo
{
  internal abstract class Settings
  {
    public Problem problem_ { get; set; }
    public String problenName_ { get; set; }
    public String encoding_ {get; set ;}
    public int numParams_ { get; set; }
    public double[] lowerLim_ { get; set; }
    public double[] upperLim_ { get; set; }
    public int numObj_ { get; set; }

    public Settings(String problemName, int numP, double[] lowerLim, double[] upperLim, int numObj)
    {
      problem_ = null;
      problenName_ = problemName;
      encoding_ = null ;
      numParams_ = numP;
      lowerLim_ = lowerLim;
      upperLim_ = upperLim;
      numObj_ = numObj;
    }

    public abstract Algorithm configure(); 
  }
}
