using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Optimo
{
  internal static class MutationFactory
  {

    public static Mutation getMutationOperator(String name, Dictionary<string, object> parameters)
    {
      if (name.ToUpper().Equals("PolynomialMutation".ToUpper()))
        return new PolynomialMutation(parameters);
      else
      {
        //System.Console.WriteLine("Mutation object doesn't exist");
        return null;
      }
    }
  }
}
