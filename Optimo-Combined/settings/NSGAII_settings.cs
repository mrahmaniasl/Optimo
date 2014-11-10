//  Developed based on NSGAII_settings.cs, which was created by 
//  Antonio J. Nebro <antonio@lcc.uma.es>
//
//  Development team:
//       Mohammad Rahmani Asl <mrah@tamu.edu>
//       Alexander Stoupine <astoupine1@tamu.edu> 
//       Wei Yan <wyan@tamu.edu>
//       at BIM-SIM Group, College of Architecture, Texas A&M University.
//
//  Below is the original comment header of NSGAII_settings.cs
//
//  NSGAII_settings.cs
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
//using jmetal.operators.crossover;
//using jmetal.operators.selection;
//using jmetal.metaheuristics;
//using jmetal.operators.comparator;
//using jmetal.operators.mutation;
//using jmetal.problems;

namespace Optimo_Combined
{
  class NSGAII_settings : Settings
  { 
    public int populationSize_;
    public int maxEvaluations_;
    public double mutationProbability_ ;
    public double crossoverProbability_ ;
    public double mutationDistributionIndex_ ;
    public double crossoverDistributionIndex_ ;

    public NSGAII_settings(String problemName, int numPar, int[] lowerLim, int[] upperLim, int numObj, int popSize)
        : base(problemName, numPar, lowerLim, upperLim, numObj, popSize)
    {
      encoding_ = "Real";
      problem_ = new ProblemFactory().getProblem(problemName, (Object)encoding_, numPar, lowerLim, upperLim, numObj);
      //Console.WriteLine ("ProblemFactory: created problem " + problem_.problemName_);

      populationSize_ = 30;
      maxEvaluations_ = 600;
      mutationProbability_ = 1.0 / this.problem_.numberOfVariables_;
      crossoverProbability_ = 0.9;
      mutationDistributionIndex_ = 10.0;
      crossoverDistributionIndex_ = 10.0;
    }

    override public Algorithm configure()
    {
      Algorithm algorithm;

      Crossover crossover;
      Mutation mutation;
      Selection selection;

      Dictionary<string, object> parameters;

      algorithm = new NSGAII(problem_);

      // Algorithm parameters
      algorithm.inputParameters_.Add("populationSize", populationSize_);
      algorithm.inputParameters_.Add("maxEvaluations", maxEvaluations_);

      // Crossover
      parameters = new Dictionary<string, object>();
      parameters.Add("probability", 0.9);
      parameters.Add("distributionIndex", 20.0);
      crossover = CrossoverFactory.getCrossoverOperator("SBXCrossover", parameters);

      // Mutation
      parameters = new Dictionary<string, object>();
      parameters.Add("probability", 0.01);
      parameters.Add("distributionIndex", 20.0);
      mutation = MutationFactory.getMutationOperator("PolynomialMutation", parameters);

      // Selection
      parameters = null;
      selection = SelectionFactory.getSelectionOperator("BinaryTournament", parameters);
      selection.comparator_ = new DominanceAndCrowdingDistanceComparator();

      // Add the operators to the algorithm
      algorithm.operators.Add("crossover", crossover);
      algorithm.operators.Add("mutation", mutation);
      algorithm.operators.Add("selection", selection);

      return algorithm;
    }
  }
}
