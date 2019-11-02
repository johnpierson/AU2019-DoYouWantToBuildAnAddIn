#region Namespaces
using System;
using System.Collections.Generic;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
#endregion

namespace BlockCADImport
{
    class App : IExternalApplication
    {
        public Result OnStartup(UIControlledApplication a)
        {
            // On the controlled application object, subscrive to the file importing event.
            a.ControlledApplication.FileImporting += ControlledApplication_FileImporting;
            return Result.Succeeded;
        }

        private void ControlledApplication_FileImporting(object sender, Autodesk.Revit.DB.Events.FileImportingEventArgs e)
        {
            // When the event fires off, see if the imported thing is a DWG. If it is, execute our logic
            if (e.Format is ImportExportFileFormat.DWG)
            {
                e.Cancel();
                TaskDialog.Show("CAD Import Failed", "Yeah, sorry no cad imports for you");
            }

        }

        public Result OnShutdown(UIControlledApplication a)
        {
            // For good measure, unsubscribe from the event when shutting down Revit.
            return Result.Succeeded;
        }
    }
}
