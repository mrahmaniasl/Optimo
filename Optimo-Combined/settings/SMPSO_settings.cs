//
//  SMPSO_settings.cs
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

/*using jmetal.core;
using jmetal.operators.crossover;
using jmetal.operators.selection;
using jmetal.metaheuristics;
using jmetal.operators.comparator;
using jmetal.operators.mutation;
using jmetal.problems;*/

namespace Optimo_Combined
{
  class SMPSO_settings : Settings
  { 
    public int swarmSize_;
    public int maxIterations_;
    public int archiveSize_ ;
    public double mutationProbability_ ;
    public double mutationDistributionIndex_ ;

    public SMPSO_settings(String problemName, int numPar, int[] lowerLim, int[] upperLim, int numObj, int popSize) : base(problemName, numPar, lowerLim, upperLim, numObj, popSize)
    {
      encoding_ = "Real";
      problem_ = new ProblemFactory().getProblem(problemName, (Object)encoding_, numPar, lowerLim, upperLim, numObj/*, popSize*/);
      //Console.WriteLine ("ProblemFactory: created problem " + problem_.problemName_);

      swarmSize_ = popSize;
      maxIterations_ = 250;
      archiveSize_ = popSize;
      mutationProbability_ = 1.0 / this.problem_.numberOfVariables_;
      mutationDistributionIndex_ = 20.0;
    }

    override public Algorithm configure()
    {
      Algorithm algorithm;

      Crossover crossover;
      Mutation mutation;
      Selection selection;

      Dictionary<string, object> parameters;

      algorithm = new _SMPSO(problem_);

      // Algorithm parameters
      algorithm.inputParameters_.Add("swarmSize", swarmSize_);
      algorithm.inputParameters_.Add("maxIterations", maxIterations_);
      algorithm.inputParameters_.Add("archiveSize", archiveSize_);

      // Mutation
      parameters = new Dictionary<string, object>();
      parameters.Add("probability", 0.01);
      parameters.Add("distributionIndex", 20.0);
      mutation = MutationFactory.getMutationOperator("PolynomialMutation", parameters);

      // Add the operators to the algorithm
      algorithm.operators.Add("mutation", mutation);

      return algorithm;
    }
  }
}
