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

namespace PhaseToComments
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


            // Collect all of the walls in our Revit model
            IList<Element> walls
              = new FilteredElementCollector(doc)
                .WhereElementIsNotElementType()
                .OfCategory(BuiltInCategory.OST_Walls)
                .ToElements();

            // Define a transaction object assigned to the current Revit file (doc)
            Transaction setCommentsTransaction = new Transaction(doc);
            // Start the transaction with a name.
            setCommentsTransaction.Start("Setting the comments to phase created..");
            // Iterate through our wall collection. 
            foreach (Element wall in walls)
            {
                // Get the phase created parameter as a string or text value
                string phaseCreated = wall.get_Parameter(BuiltInParameter.PHASE_CREATED).AsValueString();
                // Set the comments parameter to the string value of the phase created parameter
                wall.get_Parameter(BuiltInParameter.ALL_MODEL_INSTANCE_COMMENTS).Set(phaseCreated);
            }
            // Finish (commit) the changes to the Revit database.
            setCommentsTransaction.Commit();


            return Result.Succeeded;
        }
    }
}
