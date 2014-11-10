//
//  Solution.cs
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

namespace Optimo_SMPSO
{
  internal class Solution
  {
    // Problem to solve
    Problem problem_;

    // Solution type
    public SolutionType type_ { get; set; }

    // Decision variables of the solution.
    //private Variable[] variable;
    public Variable[] variable_ { get; set ; }

    // Objectives values of the solution.
    //private double[] objective;
    public double[] objective_ { get; set ; }

    // Number of objective values of the solution
    public int numberOfObjectives_ { get; set; }

    // Number of variables of the solution
    public int numberOfVariables_ {
      get { return problem_.numberOfVariables_; }
    }

    // Rank of the solution. Used in NSGA-II
    public int rank_ { get; set; }

    // Overall constraint violation value of the solution
    public double overallConstraintViolation_ { get; set; }

    // Number of constraints violated by the solution
    public int numberOfViolatedConstraints_ { get; set; }

    // Crowding distance of the the solution in a SolutionSet. Used in NSGA-II.
    public double crowdingDistance_ { get; set; }


    /// <summary>
    /// Constructor
    /// </summary>
    public Solution ()
    {
      problem_ = null;
      overallConstraintViolation_ = 0.0;
      numberOfViolatedConstraints_ = 0;
      type_ = null;
      variable_ = null;
      objective_ = null;
    }
    // Solution

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="problem"> Problem to solve
    /// A <see cref="Problem"/>
    /// </param>
    public Solution (Problem problem)
    {
      // <pex>
      if (problem == (Problem)null)
        throw new ArgumentNullException("problem");
      if (problem.solutionType_ == (SolutionType)null)
        throw new ArgumentNullException("problem");
      // </pex>
      problem_ = problem;
      type_ = problem.solutionType_;
      numberOfObjectives_ = problem_.numberOfObjectives_;
      objective_ = new double[numberOfObjectives_];
      
      // Setting initial values
      crowdingDistance_ = 0.0;

      variable_ = problem.solutionType_.createVariables () ;
    }
    // Solution

    /// <summary>
    /// Copy constructor
    /// </summary>
    /// <param name="solution">
    /// A <see cref="Solution"/>
    /// </param>
    public Solution (Solution solution)
    {
      // <pex>
      if (solution == (Solution)null)
        throw new ArgumentNullException("solution");
      if (solution.type_ == (SolutionType)null)
        throw new ArgumentNullException("solution");
      // </pex>
      problem_ = solution.problem_;
      type_ = solution.type_;
      
      numberOfObjectives_ = solution.numberOfObjectives_;
      objective_ = new double[numberOfObjectives_];
      for (int i = 0; i < numberOfObjectives_; i++) {
        objective_[i] = solution.objective_[i];
      }
      // for
      //<-
      
      variable_ = type_.copyVariables (solution.variable_);
      overallConstraintViolation_ = solution.overallConstraintViolation_;
      numberOfViolatedConstraints_ = solution.numberOfViolatedConstraints_;
      //distanceToSolutionSet_ = solution.getDistanceToSolutionSet();
      crowdingDistance_ = solution.crowdingDistance_;
      //kDistance_            = solution.getKDistance();
      //fitness_              = solution.getFitness();
      //marked_ = solution.isMarked ();
      rank_ = solution.rank_;
      //location_             = solution.getLocation();
      
    }
    // Solution
    public override string ToString ()
    {
      //return string.Format ("[Solution: type={0}, variable={1}, objective={2}]", type, variable, objective);
      string str = "Vars: ";
      int l = variable_.Length - 1;
      for (int i = 0; i < l; i++)
        str += variable_[i] + " , ";
      str += variable_[l];

      str += "\t Funcs:";
      l = objective_.Length - 1;
      for (int i = 0; i < l; i++)
        str += objective_[i] + " , ";
      str += objective_[l] ;
      
      return str;
    }
  }
}
