using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using Optimo;
//using jmetal.core;
//using Optimo_SMPSO;
//using Optimo_MOEAD;
using Optimo_Combined;


namespace Test
{
    class Test
    {
        static void Main(string[] args)
        {
            /////////////////////////////////////////////////////
            //Setting up initial population (not used later)
            /////////////////////////////////////////////////////

            List<int> lowerLimits = new List<int>();
            lowerLimits.Add(-10); //lowerLimits.Add(-10); lowerLimits.Add(-10);

            List<int> upperLimits = new List<int>();
            upperLimits.Add(10); //upperLimits.Add(10); upperLimits.Add(10);

            List<List<double>> populationList = new List<List<double>>();
            List<double> popLst; 
            popLst = new List<double>();
            popLst.Add(-1); popLst.Add(-2); popLst.Add(-3);
            populationList.Add(popLst);

            popLst = new List<double>();
            popLst.Add(1); popLst.Add(2); popLst.Add(3);
            populationList.Add(popLst);

            popLst = new List<double>();
            popLst.Add(4); popLst.Add(5); popLst.Add(6);
            populationList.Add(popLst);

            popLst = new List<double>();
            popLst.Add(7); popLst.Add(8); popLst.Add(9);
            populationList.Add(popLst);

            popLst = new List<double>();
            popLst.Add(10); popLst.Add(11); popLst.Add(12);
            populationList.Add(popLst);

            List<List<double>> fitnessList = new List<List<double>>();
            fitnessList.Add(populationList[2]);
            fitnessList.Add(populationList[3]);

            List<List<double>> p = new List<List<double>>();
            List<List<double>> s = new List<List<double>>();

            /////////////////////////////////////////////////////
            //NSGA-II Algorithm
            /////////////////////////////////////////////////////

            /*Optimo.NSGA_II.InitialSolutionList(10, 2, lowerLimits, upperLimits);

            Optimo.NSGA_II.AssignFitnessFuncResults(populationList, fitnessList);

            Optimo.NSGA_II.Sorting(populationList, Optimo.NSGA_II.GenerationAlgorithm(populationList, lowerLimits, upperLimits), lowerLimits, upperLimits);

            Optimo.NSGA_II.GenerationAlgorithm(populationList, lowerLimits, upperLimits);

            /////////////////////////////////////////////////////
            //SMPSO Algorithm
            /////////////////////////////////////////////////////

            populationList = Optimo_SMPSO.SMPSO_.InitialSolutionList(10, 2, lowerLimits, upperLimits);

            populationList = Optimo_SMPSO.SMPSO_.AssignFitnessFuncResults(populationList, fitnessList);

            p = Optimo_SMPSO.SMPSO_.GenerationAlgorithm(populationList, lowerLimits, upperLimits, 0, 1);

            s = Optimo_SMPSO.SMPSO_.Sorting(populationList, p, lowerLimits, upperLimits);

            /////////////////////////////////////////////////////
            //MOEAD Algorithm
            /////////////////////////////////////////////////////
             
            populationList = Optimo_MOEAD.MOEAD_.InitialSolutionList(10, 2, lowerLimits, upperLimits);

            Optimo_MOEAD.MOEAD_.AssignFitnessFuncResults(populationList, fitnessList);
             
            p = Optimo_MOEAD.MOEAD_.GenerationAlgorithm(populationList, lowerLimits, upperLimits);

            s = Optimo_MOEAD.MOEAD_.Sorting(populationList, p, lowerLimits, upperLimits);*/

            /////////////////////////////////////////////////////
            //Combined Algorithms
            /////////////////////////////////////////////////////

            populationList = Optimo_Combined.Combined_.InitialSolutionList(10, 2, lowerLimits, upperLimits, "MOEAD");

            //Optimo_Combined.Combined_.AssignFitnessFuncResults(populationList, fitnessList);

            p = Optimo_Combined.Combined_.GenerationAlgorithm(populationList, lowerLimits, upperLimits, 0, 1, "MOEAD");

            s = Optimo_Combined.Combined_.Sorting(populationList, p, lowerLimits, upperLimits, "MOEAD");

            /////////////////////////////////////////////////////
            //Output Test
            /////////////////////////////////////////////////////

            Console.WriteLine("Initial Population List: \n");

            for (int i = 0; i < populationList.Count(); i++)
            {
                for (int j = 0; j < populationList[i].Count(); j++)
                {
                    Console.WriteLine(populationList[i][j] + '\n');
                }
                Console.WriteLine('\n');
            }

            Console.WriteLine("Output of generation algorithm: \n");

            for (int i = 0; i < p.Count(); i++)
            {
                for (int j = 0; j < p[i].Count(); j++)
                {
                    Console.WriteLine(p[i][j] + '\n');
                }
                Console.WriteLine('\n');
            }

            Console.WriteLine("Output of sorting algorithm: \n");

            for (int i = 0; i < s.Count(); i++)
            {
                for (int j = 0; j < s[i].Count(); j++)
                {
                    Console.WriteLine(s[i][j] + '\n');
                }
                Console.WriteLine('\n');
            }

            Console.Read();
        }
    }
}
