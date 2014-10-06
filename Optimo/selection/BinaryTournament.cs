//  Developed based on BinaryTournament.cs, which was created by 
//  Antonio J. Nebro <antonio@lcc.uma.es>
//
//  Development team:
//       Mohammad Rahmani Asl <mrah@tamu.edu>
//       Alexander Stoupine <astoupine1@tamu.edu> 
//       Wei Yan <wyan@tamu.edu>
//       at BIM-SIM Group, College of Architecture, Texas A&M University.
//
//  Below is the original comment header of BinaryTournament.cs
//
//  BinaryTournament.cs
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
//using jmetal.util;
//using jmetal.core;

namespace Optimo
{
  internal class BinaryTournament : Selection
  {
    public BinaryTournament (Dictionary<string, object> parameters) : base(parameters)
    {
      // <pex>
      //if (parameters == (Dictionary<string, object>)null)
      //  throw new ArgumentNullException("parameters");
      // </pex>
      //System.Console.WriteLine ("Creado un operador de seleccion por torneo binario \n");
    }

    public override object execute(object obj)
    {
        // <pex>
        if (obj == (object)null)
            throw new ArgumentNullException("obj");
        if (obj != (object)null && !(obj is SolutionSet))
            throw new ArgumentException("complex reason", "obj");
        // </pex>

        SolutionSet solutionSet = (SolutionSet)obj;
        Solution solution1;
        Solution solution2;


        ///// OJO, FALTA CONTROLAR SI EL SOLUTION ESTA VACIO O TIENE UN SOLO ELEMENTO
        int sol1 = PseudoRandom.Instance().Next(0, solutionSet.size() - 1);
        int sol2 = PseudoRandom.Instance().Next(0, solutionSet.size() - 1);
        int sol3 = PseudoRandom.Instance().Next(0, solutionSet.size() - 1);

        solution1 = solutionSet[sol1];
        solution2 = solutionSet[sol2];

        while (sol1 == sol2)
        {
            sol2 = PseudoRandom.Instance().Next(0, solutionSet.size() - 1);
            solution2 = solutionSet[sol2];
        }

        int result = comparator_.Compare(solution1, solution2);
        if (result == -1)
            return solution1;
        else if (result == 1)
            return solution2;
        else
        {
            if (PseudoRandom.Instance().NextDouble() < 0.5)
                return solution1;
            else
                return solution2;
        }
    }

    //Test to increase the convergence rate by Mohammad.
    //public override object execute(object obj)
    //{
    //    // <pex>
    //    if (obj == (object)null)
    //        throw new ArgumentNullException("obj");
    //    if (obj != (object)null && !(obj is SolutionSet))
    //        throw new ArgumentException("complex reason", "obj");
    //    // </pex>

    //    SolutionSet solutionSet = (SolutionSet)obj;
    //    Solution solution1;
    //    Solution solution2;
    //    Solution solution3;
    //    /*Mohammad*/
    //    Solution solution;


    //    ///// OJO, FALTA CONTROLAR SI EL SOLUTION ESTA VACIO O TIENE UN SOLO ELEMENTO
    //    int sol1 = PseudoRandom.Instance().Next(0, solutionSet.size() - 1);
    //    int sol2 = PseudoRandom.Instance().Next(0, solutionSet.size() - 1);
    //    /*Mohammad*/
    //    int sol3 = PseudoRandom.Instance().Next(0, solutionSet.size() - 1);
    //    int sol = 0;

    //    solution1 = solutionSet[sol1];
    //    solution2 = solutionSet[sol2];
    //    /*Mohammad*/
    //    solution3 = solutionSet[sol3];

    //    while (sol1 == sol2)
    //    {
    //        sol2 = PseudoRandom.Instance().Next(0, solutionSet.size() - 1);
    //        solution2 = solutionSet[sol2];
    //    }

    //    int result = comparator_.Compare(solution1, solution2);
    //    if (result == -1)
    //    {
    //        solution = solution1;
    //        sol = sol1;
    //        //return solution1;
    //    }
    //    else if (result == 1)
    //    {
    //        solution = solution2;
    //        sol = sol2;
    //        //return solution2;
    //    }
    //    else
    //    {
    //        if (PseudoRandom.Instance().NextDouble() < 0.5)
    //        {
    //            solution = solution1;
    //            sol = sol1;
    //            //return solution1;
    //        }
    //        else
    //        {
    //            solution = solution2;
    //            sol = sol2;
    //            //return solution2;
    //        }
    //    }

    //    while (sol3 == sol)
    //    {
    //        sol3 = PseudoRandom.Instance().Next(0, solutionSet.size() - 1);
    //        solution3 = solutionSet[sol3];
    //    }

    //    result = comparator_.Compare(solution, solution3);
    //    if (result == -1)
    //    {
    //        return solution;
    //    }
    //    else if (result == 1)
    //    {
    //        return solution3;
    //    }
    //    else
    //    {
    //        if (PseudoRandom.Instance().NextDouble() < 0.5)
    //        {
    //            return solution;
    //        }
    //        else
    //        {
    //            return solution3;
    //        }
    //    }
    //}
  }
}
