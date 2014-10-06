using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using jmetal.core;
//using jmetal.encodings.variable;

namespace Optimo
{
  internal class RealSolutionType : SolutionType
  {
    public RealSolutionType (Problem problem) : base(problem)
    {
      // <pex>
      if (problem == (Problem)null)
        throw new ArgumentNullException("problem");
      // </pex>
      problem.variableType_ = new Type[problem.numberOfVariables_];
      problem.solutionType_ = this;
      
      // Initializing the types of the variables
      for (int i = 0; i < problem.numberOfVariables_; i++) {
        problem.variableType_[i] = System.Type.GetType ("jmetal.core.variable.Real");
        //problem.variableType_[i] = this.GetType ();
      }
      // for
    }
    // RealSolutionType  
    public override Variable[] createVariables ()
    {
      Variable[] variables = new Variable[problem_.numberOfVariables_];
      
      for (int var = 0; var < problem_.numberOfVariables_; var++)
        variables[var] = new Real (problem_.lowerLimit_[var], problem_.upperLimit_[var]);

      return variables;
    }
  }
}
