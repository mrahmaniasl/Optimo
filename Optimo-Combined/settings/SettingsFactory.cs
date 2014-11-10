using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
//using jmetal.core;

namespace Optimo_Combined
{
    internal class SettingsFactory
    {
        public Settings getSettingsObject(string algorithmName, string problemName, int NumParam, int[] lowerLim, int[] upperLim, int numObj, int popSize)
        {
            string str = "Optimo_Combined." + algorithmName + "_settings";

            Type type = Type.GetType(str);
            Type[] types = new Type[6];
            types[0] = typeof(String);
            types[1] = typeof(int); //Mohammad
            types[2] = typeof(int[]);
            types[3] = typeof(int[]);
            types[4] = typeof(int);
            types[5] = typeof(int);

            ConstructorInfo ci = type.GetConstructor(types);

            var settingsObject = ci.Invoke(new object[] { problemName,  NumParam, lowerLim, upperLim, numObj, popSize });

            //Settings settings = type.GetConstructor(null);
            return (Settings)settingsObject;
        }
    }
}
