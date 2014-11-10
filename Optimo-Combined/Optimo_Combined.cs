//
//  Optimo.cs
//
//  Authors:
//       Mohammad Rahmani Asl <mrah@tamu.edu>
//       Alexander Stoupine <astoupine1@tamu.edu> 
//       Wei Yan <wyan@tamu.edu>
//       at BIM-SIM Group, College of Architecture, Texas A&M University.
//
//  This program is based on Jmetal optimization program and developed at BIM-SIM Group
//  at Texas A&M university, college of Arichtecture. The source code of jmetal is
//  changed appropriately to enable optimization process on Dynamo <http://dynamobim.org/>.   
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
//  along with this program. See <http://www.gnu.org/licenses/> for GNU Lesser General Public License.

using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Optimo_SMPSO;
using Optimo_MOEAD;
using Optimo;

namespace Optimo_Combined
{
    public static class Combined_
    {
        //creates NSGA_II Algorithm using Lower Limits, Upper Limits, numObjectives, and population size
        private static Optimo_Combined.NSGAII CreateAlgorithm_NSGAII(double numObjectives, List<int> lowerLimits, List<int> upperLimits, int popSize)   //done
        {
            String algorithmName = "NSGAII";//name;
            String problemName = "myTest";

            int NumParam = lowerLimits.Count;
            int numObj = Convert.ToInt32(numObjectives);

            int[] upperLim = upperLimits.ToArray<int>();
            int[] lowerLim = lowerLimits.ToArray<int>();

            Object settingsParams = problemName;
            SettingsFactory sf = new SettingsFactory();
            Settings sets = sf.getSettingsObject(algorithmName, problemName, NumParam, lowerLim, upperLim, numObj, popSize);

            Optimo_Combined.NSGAII algorithm = (Optimo_Combined.NSGAII)sets.configure();    //changed to be specific algorithm

            //Algorithm algorithm = sets.configure();
            return algorithm;
        }
        //creates SMPSO Algorithm using Lower Limits, Upper Limits, numObjectives, and population size
        private static Optimo_Combined._SMPSO CreateAlgorithm_SMPSO(double numObjectives, List<int> lowerLimits, List<int> upperLimits, int popSize)    //done
        {
            String algorithmName = "SMPSO";//name;
            String problemName = "myTest";

            int NumParam = lowerLimits.Count;
            int numObj = Convert.ToInt32(numObjectives);

            int[] upperLim = upperLimits.ToArray<int>();
            int[] lowerLim = lowerLimits.ToArray<int>();

            Object settingsParams = problemName;
            SettingsFactory sf = new SettingsFactory();
            Settings sets = sf.getSettingsObject(algorithmName, problemName, NumParam, lowerLim, upperLim, numObj, popSize);

            Optimo_Combined._SMPSO algorithm = (Optimo_Combined._SMPSO)sets.configure();    //changed to be specific algorithm

            //Algorithm algorithm = sets.configure();
            return algorithm;
        }
        //creates MOEAD Algorithm using Lower Limits, Upper Limits, numObjectives, and population size
        private static Optimo_Combined.MOEAD CreateAlgorithm_MOEAD(double numObjectives, List<int> lowerLimits, List<int> upperLimits, int popSize) //done
        {
            String algorithmName = "MOEAD";//name;
            String problemName = "myTest";

            int NumParam = lowerLimits.Count;
            int numObj = Convert.ToInt32(numObjectives);

            int[] upperLim = upperLimits.ToArray<int>();
            int[] lowerLim = lowerLimits.ToArray<int>();

            Object settingsParams = problemName;
            SettingsFactory sf = new SettingsFactory();
            Settings sets = sf.getSettingsObject(algorithmName, problemName, NumParam, lowerLim, upperLim, numObj, popSize);

            Optimo_Combined.MOEAD algorithm = (Optimo_Combined.MOEAD)sets.configure();  //changed to be specific algorithm

            //Algorithm algorithm = sets.configure();
            return algorithm;
        }

        //changes the population to list of list of doubles
        private static List<List<double>> PopToSolutionList(SolutionSet population) //possibly add input for name, add it to the beginning of the list
        {
            int numVar;
            int numObj;
            if (population.size() > 0)
            {
                numVar = population[0].numberOfVariables_;
                numObj = population[0].numberOfObjectives_;
            }
            else
            {
                throw new Exception("Population size is 0 and it cannot be changed to list of list of doubles");
            }

            List<List<double>> SolutionsList = new List<List<double>>();
            for (int i = 0; i < numVar; i++)
            {
                List<double> SolutionList = new List<double>();
                for (int j = 0; j < population.size(); j++)
                {
                    SolutionList.Add(population[j].variable_[i].value_);
                }
                SolutionsList.Add(SolutionList);
            }
            for (int i = 0; i < numObj; i++)
            {
                List<double> SolutionList = new List<double>();
                for (int j = 0; j < population.size(); j++)
                {
                    SolutionList.Add(population[j].objective_[i]);
                }
                SolutionsList.Add(SolutionList);
            }
            return SolutionsList;
        }

        //changes solution list to solution set
        private static SolutionSet SolutionListToPop(List<List<double>> populationList, Algorithm algorithm, int numVar)    //done
        {
            int popSize = populationList[0].Count;
            int numObj = populationList.Count - numVar;

            SolutionSet population = new SolutionSet(popSize);
            Solution newSolution;

            for (int j = 0; j < popSize; j++)
            {
                newSolution = new Solution(algorithm.problem_);
                for (int i = 0; i < numVar; i++)
                {
                    newSolution.variable_[i].value_ = populationList[i][j];
                }
                population.add(newSolution);
            }
            for (int j = 0; j < population.size(); j++)
            {
                for (int i = 0; i < numObj; i++)
                {
                    population[j].objective_[i] = populationList[i + numVar][j];
                }
            }
            return population;
        }


        /// <summary>
        /// Creates the initial solution lists of variables and objectives.
        /// The objectives will be assigned after evaluation using
        /// NSGA_II Assigne Fitness Function Results node.
        /// </summary>
        /// <param name="populationSize">Size that will be used to generate population.</param>
        /// <param name="numObjectives">Number of objectives that you are trying to optimize. If your problem is single objective optimization, set this number to one.</param>
        /// <param name="lowerLimits">List of lower limits for all variable. The number of lower limits should be the same as the number of variables.</param>
        /// <param name="upperLimits">List of upper limits for all variable. The number of upper limits should be the same as the number of variables.</param>
        /// <returns>An initial solution list.</returns>
        public static List<List<double>> InitialSolutionList(double populationSize = 10, double numObjectives = 1, List<int> lowerLimits = null, List<int> upperLimits = null, string name = "NSGAII")    //done
        {
            //number of varables plus number of objectives
            int numVar;
            int numObj = Convert.ToInt32(numObjectives);

            if (lowerLimits != null)    //initializing numVar based on whether an input was given or not
                numVar = lowerLimits.Count;
            else
                numVar = 1;

            int popSize = (int)populationSize;

            Algorithm algorithm; 

            //Creating the right algorithm based on which algorithm the user wants to use
            if (name == "MOEAD")
                algorithm = CreateAlgorithm_SMPSO(numObjectives, lowerLimits, upperLimits, popSize);
            else if (name == "SMPSO")
                algorithm = CreateAlgorithm_MOEAD(numObjectives, lowerLimits, upperLimits, popSize);
            else //default case
                algorithm = CreateAlgorithm_NSGAII(numObjectives, lowerLimits, upperLimits, popSize);


            //Initializing variables
            SolutionSet population = new SolutionSet(Convert.ToInt32(populationSize));

            // Creating the initial solutionSet
            Solution newSolution;
            for (int i = 0; i < populationSize; i++)
            {
                newSolution = new Solution(algorithm.problem_);
                population.add(newSolution);
            }

            List<List<double>> test = PopToSolutionList(population);

            return PopToSolutionList(population);
        }

        /// <summary>
        /// Assigns the evaluated fitness function results to the population list.
        /// </summary>
        /// <param name="populationList">Population list that is not evaluated yet.</param>
        /// <param name="FitnessList">Fitness Function Results to be assigne to the population list.</param>
        /// <returns>Evalusted population list.</returns>
        public static List<List<double>> AssignFitnessFuncResults(List<List<double>> populationList, List<List<double>> FitnessList)    //done, no default inputs
        {
            int populationSize = populationList[0].Count;
            int numVar = populationList.Count - FitnessList.Count;

            for (int y = numVar; y < populationList.Count; y++)
            {
                for (int x = 0; x < populationSize; x++)
                {
                    populationList[y][x] = FitnessList[y - numVar][x];
                }
            }

            return populationList;
        }

        /// <summary>
        /// Generated new the children population list based on the parents population list.
        /// This node needs to be used in a loop along with Assign Fitness Function node and
        /// Sorting node.
        /// </summary>
        /// <param name="populationList">Parents population list.</param>
        /// <param name="lowerLimits">List of lower limits for new generation.</param>
        /// <param name="upperLimits">List of upper limits for new generation.</param>
        /// <returns>Generated population list.</returns>
        public static List<List<double>> GenerationAlgorithm(List<List<double>> populationList, List<int> lowerLimits, List<int> upperLimits, int currentIteration = 0, int maxIterations = 0, string name = "NSGAII") //done
        {
            //Simply calling the generation algorithm function for the algorithm specified
            if (name == "MOEAD")
                return Optimo_MOEAD.MOEAD_.GenerationAlgorithm(populationList, lowerLimits, upperLimits);
            else if (name == "SMPSO")
                return Optimo_SMPSO.SMPSO_.GenerationAlgorithm(populationList, lowerLimits, upperLimits, currentIteration, maxIterations);
            else    //default case
                return Optimo.NSGA_II.GenerationAlgorithm(populationList, lowerLimits, upperLimits);
        }

        /// <summary>
        /// Sorts the combination of parents and child population set based on Pareto Fron Ranking.
        /// </summary>
        /// <param name="population">Parent solution set.</param>
        /// <param name="offspringPop">Offspring solution set.</param>
        /// <param name="lowerLimits">List of lower limits.</param>
        /// <param name="upperLimits">List of upper limits.</param>
        /// <returns></returns>
        public static List<List<double>> Sorting(List<List<double>> populationList, List<List<double>> offspringList, List<int> lowerLimits, List<int> upperLimits, string name = "NSGAII") //done
        {
            //Simply calling the sorting algorithm function for the algorithm specified
            if (name == "MOEAD")
                return Optimo_MOEAD.MOEAD_.Sorting(populationList, offspringList, lowerLimits, upperLimits);
            else if (name == "SMPSO")
                return Optimo_SMPSO.SMPSO_.Sorting(populationList, offspringList, lowerLimits, upperLimits);
            else    //default case
                return Optimo.NSGA_II.Sorting(populationList, offspringList, lowerLimits, upperLimits);

        }
    }
}
