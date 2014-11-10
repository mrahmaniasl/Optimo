using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Optimo_Combined
{

    internal abstract class Crossover : Optimo_Combined.Operator
  {
    public Crossover (Dictionary<string, object> parameters) : base(parameters)
    {
    } // Crossover
  }
}
