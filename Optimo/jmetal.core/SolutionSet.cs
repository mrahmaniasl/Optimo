//
//  SolutionSet.cs
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
using System.Collections;
using System.Collections.Generic;
using System.IO;

using System.Linq;
using System.Text;

namespace Optimo
{
  internal class SolutionSet : IEnumerable
  {
    private int capacity_ = 0;

    protected List<Solution> _solutionsList;
    public List<Solution> solutionList_ {
      get { return _solutionsList; }
    }

    /// <summary>
    /// Indexer 
    /// </summary>
    /// <param name="index">
    /// A <see cref="System.Int32"/>
    /// </param>
    public Solution this[int index] {
      get {
        if ((index >= solutionList_.Count) || (index < 0))
          throw new ArgumentNullException("Solution this[int index]"); 
        else
          return _solutionsList[index];
      }
      set {         
        if ((index >= solutionList_.Count) || (index < 0))
          throw new ArgumentNullException("Solution this[int index]"); 
        else
        _solutionsList[index] = value; }
    }

    /// <summary>
    /// Iterator
    /// </summary>
    /// <returns>
    /// A <see cref="IEnumerator"/>
    /// </returns>
    public IEnumerator GetEnumerator ()
    {
      for (int i = 0; i < _solutionsList.Count; i++) {
        yield return _solutionsList[i];
      }
    }

    /// <summary>
    /// Constructor
    /// </summary>
    public SolutionSet ()
    {
      _solutionsList = new List<Solution> ();
    }
    // SolutionSet
    /// <summary>
    /// Constructor 
    /// </summary>
    /// <param name="maximumSize">
    /// A <see cref="System.Int32"/>
    /// </param>
    public SolutionSet (int maximumSize)
    {
      _solutionsList = new List<Solution> ();
      capacity_ = maximumSize;
    }
    // SolutionSet
    /// <summary>
    /// Adds a new solution into the solution set
    /// </summary>
    /// <param name="solution">
    /// A <see cref="Solution"/>
    /// </param>
    /// <returns>
    /// A <see cref="System.Boolean"/>
    /// </returns>
    public bool add (Solution solution)
    {
      if (_solutionsList.Count == capacity_) {
        return false;
      // if
      } else {
        _solutionsList.Add (solution);
        return true;
      }
      // else
    }
    // add
    /// <summary>
    /// Returns the number of elements in the solution set
    /// </summary>
    /// <returns> Number of elements in the solution set
    /// A <see cref="System.Int32"/>
    /// </returns>
    public int size ()
    {
      return _solutionsList.Count;
    }
    // size
    /// <summary>
    /// Empties the solution set 
    /// </summary>
    public void clear ()
    {
      _solutionsList.Clear ();
    }

    /// <summary>
    /// Joins two solution sets: the current one and the one passed as argument
    /// </summary>
    /// <param name="solutionSet"> 
    /// A <see cref="SolutionSet"/>
    /// </param>
    /// <returns> A new solution set
    /// A <see cref="SolutionSet"/>
    /// </returns>
    public SolutionSet union (SolutionSet solutionSet)
    {
      // <pex>
      if (solutionSet == (SolutionSet)null)
        throw new ArgumentNullException("solutionSet");
      // </pex>
      //Check the correct size
      int newSize = this.size () + solutionSet.size ();
      if (newSize < capacity_)
        //////////////////
        newSize = capacity_;
      
      //Create a new population
      SolutionSet union = new SolutionSet (newSize);
      for (int i = 0; i < this.size (); i++) {
        union.add (this.solutionList_[i]);
      }
      // for
      for (int i = this.size (); i < (this.size () + solutionSet.size ()); i++) {
        union.add (solutionSet.solutionList_[i - this.size ()]);
      }
      // for
      return union;
    }
    // union

    public void replace(int position, Solution solution) {
    if (position > _solutionsList.Count) {
      _solutionsList.Add(solution);
    } // if
    _solutionsList.RemoveAt(position);
    _solutionsList.Insert(position,solution);
  } // replace


    public void printObjectivesToFile (String path)
    {
      // <pex>
      if (path == (string)null)
        throw new ArgumentNullException("path");
      if (path.Length == 0)
        throw new ArgumentException("path.Length == 0", "path");
      // </pex>
      System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("EN-us");  
      TextWriter tw = new StreamWriter (path);
      for (int i = 0; i < this.solutionList_.Count; i++) {
        for (int j = 0; j < this.solutionList_[i].numberOfObjectives_; j++)
          tw.Write (solutionList_[i].objective_[j].ToString(culture.NumberFormat) + " ");
        tw.WriteLine ();
      }
      
      tw.Close ();
    }
    // printObjectivesToFile
    public void printVariablesToFile (String path)
    {
      // <pex>
      if (path == (string)null)
        throw new ArgumentNullException("path");
      if (path.Length == 0)
        throw new ArgumentException("path.Length == 0", "path");
      // </pex>
      System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("EN-us");  
 
      TextWriter tw = new StreamWriter (path);
      for (int i = 0; i < this.solutionList_.Count; i++) {
        for (int j = 0; j < this.solutionList_[i].numberOfVariables_; j++)
          tw.Write (solutionList_[i].variable_[j].value_.ToString(culture.NumberFormat) + " ");
        tw.WriteLine ();
      }
      
      tw.Close ();
    }
    // printVariablesToFile

    public List<Solution> resultFunction()
    {
        return this._solutionsList;
    }
  }
  // SolutionSet
}
