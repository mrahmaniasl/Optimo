using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using jmetal.core;
//using jmetal.operators.comparator;

namespace Optimo_SMPSO
{
  internal abstract class Selection : Operator
  {
    private IComparer defaultComparator = new DominanceComparator() ;
    public IComparer comparator_
    {
      get { return defaultComparator; }
      set { defaultComparator = value; }
    }

    public Selection (Dictionary<string, object> parameters):base(parameters)
    {
    }
  }
}
