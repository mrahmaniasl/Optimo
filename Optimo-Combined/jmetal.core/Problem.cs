//
//  Problem.cs
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

namespace Optimo_Combined
{
  internal abstract class Problem
  {

    // Defines the default precision of binary-coded variables
    private const int DEFAULT_PRECISSION = 20;

    // Stores the number of variables of the problem
    public int numberOfVariables_
    {
      get;
      set;
    }

    // Stores the number of objectives of the problem
    public int numberOfObjectives_
    {
      get;
      set;
    }

    // Stores the number of constraints of the problem
    public int numberOfConstraints_
    {
      get;
      set;
    }

    // Stores the problem name
    public String problemName_
    {
      get;
      set;
    }

    // Stores the type of the solutions of the problem
    //protected SolutionType solutionType_;
    public SolutionType solutionType_ { get; set ;}

    // Stores the lower bound values for each variable (only if needed)
    //protected double[] lowerLimit_;
    public double[] lowerLimit_ { get; set; }

    // Stores the upper bound values for each variable (only if needed)
    //protected double[] upperLimit_;
    public double[] upperLimit_ { get; set; }
    
    /*
     * Stores the number of bits used by binary-coded variables (e.g., BinaryReal
     * variables). By default, they are initialized to DEFAULT_PRECISION)
     */
    protected int[] precision_;

    /*
     * Stores the length of each variable when applicable (e.g., Binary and
     * Permutation variables)
     */
    protected int[] length_;
    public int getLength(int var)
    {
      if (length_ == null)
        return DEFAULT_PRECISSION;
      return length_[var];
    }

    /*
     * Stores the type of each variable
     */
    public Type[] variableType_;

    /// <summary>
    /// Constructor
    /// </summary>
    public Problem ()
    {
      solutionType_ = null;
      problemName_ = "noname";
    }
    // Solves
    public abstract void evaluate (Solution solution);

    public virtual void evaluateConstraints (Solution solution)
    {
    }  
  }
}
