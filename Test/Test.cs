using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Optimo;
//using jmetal.core;


namespace Test
{
    class Test
    {
        static void Main(string[] args)
        {
            List<double> lowerLimits = new List<double>();
            lowerLimits.Add(-10); lowerLimits.Add(-10);

            List<double> upperLimits = new List<double>();
            upperLimits.Add(10); upperLimits.Add(10);

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

            List<List<double>> fitnessList = new List<List<double>>();
            fitnessList.Add(populationList[2]);
            fitnessList.Add(populationList[3]);


            Optimo.NSGA_II.InitialSolutionList(10, 2, lowerLimits, upperLimits);

            Optimo.NSGA_II.AssignFitnessFuncResults(populationList, fitnessList);

            Optimo.NSGA_II.Sorting(populationList, Optimo.NSGA_II.GenerationAlgorithm(populationList, lowerLimits, upperLimits), lowerLimits, upperLimits);


            Optimo.NSGA_II.GenerationAlgorithm(populationList, lowerLimits, upperLimits);
        }
    }
}
