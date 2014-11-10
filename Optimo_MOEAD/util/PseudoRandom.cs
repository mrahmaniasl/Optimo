using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Optimo_MOEAD
{
  internal class PseudoRandom : Random
  {
    private static object mutex_ = new object();
    private static PseudoRandom PseudoRandom_ = null;

    private PseudoRandom()
    {
      // normal initialization, do not call Instance()
    }

    public static PseudoRandom Instance()
    {
      if (PseudoRandom_ == null)
      {
        lock (mutex_)
        {
          if (PseudoRandom_ == null)
            PseudoRandom_ = new PseudoRandom();
        }
      }
      return PseudoRandom_;
    }
  }
}
