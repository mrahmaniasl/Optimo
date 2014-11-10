//
//  toArr.cs
//
//  Author:
//      Mohammad Rahmani Asl <mra1242@neo.tamu.edu>
//
//  Copyright (c) 2014 Mohammad Rahmani Asl
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

//This file contains functions to convert python lists into arrays in Dynamo


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using jmetal.core;

namespace Optimo_SMPSO
{
    internal class toArray
    {
        public Solution[] toArr(Solution o1, Solution o2)
        {
            Solution[] obArr = { o1, o2 };
            return obArr;
        }

        public Solution select(SolutionSet pop)
        {
            int popSize = pop.size();
            Random rnd = new Random();
            int r = rnd.Next(1, popSize);
            Solution sl = pop[r];
            System.Threading.Thread.Sleep(50);
            return sl;
        }
    }
}
