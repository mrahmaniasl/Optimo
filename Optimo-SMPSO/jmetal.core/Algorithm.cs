//
//  Algorithm.cs
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

namespace Optimo_SMPSO
{
  /// <summary>
  /// Abstract lass presenting an algorithm
  /// </summary>
  internal abstract class Algorithm
  {
    public Problem problem_ {
      get; set;
    }

    /// <summary>
    ///  Stores the operators used by the algorithm, such as selection, crossover, etc.
    /// </summary>
    public Dictionary<String, Operator> operators_;
    public Dictionary<String, Operator> operators
    {
      get {return operators_ ;}
    }


    /// <summary>
    /// Stores algorithm specific parameters
    /// </summary>
    protected Dictionary<String, Object> _inputParameters;
    public Dictionary<String, Object> inputParameters_
    {
      get { return _inputParameters;}
    }

    /// <summary>
    /// Stores output parameters, returned by the algorithm
    /// </summary>
    protected Dictionary<String, Object> _outputParameters;
    public Dictionary<String, Object> outputParameters_
    {
      get { return _outputParameters;}
    }


  /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="problem">
    /// A <see cref="Problem"/>
    /// </param>
    public Algorithm (Problem problem):this()
    {
      problem_ = problem;
    }

     /// <summary>
    /// Constructor
    /// </summary>
    public Algorithm ()
    {
      operators_ = new Dictionary<string, Operator> ();
      _inputParameters = new Dictionary<string, Object> ();
      _outputParameters = new Dictionary<string, Object> ();
    }

    /// <summary>
    /// Abstract method to execute the algorithm
    /// </summary>
    /// <returns></returns>
    public abstract SolutionSet execute();
  }
}
