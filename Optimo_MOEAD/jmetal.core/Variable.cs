//
//  Variable.cs
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
  internal abstract class Variable : ICloneable
  {
    public abstract Object Clone();
    /*
    public abstract double getValue();
    public abstract void setValue(double value);
    public abstract double getLowerBound();
    public abstract double getUpperBound();
    public abstract void setLowerBound(double lowerBound);
    public abstract void setUpperBound(double upperBound);
     * */

    public virtual double value_ { get; set ;}
    public virtual double lowerBound_ { get; set; }
    public virtual double upperBound_ { get; set; }

    public Type variableType {
      get
      {
        return this.GetType();
      }
    }
    /*
    public Type getVariableType()
    {
      return this.GetType();
    }
     */
    public override string ToString ()
    {
      return "" + value_ ;
    }
  }
}
