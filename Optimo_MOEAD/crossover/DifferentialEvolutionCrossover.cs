//
//  DifferentialEvolutionCrossover.cs
//
//  Author:
//       Antonio J. Nebro <antonio@lcc.uma.es>
//
//  Copyright (c) 2011 Antonio J. Nebro
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU Lesser General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU Lesser General Public License for more details.
// 
//  You should have received a copy of the GNU Lesser General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using jmetal.core;
//using jmetal.util;
//using jmetal.operators.crossover;

namespace Optimo_MOEAD
{
  internal class DifferentialEvolutionCrossover : Crossover
  {
    private const double DEFAULT_CR = 0.5;
    private const double DEFAULT_F = 0.5;
    private const double DEFAULT_K = 0.5;
    private const string DEFAULT_DE_VARIANT = "rand/1/bin";

    public double CR_ { get; set; }
    public double F_ { get; set; }
    public double K_ { get; set; }
    public string DE_Variant_ { get; set; }


    private static Type[] validTypes = new Type[] { System.Type.GetType ("jmetal.encodings.solutionType.RealSolutionType") };

    public DifferentialEvolutionCrossover (Dictionary<string, object> parameters) : base(parameters)
    {
      // <pex>
      if (parameters == (Dictionary<string, object>)null)
        throw new ArgumentNullException ("parameters");
      // </pex>
      CR_ = DEFAULT_CR;
      F_ = DEFAULT_F;
      K_ = DEFAULT_K;
      DE_Variant_ = DEFAULT_DE_VARIANT;

      if (parameters.ContainsKey ("CR"))
        CR_ = (double)parameters["CR"];
      
      if (parameters.ContainsKey ("F"))
        F_ = (double)parameters["F"];

     if (parameters.ContainsKey ("K"))
        K_ = (double)parameters["K"];

      if (parameters.ContainsKey ("variant"))
        DE_Variant_ = (string)parameters["variant"];
    }

    public override object execute (object obj)
    {
      Object[] parameters = (Object[])obj;
      Solution current = (Solution)parameters[0];
      Solution[] parent = (Solution[])parameters[1];

      //if ((Array.Find (validTypes, n => n == parent[0].type_.GetType ()) == null) ||
      //    (Array.Find (validTypes, n => n == parent[1].type_.GetType ()) == null) ||
      //    (Array.Find (validTypes, n => n == parent[2].type_.GetType ()) == null))
      //  //Console.WriteLine ("DiffentialEvolution. Types are incompatible");
      //else
      //  ;
      //Console.WriteLine ("Differential evolution. Types are Compatible");

      int numberOfVariables = current.numberOfVariables_;
      double jrand = PseudoRandom.Instance ().Next (numberOfVariables);

      Solution child = new Solution (current);

      if (DE_Variant_.CompareTo ("rand/1/bin") == 0) {
        for (int j = 0; j < numberOfVariables; j++) {
          if (PseudoRandom.Instance ().NextDouble () < CR_ || j == jrand) {
            double val;
            val = parent[2].variable_[j].value_ + F_ * (parent[0].variable_[j].value_ - parent[1].variable_[j].value_);
            if (val < child.variable_[j].lowerBound_)
              val = child.variable_[j].value_ = child.variable_[j].lowerBound_;
            if (val > child.variable_[j].upperBound_)
              val = child.variable_[j].value_ = child.variable_[j].upperBound_;

            child.variable_[j].value_ = val;
          } else {
            child.variable_[j].value_ = current.variable_[j].value_ ;
        } // else
      } // for
    } // if
      return child;
    }
  }
}
