using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Xml;
using DSCoreNodesUI;
using Dynamo.Models;
//using Autodesk.Revit.DB;
//using Autodesk.Revit.DB.Analysis;
using Selection;

namespace DropDown
{
    [NodeName("Algorithm Type Dropdown")]
    [NodeCategory("DropDown.Settings")]
    [NodeDescription("Select the algorithm you would like to use.")]
    [IsDesignScriptCompatible]
    public class AlgorithmTypeDropdown : EnumAsString<gbXMLAlgorithmType>
    {
        public AlgorithmTypeDropdown(WorkspaceModel workspace) : base(workspace) { }
    }
}
