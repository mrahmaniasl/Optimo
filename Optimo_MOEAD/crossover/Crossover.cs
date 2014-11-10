using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Optimo_MOEAD
{

    internal abstract class Crossover : Optimo_MOEAD.Operator
  {
    public Crossover (Dictionary<string, object> parameters) : base(parameters)
    {
    } // Crossover
  }
}
