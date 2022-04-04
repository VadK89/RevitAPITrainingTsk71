using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitAPITrainingTsk71
{
    public class MainViewViewModel
    {

        private ExternalCommandData _commandData;
        private Document _doc;

        public List<FamilySymbol> TitleBlocks { get; }
        public List<ViewPlan> Views { get; }
        public FamilySymbol SelectedTitleBlock { get; set; }
        public View SelectedViews { get; set; }
        public DelegateCommand Creations { get; }
        public int ListNum { get; set; }
        public string DisignerName { get; set; }

        public MainViewViewModel(ExternalCommandData commandData)
        {
            _commandData = commandData;
            _doc = _commandData.Application.ActiveUIDocument.Document;
            TitleBlocks = TitleBlockUtils.GetTitleBlocks(commandData);
            Views = ViewsUtils.GetPlanViews(_doc);

            ListNum = 1;
            DisignerName = "Введите имя";
            Creations = new DelegateCommand(OnCreations);

        }

        private void OnCreations()
        {
            if (SelectedViews == null || SelectedTitleBlock == null)
                return;
            using (var ts = new Transaction(_doc, "Add List"))
            {
                ts.Start();
                for (int i = 0; i < ListNum; i++)
                {
                    ElementId viewPlan = (SelectedViews as View).Duplicate(ViewDuplicateOption.WithDetailing);
                ViewSheet viewList = ViewSheet.Create(_doc, SelectedTitleBlock.Id);
                    Parameter paramDisName = viewList.get_Parameter(BuiltInParameter.SHEET_DESIGNED_BY);
                    paramDisName.Set(DisignerName);
                    viewList.Name = "Создан через утилиту";
                    XYZ xYZ = new XYZ(1, 1, 0);
                    Viewport viewport = Viewport.Create(_doc, viewList.Id, viewPlan, xYZ);
                }
                ts.Commit();
            }


            RaiseCloseRequest();
            return;
        }




        //ViewPlan.Id;

        //для закрытия окна
        public event EventHandler CloseRequest;
        //метод для закрытия окна
        private void RaiseCloseRequest()
        {//Для запуска методов привзязанных к запросу закрытия
            CloseRequest?.Invoke(this, EventArgs.Empty);
        }

    }
}
