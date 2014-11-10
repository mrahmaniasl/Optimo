using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using jmetal.core;
//using jmetal.encodings.variable;
using System.Diagnostics;

namespace Optimo_SMPSO
{
  class BinarySolutionType : SolutionType
  {
    public BinarySolutionType(Problem problem) : base(problem)
    {
      // <pex>
      if (problem == (Problem)null)
        throw new ArgumentNullException("problem");
      // </pex>
      problem.variableType_ = new Type[problem.numberOfVariables_];
      problem.solutionType_ = this;
      
      // Initializing the types of the variables
      for (int i = 0; i < problem.numberOfVariables_; i++) {
        problem.variableType_[i] = System.Type.GetType ("jmetal.core.variable.Binary");
        //problem.variableType_[i] = this.GetType ();
      }
      // for
    }

    public override Variable[] createVariables()
    {
      Variable[] variables = new Variable[problem_.numberOfVariables_];

      for (int var = 0; var < problem_.numberOfVariables_; var++)
        variables[var] = new Binary(problem_.getLength(var));

      return variables;
    }
  }
}
