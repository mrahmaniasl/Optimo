//  Developed based on NSGAII.cs, which was created by 
//  Antonio J. Nebro <antonio@lcc.uma.es>
//
//  Development team:
//       Mohammad Rahmani Asl <mrah@tamu.edu>
//       Alexander Stoupine <astoupine1@tamu.edu> 
//       Wei Yan <wyan@tamu.edu>
//       at BIM-SIM Group, College of Architecture, Texas A&M University.
//
//  Below is the original comment header of NSGAII.cs
//
//  NSGAII.cs
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
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using jmetal.core;
//using jmetal.operators.comparator;
//using jmetal.util;
using System.IO;

namespace Optimo
{
    internal class NSGAII : Algorithm
    {

        public NSGAII(Problem problem)
            : base(problem)
        {
        }

        // NSGAII
        public override SolutionSet execute()
        {
            int populationSize;
            int maxEvaluations;
            int evaluations;

            SolutionSet population;
            SolutionSet offspringPopulation;
            SolutionSet union;
            SolutionSet allTest = new SolutionSet();

            Operator mutationOperator;
            Operator crossoverOperator;
            Operator selectionOperator;

            populationSize = (int)inputParameters_["populationSize"];
            maxEvaluations = (int)inputParameters_["maxEvaluations"];

            // Initializing variables
            population = new SolutionSet(populationSize);
            evaluations = 0;

            //System.Console.WriteLine("Solves Name:" + problem_.problemName_);
            //System.Console.WriteLine("Pop size : " + populationSize);
            //System.Console.WriteLine("Max Evals: " + maxEvaluations);

            // Reading operators
            mutationOperator = operators["mutation"];
            crossoverOperator = operators["crossover"];
            selectionOperator = operators["selection"];

            //System.Console.WriteLine("Crossover parameters: " + crossoverOperator);
            //System.Console.WriteLine("Mutation parameters: " + mutationOperator);

            // Creating the initial solutionSet
            Solution newSolution;
            for (int i = 0; i < populationSize; i++)
            {
                newSolution = new Solution(problem_);
                //newSolution.variable_[0] = 1;
                problem_.evaluate(newSolution);
                //newSolution.objective_[0] = 10;
                //Mohammad
                //slnLst.Add(newSolution);
                //System.Console.WriteLine ("" + i + ": " + newSolution);
                //problem_.evaluateConstraints(newSolution);
                evaluations++;
                population.add(newSolution);
            }
            //for
            // Main loop
            while (evaluations < maxEvaluations)
            {
                // Creating the offSpring solutionSet

                offspringPopulation = new SolutionSet(populationSize);
                Solution[] parents = new Solution[2];

                for (int i = 0; i < (populationSize / 2); i++)
                {
                    if (evaluations < maxEvaluations)
                    {
                        //if ((evaluations % 1000) == 0)
                        //    Console.WriteLine("Evals: " + evaluations);
                        // selection
                        parents[0] = (Solution)selectionOperator.execute(population);
                        parents[1] = (Solution)selectionOperator.execute(population);

                        // crossover
                        Solution[] offSpring = (Solution[])crossoverOperator.execute(parents);

                        // mutation
                        mutationOperator.execute(offSpring[0]);
                        mutationOperator.execute(offSpring[1]);

                        //Environment.Exit(0);
                        // evaluation
                        problem_.evaluate(offSpring[0]);
                        problem_.evaluate(offSpring[1]);

                        offspringPopulation.@add(offSpring[0]);
                        offspringPopulation.add(offSpring[1]);

                        evaluations += 2;
                    }
                    // if
                }
                // for
                // Creating the solutionSet union of solutionSet and offSpring
                union = ((SolutionSet)population).union(offspringPopulation);

                //Mohammad
                allTest = ((SolutionSet)allTest).union(union);
                //System.Console.WriteLine ("Union size:" + union.size ());

                // Ranking the union
                Ranking ranking = new Ranking(union);

                int remain = populationSize;
                int index = 0;
                SolutionSet front = null;

                //Distance distance = new Distance ();

                population.clear();

                // Obtain the next front
                front = ranking.getSubfront(index);
                //*
                while ((remain > 0) && (remain >= front.size()))
                {
                    //Assign crowding distance to individuals
                    Distance.crowdingDistanceAssignment(front, problem_.numberOfObjectives_);
                    //Add the individuals of this front
                    for (int k = 0; k < front.size(); k++)
                    {
                        population.@add(front[k]);
                    }
                    // for
                    //Decrement remain
                    remain = remain - front.size();

                    //Obtain the next front
                    index++;
                    if (remain > 0)
                    {
                        front = ranking.getSubfront(index);
                    }
                    // if        
                }
                // while
                // Remain is less than front(index).size, insert only the best one
                if (remain > 0)
                {
                    // front contains individuals to insert                        
                    Distance.crowdingDistanceAssignment(front, problem_.numberOfObjectives_);
                    IComparer comp = new CrowdingDistanceComparator();
                    front.solutionList_.Sort(comp.Compare);
                    for (int k = 0; k < remain; k++)
                    {
                        population.@add(front[k]);
                    }
                    // for
                    remain = 0;
                }
                // if
            }
            // while
            // Return as output parameter the required evaluations
            outputParameters_["evaluations"] = evaluations;

            // Return the first non-dominated front
            Ranking ranking2 = new Ranking(population);
            //Console.WriteLine(slnLst[0].objective_.GetValue(0));

            //File.WriteAllLines("C:/text.txt",slnLst.ConvertAll(Convert.ToString));
            return ranking2.getSubfront(0);
        }
        // execute
    }
}
