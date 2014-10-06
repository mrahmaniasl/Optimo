using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Optimo
{

    internal abstract class Crossover : Optimo.Operator
  {
    public Crossover (Dictionary<string, object> parameters) : base(parameters)
    {
    } // Crossover
  }
}
