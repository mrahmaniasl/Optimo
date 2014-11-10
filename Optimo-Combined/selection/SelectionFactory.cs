using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Optimo_Combined
{
  internal static class SelectionFactory
  {

    public static Selection getSelectionOperator(String name, Dictionary<string, object> parameters)
    {
      if (name.ToUpper().Equals("BinaryTournament".ToUpper()))
        return new BinaryTournament(parameters);
      else
      {
        //System.Console.WriteLine("Selecion object doesn't exist");
        return null;
      }
    }
  }
}
