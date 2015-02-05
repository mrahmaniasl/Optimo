//  Developed based on myTest.cs, which was created by 
//  Antonio J. Nebro <antonio@lcc.uma.es>
//
//  Development team:
//       Mohammad Rahmani Asl <mrah@tamu.edu>
//       Alexander Stoupine <astoupine1@tamu.edu> 
//       Wei Yan <wyan@tamu.edu>
//       at BIM-SIM Group, College of Architecture, Texas A&M University.
//
//  Below is the original comment header of myTets.cs
//
//  myTest.cs
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
//using jmetal.encodings.variable;
//using jmetal.encodings.solutionType;

namespace Optimo
{
    internal class myTest : Problem
    {
        public myTest(String solutionType, int NumParam, double[] lowerLim, double[] upperLim, int numObj)
        {
            numberOfVariables_ = NumParam;
            numberOfObjectives_ = numObj;
            numberOfConstraints_ = 0;
            problemName_ = "myTest";

            lowerLimit_ = new double[numberOfVariables_];
            upperLimit_ = new double[numberOfVariables_];
            for (int i = 0; i < numberOfVariables_; i++)
            {
                lowerLimit_[i] = lowerLim[i];
                upperLimit_[i] = upperLim[i];
            }

            if (solutionType == "Real")
                solutionType_ = new RealSolutionType(this);
        }

        public override void evaluate(Solution solution)
        {
            // <pex>
            if (solution == (Solution)null)
                throw new ArgumentNullException("solution");
            if (solution.variable_ == (Variable[])null)
                throw new ArgumentNullException("solution");
            // </pex>
            Variable[] x = solution.variable_;
            double[] fx = new double[numberOfObjectives_];

            double sum1 = 0.0;
            sum1 = Math.Sqrt(Math.Pow((x[0].value_ -2),2) + Math.Pow((x[1].value_ -3) ,2)) ;
            fx[0] = sum1;

            //for (int var = 0; var < numberOfVariables_; var++)
            //{
            //    sum1 += Math.Pow(x[var].value_
            //      - (1.0 / Math.Sqrt((double)numberOfVariables_)), 2.0);
            //}
            //double exp1 = Math.Exp((-1.0) * sum1);
            //fx[0] = 1 - exp1;

            //double sum2 = 0.0;
            //sum2 = (x[0].value_ + x[1].value_ + x[2].value_)*(-2);
            //fx[1] = sum2;

            //double sum2 = 0.0;
            //for (int var = 0; var < numberOfVariables_; var++)
            //{
            //    sum2 += Math.Pow(x[var].value_
            //        + (1.0 / Math.Sqrt((double)numberOfVariables_)), 2.0);
            //}
            //double exp2 = Math.Exp((-1.0) * sum2);
            //fx[1] = 1 - exp2;

            solution.objective_[0] = fx[0];
            //solution.objective_[1] = fx[1];
        }
        // evaluate
    }
}
