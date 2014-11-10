//
//  Operator.cs
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
  /// <summary>
  /// Uses class
  /// </summary>
  internal abstract class Operator
  {
    /// <summary>
    ///  Stores the current operator parameters. 
    ///  It is defined as a Map of pairs <<code>String</code>, <code>Object</code>>, 
    ///  and it allow objects to be accessed by their names, which  are specified 
    ///  by the string.
    /// </summary>
    //private Dictionary<String, Object> _parameters;
    public Dictionary<String, Object> parameters_ {get; set;}

    /// <summary>
    /// Operator name
    /// </summary>
    public String name_ {get ; set ;}        


    /// <summary>
    /// Constructor
    /// </summary>
    public Operator (Dictionary<string, object> parameters)
    {
      parameters_ = parameters ;
      name_ = "noname";
    }

    /// <summary>
    ///  Abstract method that must be defined by all the operators. When invoked, 
    ///  this method executes the operator represented by the current object.
    /// </summary>
    /// <param name="obj">This param inherits from Object to allow different kinds 
    /// of parameters for each operator.</param>
    /// <returns>An object reference. The returned value depens on the operator</returns>
    public abstract Object execute(Object obj);

    public override string ToString ()
    {
      String str = "";
      foreach (KeyValuePair<String, Object> kvp in parameters_)
        str += kvp.Key + " = " + kvp.Value + "\n";

      return str ;
    }
  }
}
