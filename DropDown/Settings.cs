using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Xml;

//Revit & Dynamo 
//using Autodesk.Revit.DB;
//using Autodesk.Revit.DB.Analysis;
//using Autodesk.Revit.UI;
using DSCore;
using DSCoreNodesUI;
using Dynamo.Models;
using Dynamo.Nodes;
using Dynamo.Utilities;
using ProtoCore.AST.AssociativeAST;
//using RevitServices.Persistence;
//using RevitServices.Transactions;
using ProtoCore;
using ProtoCore.Utils;
//using Autodesk.DesignScript.Runtime;
//using Revit.GeometryConversion;


namespace DropDown
{
    public static class Settings
    {
        /// <summary>
        /// Sets the Algorithm Settings
        /// </summary>
        /// <param name="AlgTyp"> Input Algorithm Type </param>
        /// <returns></returns>
        //[MultiReturn("EnergySettings", "report")]
        public static Dictionary<string, object> SetAlgSettings(string AlgTyp = "")
        {
            string n = "null";
            if (!string.IsNullOrEmpty(AlgTyp))
            {
                Selection.gbXMLAlgorithmType type;
                try
                {
                    type = (Selection.gbXMLAlgorithmType)Enum.Parse(typeof(Selection.gbXMLAlgorithmType), AlgTyp);
                }
                catch (Exception)
                {
                    throw new Exception("Building type is not found");
                }
            }

            return new Dictionary<string, object> { { "null", n } };

        }

    }
}
