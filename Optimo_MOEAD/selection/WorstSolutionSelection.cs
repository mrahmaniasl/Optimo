using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using jmetal.core;
//using jmetal.util;

namespace Optimo_MOEAD
{
  internal class WorstSolutionSelection : Selection
  {
    public WorstSolutionSelection (Dictionary<string, object> parameters) : base(parameters)
    {
      // <pex>
      if (parameters == (Dictionary<string, object>)null)
        throw new ArgumentNullException("parameters");
      // </pex>
      if (parameters.ContainsKey ("comparator"))
        comparator_ = (IComparer)parameters["comparator"];
      
    }

    public override object execute (object obj)
    {
      // <pex>
      if (obj == (object)null)
        throw new ArgumentNullException("obj");
      if (obj != (object)null && !(obj is SolutionSet))
        throw new ArgumentException("complex reason", "obj");
      // </pex>
      //System.Console.WriteLine ("Creado un operador de seleccion del peor individuo \n");
      
      SolutionSet solutionSet = (SolutionSet)obj;
      
      if (solutionSet.size() == 0)
        return null;
      
      int worstSolution;
      
      worstSolution = 0;
      
      for (int i = 1; i < solutionSet.size (); i++) {
        if (comparator_.Compare (solutionSet[i], solutionSet[worstSolution]) > 0)
          worstSolution = i;
      }
      // for
      return worstSolution;

    }
  }
}
