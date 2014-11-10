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

namespace Optimo_SMPSO
{
    public static class SMPSO_
    {
        //creates SMPSO Algorithm using Lower Limits, Upper Limits and numObjectives
        private static Optimo_SMPSO._SMPSO CreateAlgorithm(double numObjectives, List<int> lowerLimits, List<int> upperLimits, int popSize)
        {
            String algorithmName = "SMPSO";
            String problemName = "myTest";

            int NumParam = lowerLimits.Count;
            int numObj = Convert.ToInt32(numObjectives);

            int[] upperLim = upperLimits.ToArray<int>();
            int[] lowerLim = lowerLimits.ToArray<int>();

            Object settingsParams = problemName;
            SettingsFactory sf = new SettingsFactory();
            Settings sets = sf.getSettingsObject(algorithmName, problemName, NumParam, lowerLim, upperLim, numObj, popSize);

            Optimo_SMPSO._SMPSO algorithm = (Optimo_SMPSO._SMPSO)sets.configure();

            //Algorithm algorithm = sets.configure();
            return algorithm;
        }

        //changes the population to list of list of doubles
        private static List<List<double>> PopToSolutionList(SolutionSet population)
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
        private static SolutionSet SolutionListToPop(List<List<double>> populationList, Algorithm algorithm, int numVar)
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
        public static List<List<double>> InitialSolutionList(double populationSize, double numObjectives, List<int> lowerLimits, List<int> upperLimits)
        {
            //number of varables plus number of objectives
            int numObj = Convert.ToInt32(numObjectives);
            int numVar = lowerLimits.Count;
            int popSize = (int)populationSize;

            Algorithm algorithm = CreateAlgorithm(numObjectives, lowerLimits, upperLimits, popSize);

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
        public static List<List<double>> AssignFitnessFuncResults(List<List<double>> populationList, List<List<double>> FitnessList)
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
        public static List<List<double>> GenerationAlgorithm(List<List<double>> populationList, List<int> lowerLimits, List<int> upperLimits, int currentIteration, int maxIterations)
        {
            int numVar = lowerLimits.Count;
            int numObj = populationList.Count - numVar;
            int popSize = populationList[0].Count;

            //create the SMPSO algorithm
            Optimo_SMPSO._SMPSO algorithm = CreateAlgorithm(numObj, lowerLimits, upperLimits, popSize);

            //change solutions list to solutionSet
            SolutionSet population = SolutionListToPop(populationList, algorithm, numVar);
            
            Operator mutationOperator = algorithm.operators["mutation"];

            int populationSize = population.size();
            algorithm.swarmSize_ = populationSize;
            algorithm.archiveSize_ = populationSize;

            //Initialize Parameters
            algorithm.polynomialMutation_ = algorithm.operators_["mutation"];
            algorithm.particles_ = population;

            algorithm.best_ = new Solution[algorithm.swarmSize_];
            algorithm.leaders_ = new CrowdingArchive(algorithm.archiveSize_, algorithm.problem_.numberOfObjectives_);

            // Create comparators for dominance and crowding distance
            algorithm.dominance_ = new DominanceComparator();
            algorithm.crowdingDistanceComparator_ = new CrowdingDistanceComparator();

            // Create the speed_ vector
            algorithm.speed_ = new double[algorithm.swarmSize_, algorithm.problem_.numberOfVariables_];

            algorithm.deltaMax_ = new double[algorithm.problem_.numberOfVariables_];
            algorithm.deltaMin_ = new double[algorithm.problem_.numberOfVariables_];
            for (int i = 0; i < algorithm.problem_.numberOfVariables_; i++)
            {
                algorithm.deltaMax_[i] = (algorithm.problem_.upperLimit_[i] - algorithm.problem_.lowerLimit_[i]) / 2.0;
                algorithm.deltaMin_[i] = -algorithm.deltaMax_[i];

            }

            //Initialize the speed_ of each particle to 0
            for (int i = 0; i < algorithm.swarmSize_; i++)
            {
                for (int j = 0; j < algorithm.problem_.numberOfVariables_; j++)
                {
                    algorithm.speed_[i, j] = 0.0;
                }
            }

            //Initialize leader archive
            for (int i = 0; i < algorithm.particles_.size(); i++)
            {
                Solution particle = new Solution(algorithm.particles_[i]);
                algorithm.leaders_.add(particle);
            }

            //Initialize the memory of each particle
            for (int i = 0; i < algorithm.particles_.size(); i++)
            {
                Solution particle = new Solution(algorithm.particles_[i]);
                algorithm.best_[i] = particle;
            }

            //Assign the crowding distance to the leaders
            Distance.crowdingDistanceAssignment(algorithm.leaders_, algorithm.problem_.numberOfObjectives_);

            //Compute Speed
            algorithm.computeSpeed(currentIteration, maxIterations);

            //Compute the new positions for the particles_
            algorithm.computeNewPositions();

            //Mutate the particles_          
            algorithm.mopsoMutation(currentIteration, maxIterations); 
            
            return PopToSolutionList(algorithm.particles_); 
        }

        /// <summary>
        /// Sorts the combination of parents and child population set based on Pareto Fron Ranking.
        /// </summary>
        /// <param name="population">Parent solution set.</param>
        /// <param name="offspringPop">Offspring solution set.</param>
        /// <param name="lowerLimits">List of lower limits.</param>
        /// <param name="upperLimits">List of upper limits.</param>
        /// <returns></returns>
        public static List<List<double>> Sorting(List<List<double>> populationList, List<List<double>> offspringList, List<int> lowerLimits, List<int> upperLimits) //may not be necessary for SMPSO
        {
            int numVar = lowerLimits.Count;
            int numObj = populationList.Count - numVar;
            int popSize = populationList[0].Count;

            Optimo_SMPSO._SMPSO algorithm = CreateAlgorithm(numObj, lowerLimits, upperLimits, popSize);

            //change solutions list to solutionSet
            SolutionSet population = SolutionListToPop(populationList, algorithm, numVar);
            SolutionSet offspringPop = SolutionListToPop(offspringList, algorithm, numVar);
            SolutionSet best = new SolutionSet(popSize);
            SolutionSet bestTemp = new SolutionSet(popSize);

            //SolutionSet union = ((SolutionSet)population).union(offspringPop);

            algorithm.particles_ = population;
            algorithm.swarmSize_ = population.size();
            algorithm.best_ = new Solution[algorithm.swarmSize_];

            for (int i = 0; i < algorithm.swarmSize_; i++)
            {
                algorithm.best_[i] = offspringPop[i];
                bestTemp.add(offspringPop[i]);
            }

            //algorithm.leaders_ = new CrowdingArchive(algorithm.archiveSize_, algorithm.problem_.numberOfObjectives_);

            //Evaluate the new particles_ in new positions
            //for (int i = 0; i < algorithm.particles_.size(); i++)
            //{
                //Solution particle = algorithm.particles_[i];
                //algorithm.problem_.evaluate(particle);
            //}

            //Actualize the archive- dont need maybe?         
            /*for (int i = 0; i < algorithm.particles_.size(); i++)
            {
                Solution particle = new Solution(algorithm.particles_[i]);
                algorithm.leaders_ = new CrowdingArchive(algorithm.archiveSize_, algorithm.problem_.numberOfObjectives_); algorithm.leaders_.Add(particle);
            }*/

            //Assign the crowding distance to the leaders_
            Distance.crowdingDistanceAssignment(algorithm.particles_, algorithm.problem_.numberOfObjectives_);
            Distance.crowdingDistanceAssignment(bestTemp, algorithm.problem_.numberOfObjectives_);

            //Actualize the memory of this particle
            for (int i = 0; i < algorithm.particles_.size(); i++)
            {
                int flag = algorithm.dominance_.Compare(algorithm.particles_[i], bestTemp[i] /*algorithm.best_[i]*/);   //comparing children and parents//x is better than y if flag < 0, equal if flag = 0
                if (flag != 1)
                {
                    // the new particle is best_ than the older remeber        
                    Solution particle = new Solution(algorithm.particles_[i]);
                    //algorithm.best_[i] = particle;
                    best.add(algorithm.particles_[i]);   //adding to the output
                }
                else
                    best.add(algorithm.best_[i]);   //adding to the output
            }

            return PopToSolutionList(best);
        }
    }
}
