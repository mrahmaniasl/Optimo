
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using jmetal.core;
//using jmetal.util;

namespace Optimo
{
  internal class PolynomialMutation : Mutation
  {

    public const double ETA_M_DEFAULT_ = 20.0;

    private static Type[] validTypes = new Type[] { System.Type.GetType ("jmetal.encodings.solutionType.RealSolutionType") };

    private double mutationProbability_ = 0.0;
    private double distributionIndex_ = ETA_M_DEFAULT_;

    public PolynomialMutation (Dictionary<string, object> parameters) : base(parameters)
    {
      //System.Console.WriteLine ("Creado un operador de mutation polinomial \n");

      //Console.WriteLine ("ValidTypes: " + validTypes[0]);
      if (parameters.ContainsKey ("probability"))
        mutationProbability_ = (double)parameters["probability"];
      
      if (parameters.ContainsKey ("distributionIndex"))
        distributionIndex_ = (double)parameters["distributionIndex"];
      
      //Console.WriteLine ("probability: " + mutationProbability_);
      //Console.WriteLine ("distributionIndex: " + distributionIndex_);
    }

    public override object execute (object obj)
    {

      Solution solution = (Solution)obj;
      
      Type type = Array.Find (validTypes, n => n == solution.type_.GetType ());
      //if (type == null)
      //  Console.WriteLine ("PolynomialMutation. Type incompatible");
      //else ;
        //Console.WriteLine ("PolynomialMutation. Type Compatible");

      doMutation (solution);
      
      return solution;
    }

    private void doMutation (Solution x)
    {
      double rnd, delta1, delta2, mut_pow, deltaq;
      double y, yl, yu, val, xy;
      double eta_m = distributionIndex_;
      
      for (int var = 0; var < x.numberOfVariables_; var++) {
        if (PseudoRandom.Instance ().NextDouble () <= mutationProbability_) {
          y = x.variable_[var].value_;
          yl = x.variable_[var].lowerBound_;
          yu = x.variable_[var].upperBound_;
          delta1 = (y - yl) / (yu - yl);
          delta2 = (yu - y) / (yu - yl);
          rnd = PseudoRandom.Instance ().NextDouble ();
          mut_pow = 1.0 / (eta_m + 1.0);
          if (rnd <= 0.5) {
            xy = 1.0 - delta1;
            val = 2.0 * rnd + (1.0 - 2.0 * rnd) * (Math.Pow (xy, (eta_m + 1.0)));
            deltaq = Math.Pow (val, mut_pow) - 1.0;
          } else {
            xy = 1.0 - delta2;
            val = 2.0 * (1.0 - rnd) + 2.0 * (rnd - 0.5) * (Math.Pow (xy, (eta_m + 1.0)));
            deltaq = 1.0 - (Math.Pow (val, mut_pow));
          }
          y = y + deltaq * (yu - yl);
          if (y < yl)
            y = yl;
          if (y > yu)
            y = yu;
          x.variable_[var].value_ = y;
        }
      }
    }
  }
}
