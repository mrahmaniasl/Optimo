using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Optimo_SMPSO
{

    internal abstract class Crossover : Optimo_SMPSO.Operator
  {
    public Crossover (Dictionary<string, object> parameters) : base(parameters)
    {
    } // Crossover
  }
}
