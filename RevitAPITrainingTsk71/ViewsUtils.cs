using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitAPITrainingTsk71
{
    public class ViewsUtils
    {
        public static List<ViewPlan> GetPlanViews(Document doc)
        {
            var views
               = new FilteredElementCollector(doc)
            //.OfCategory(BuiltInCategory.OST_Views)
            //.WhereElementIsNotElementType()
            //.Cast<View>()
            //.ToList();
            .OfClass(typeof(ViewPlan))
            .Cast<ViewPlan>()
            //.Where(p => p.ViewType == ViewType.FloorPlan)
            .ToList();
            return views;
        }
    }
}
