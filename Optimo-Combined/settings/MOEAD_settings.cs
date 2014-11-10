//
//  MOEAD_settings.cs
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
  class MOEAD_settings : Settings
  { 
    public int populationSize_;
    public int maxEvaluations_;
    public string dataDirectory_ ;

    public double mutationProbability_ ;
    public double mutationDistributionIndex_ ;

    public double CR_ ;
    public double F_ ;

    public MOEAD_settings(String problemName, int numP, int[] lowerLim, int[] upperLim, int numObj, int popSize)
        : base(problemName, numP, lowerLim, upperLim, numObj, popSize)
    {
      encoding_ = "Real";
      problem_ = new ProblemFactory().getProblem(problemName, (Object)encoding_, numP, lowerLim, upperLim, numObj);
      //Console.WriteLine ("ProblemFactory: created problem " + problem_.problemName_);

      populationSize_ = popSize;
      maxEvaluations_ = 150000;
      mutationProbability_ = 1.0 / this.problem_.numberOfVariables_;
      mutationDistributionIndex_ = 20.0;
      CR_ = 1.0;
      F_ = 0.5 ;
      dataDirectory_ ="/Users/Alex";//antonio/Softw/pruebas/data/MOEAD_parameters/Weight/";
    }


    override public Algorithm configure()
    {
      Algorithm algorithm;
      Crossover crossover;
      Mutation mutation;

      Dictionary<string, object> parameters;

      algorithm = new MOEAD(problem_);

      // Algorithm parameters
      algorithm.inputParameters_.Add("populationSize", populationSize_);
      algorithm.inputParameters_.Add("maxEvaluations", maxEvaluations_);
      algorithm.inputParameters_.Add ("dataDirectory", dataDirectory_);

      // Crossover
      parameters = new Dictionary<string, object> ();
      parameters.Add ("CR", CR_);
      parameters.Add ("F", F_);
      crossover = CrossoverFactory.getCrossoverOperator ("DifferentialEvolutionCrossover", parameters);

      // Mutation
      parameters = new Dictionary<string, object> ();
      parameters.Add ("probability", mutationProbability_);
      parameters.Add ("distributionIndex", mutationDistributionIndex_);
      mutation = MutationFactory.getMutationOperator ("PolynomialMutation", parameters);


      // Add the operators to the algorithm
      algorithm.operators.Add("crossover", crossover);
      algorithm.operators.Add("mutation", mutation);

      return algorithm;
    }
  }
}
