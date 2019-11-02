#region Namespaces
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
#endregion

namespace HelloWorld
{
    [Transaction(TransactionMode.Manual)]
    public class Command : IExternalCommand
    {
        public Result Execute(
          ExternalCommandData commandData,
          ref string message,
          ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;
            Document doc = uidoc.Document;

            // Access current selection

            Selection sel = uidoc.Selection;

            //using the selection method, ask user to pick one object and get it's element id
            ElementId selectedElementId = sel.PickObject(ObjectType.Element).ElementId;

            //get the element from the database given the ElementId
            Element selectedElement = doc.GetElement(selectedElementId);

            //obtain a parameter's value given the name to display the value to the user
            string unconnectedHeight = selectedElement.LookupParameter("Unconnected Height").AsValueString();

            //show our dialog box with a result derived from an element
            Autodesk.Revit.UI.TaskDialog.Show("Hello world!", unconnectedHeight);

            return Result.Succeeded;
        }
    }
}
