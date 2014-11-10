using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using jmetal.core;

namespace Optimo_Combined
{
  internal abstract class Settings
  {
    public Problem problem_ { get; set; }
    public String problenName_ { get; set; }
    public String encoding_ {get; set ;}
    public int numParams_ { get; set; }
    public int[] lowerLim_ { get; set; }
    public int[] upperLim_ { get; set; }
    public int numObj_ { get; set; }

    public Settings (String problemName, int numP, int[] lowerLim, int[] upperLim, int numObj, int popSize)
    {
      problem_ = null;
      problenName_ = problemName;
      encoding_ = null ;
      numParams_ = numP;
      lowerLim_ = lowerLim;
      upperLim_ = upperLim;
      numObj_ = numObj;
    }

    public abstract Algorithm configure(); 
  }
}
