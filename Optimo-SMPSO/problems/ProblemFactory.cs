//
//  ProblemFactory.cs
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
using System.Reflection;

//using jmetal.core;

namespace Optimo_SMPSO
{
  internal class ProblemFactory
  {
    public Problem getProblem (String name, Object parameters, int numParam, int[] lowerLimit, int[] upperLimit, int numObj/*, int popSize*/)
    {
      string problemName = "Optimo_SMPSO." + name;

      Type type = Type.GetType (problemName);

      Type[] types = new Type[5];
      //types[0] = typeof(String);
      types[0] = typeof(String);
      types[1] = typeof(int);  //Mohammad
      types[2] = typeof(int[]);
      types[3] = typeof(int[]);
      types[4] = typeof(int);
      //types[5] = typeof(int);

      ConstructorInfo ci = type.GetConstructor (types);
      var problem = ci.Invoke (new object[] { /*problemName, */(String)parameters, numParam, lowerLimit, upperLimit, numObj/*, popSize*/ });

      return (Problem)problem;
    }
  }
}
