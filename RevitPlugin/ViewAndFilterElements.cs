using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.UI;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;


namespace RevitPlugin
{
    [Transaction(TransactionMode.Manual)]
    public class ViewAndFilterElements : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            try
            {
                // current doc
                Document document = commandData.Application.ActiveUIDocument.Document;

                return Result.Succeeded;
            }

            catch (Exception ex)
            {
                return Result.Failed;
            }
        }
    }
}
