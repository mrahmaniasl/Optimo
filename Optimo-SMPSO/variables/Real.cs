using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using jmetal.core;
//using jmetal.util;

namespace Optimo_SMPSO
{
  internal class Real : Variable
  {
    // Value of the real variable
    //private double value_;
    public override double value_ {
      get ; set ;
    }

    // Lower bound of the real variable
    public override double lowerBound_
    {
      get ; set ;
    }

   // Upper bound of the real variable
    public override double upperBound_
    {
      get; set ;
    }

    /// <summary>
    /// Constructor
    /// </summary>
    public Real()
    {
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="lowerBound">Lowerbound value</param>
    /// <param name="upperBound">Upperbound value</param>
    public Real (double lowerBound, double upperBound)
    {
      lowerBound_ = lowerBound;
      upperBound_ = upperBound;
      value_ = PseudoRandom.Instance ().NextDouble () * (upperBound - lowerBound) + lowerBound;

      //Contract.Requires((value_ >= lowerBound_) && (value_ <= upperBound_));
    } //Real


    public Real(Variable variable) {
      lowerBound_ = variable.lowerBound_;
      upperBound_ = variable.upperBound_;
      value_ = variable.value_;

      //Contract.Requires((value_ >= lowerBound_) && (value_ <= upperBound_));
    } //Real

    public override object Clone ()
    {
      return new Real (this);
    }

    public override string ToString ()
    {
      string str = "";
      str += value_ ;
      return str ;
    }
  } // Real
}
