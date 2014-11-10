using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using jmetal.core;

namespace Optimo_MOEAD
{
  internal abstract class Mutation : Operator 
  {
    public Mutation(Dictionary<string, object> parameters):base(parameters)
    {
    } // Mutation
  }
}
