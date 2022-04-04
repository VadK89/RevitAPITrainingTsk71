using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace RevitAPITrainingTsk71
{
    public class TitleBlockUtils
    {
        public static List<FamilySymbol> GetTitleBlocks(ExternalCommandData commandData)
        {
            var uiapp = commandData.Application;
            var uidoc = uiapp.ActiveUIDocument;
            var doc = uidoc.Document;
            var titleBlocks
               = new FilteredElementCollector(doc)
                   .OfClass(typeof(FamilySymbol))
                   .OfCategory(BuiltInCategory.OST_TitleBlocks)
                   .Cast<FamilySymbol>()
                   .ToList();
            return titleBlocks;
        }
    }
}
