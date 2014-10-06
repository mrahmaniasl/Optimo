//  Developed based on BinarySolutionType.cs, which was created by 
//  Antonio J. Nebro <antonio@lcc.uma.es>
//
//  Development team:
//       Mohammad Rahmani Asl <mrah@tamu.edu>
//       Alexander Stoupine <astoupine1@tamu.edu> 
//       Wei Yan <wyan@tamu.edu>
//       at BIM-SIM Group, College of Architecture, Texas A&M University.
//
//  Below is the original comment header of BinarySolutionType.cs
//
//  BinarySolutionType.cs
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
//using jmetal.encodings.variable;
using System.Diagnostics;

namespace Optimo
{
  class BinarySolutionType : SolutionType
  {
    public BinarySolutionType(Problem problem) : base(problem)
    {
      // <pex>
      if (problem == (Problem)null)
        throw new ArgumentNullException("problem");
      // </pex>
      problem.variableType_ = new Type[problem.numberOfVariables_];
      problem.solutionType_ = this;
      
      // Initializing the types of the variables
      for (int i = 0; i < problem.numberOfVariables_; i++) {
        problem.variableType_[i] = System.Type.GetType ("jmetal.core.variable.Binary");
        //problem.variableType_[i] = this.GetType ();
      }
      // for
    }

    public override Variable[] createVariables()
    {
      Variable[] variables = new Variable[problem_.numberOfVariables_];

      for (int var = 0; var < problem_.numberOfVariables_; var++)
        variables[var] = new Binary(problem_.getLength(var));

      return variables;
    }
  }
}
