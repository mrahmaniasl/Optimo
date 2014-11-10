using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using jmetal.core;
//using jmetal.encodings.variable;
//using jmetal.encodings.solutionType;

namespace Optimo_SMPSO
{
    internal class myTest : Problem
    {
        public myTest(String solutionType, int NumParam, int[] lowerLim, int[] upperLim, int numObj)
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
            sum1 = Math.Sqrt(Math.Pow((x[0].value_ - 2), 2) + Math.Pow((x[1].value_ - 3), 2));
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
            solution.objective_[1] = fx[1];
        }
        // evaluate
    }
}
