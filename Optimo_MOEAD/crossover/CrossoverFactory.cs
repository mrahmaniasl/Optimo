//
//  CrossoverFactory.cs
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

namespace Optimo_MOEAD
{
  internal static class CrossoverFactory
  {

    public static Crossover getCrossoverOperator (String name, Dictionary<string, object> parameters)
    {
      // <pex>
      if (name == (string)null)
        throw new ArgumentNullException ("name");
      if (parameters == (Dictionary<string, object>)null)
        throw new ArgumentNullException ("parameters");
      // </pex>
      Crossover oper = null;
      if (name.ToUpper ().Equals ("SBXCrossover".ToUpper ())) {
        oper = new SBXCrossover (parameters);
      }
      else if (name.ToUpper ().Equals ("DifferentialEvolutionCrossover".ToUpper ())) {
        oper = new DifferentialEvolutionCrossover (parameters);
      }
      else {
        //System.Console.WriteLine ("Crossover object doesn't existtttttttttttttt");
        //throw new
      }
      return oper;
    }
  }
}
