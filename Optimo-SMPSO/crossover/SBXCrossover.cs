//
//  SBXCrossover.cs
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
//using jmetal.core ;
//using jmetal.util;

namespace Optimo_SMPSO
{
  internal class SBXCrossover : Crossover
  {
    public const double ETA_C_DEFAULT_ = 20.0;

    // EPS defines the minimum difference allowed between real values
    private const double EPS = 1.0e-14;

    private static Type[] validTypes = new Type[] { System.Type.GetType ("jmetal.encodings.solutionType.RealSolutionType") };

    private double crossoverProbability_ = 0.0;
    private double distributionIndex_ = ETA_C_DEFAULT_;

    public SBXCrossover (Dictionary<string, object> parameters) : base(parameters)
    {
      // <pex>
      if (parameters == (Dictionary<string, object>)null)
        throw new ArgumentNullException("parameters");
      // </pex>
      if (parameters.ContainsKey ("probability"))
        crossoverProbability_ = (double)parameters["probability"];
      
      if (parameters.ContainsKey ("distributionIndex"))
        distributionIndex_ = (double)parameters["distributionIndex"];
      
      //Console.WriteLine ("SBX probability: " + crossoverProbability_);
      //Console.WriteLine ("SBX distributionIndex: " + distributionIndex_);
    }

    public override object execute (object obj)
    {
      Solution[] parents = (Solution[])obj;

      //if ((Array.Find (validTypes, n => n == parents[0].type_.GetType ()) == null) ||
      //    (Array.Find (validTypes, n => n == parents[1].type_.GetType ()) == null))
      //  Console.WriteLine ("SBX. Types are incompatible");
      //else ;
      //  //Console.WriteLine ("SBX. Types are Compatible");
      
      Solution[] offspring;
      offspring = doCrossover (parents[0], parents[1]);
            
      return offspring;
    }

    Solution[] doCrossover (Solution parent1, Solution parent2)
    {
      Solution[] offSpring = new Solution[2];
      
      offSpring[0] = new Solution (parent1);
      offSpring[1] = new Solution (parent2);
      
      int i;
      double rand;
      double y1, y2, yL, yu;
      double c1, c2;
      double alpha, beta, betaq;
      double valueX1, valueX2;
      Solution x1 = parent1;
      Solution x2 = parent2;
      Solution offs1 = offSpring[0];
      Solution offs2 = offSpring[1];
      
      
      int numberOfVariables = x1.numberOfVariables_;
      
      if (PseudoRandom.Instance ().NextDouble () <= crossoverProbability_) {
        for (i = 0; i < numberOfVariables; i++) {
          valueX1 = x1.variable_[i].value_;
          valueX2 = x2.variable_[i].value_;
          if (PseudoRandom.Instance ().NextDouble () <= 0.5) {
            if (Math.Abs (valueX1 - valueX2) > EPS) {
              
              if (valueX1 < valueX2) {
                y1 = valueX1;
                y2 = valueX2;
              } else {
                y1 = valueX2;
                y2 = valueX1;
              }
              // if
              yL = x1.variable_[i].lowerBound_;
              yu = x1.variable_[i].upperBound_;
              rand = PseudoRandom.Instance ().NextDouble ();
              beta = 1.0 + (2.0 * (y1 - yL) / (y2 - y1));
              alpha = 2.0 - Math.Pow (beta, -(distributionIndex_ + 1.0));
              
              if (rand <= (1.0 / alpha)) {
                betaq = Math.Pow ((rand * alpha), (1.0 / (distributionIndex_ + 1.0)));
              } else {
                betaq = Math.Pow ((1.0 / (2.0 - rand * alpha)), (1.0 / (distributionIndex_ + 1.0)));
              }
              // if
              c1 = 0.5 * ((y1 + y2) - betaq * (y2 - y1));
              beta = 1.0 + (2.0 * (yu - y2) / (y2 - y1));
              alpha = 2.0 - Math.Pow (beta, -(distributionIndex_ + 1.0));
              
              if (rand <= (1.0 / alpha)) {
                betaq = Math.Pow ((rand * alpha), (1.0 / (distributionIndex_ + 1.0)));
              } else {
                betaq = Math.Pow ((1.0 / (2.0 - rand * alpha)), (1.0 / (distributionIndex_ + 1.0)));
              }
              // if
              c2 = 0.5 * ((y1 + y2) + betaq * (y2 - y1));
              
              if (c1 < yL)
                c1 = yL;
              
              if (c2 < yL)
                c2 = yL;
              
              if (c1 > yu)
                c1 = yu;
              
              if (c2 > yu)
                c2 = yu;
              
              if (PseudoRandom.Instance ().NextDouble () <= 0.5) {
                offs1.variable_[i].value_ = c2;
                offs2.variable_[i].value_ = c1;
              } else {
                offs1.variable_[i].value_ = c1;
                offs2.variable_[i].value_ = c2;
              }
              // if
            } else {
              offs1.variable_[i].value_ = valueX1;
              offs2.variable_[i].value_ = valueX2;
            }
            // if
          } else {
            offs1.variable_[i].value_ = valueX2;
            offs2.variable_[i].value_ = valueX1;
          }
          // if
        }
        // if
      }
      // if
      return offSpring;
    }
  }
  
}
