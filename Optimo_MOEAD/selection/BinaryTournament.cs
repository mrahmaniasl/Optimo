using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using jmetal.util;
//using jmetal.core;

namespace Optimo_MOEAD
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

    public override object execute (object obj)
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
      int sol1 = PseudoRandom.Instance ().Next (0, solutionSet.size () - 1);
      int sol2 = PseudoRandom.Instance ().Next (0, solutionSet.size () - 1);
      
      solution1 = solutionSet[sol1];
      solution2 = solutionSet[sol2];
      
      while (sol1 == sol2) {
        sol2 = PseudoRandom.Instance ().Next (0, solutionSet.size () - 1);
        solution2 = solutionSet[sol2];
      }

      int result = comparator_.Compare (solution1, solution2);
       if (result == -1)
        return solution1;
      else if (result == 1)
        return solution2;
      else {
        if (PseudoRandom.Instance ().NextDouble () < 0.5)
          return solution1;
        else
          return solution2;
      }
    }
  }
}
