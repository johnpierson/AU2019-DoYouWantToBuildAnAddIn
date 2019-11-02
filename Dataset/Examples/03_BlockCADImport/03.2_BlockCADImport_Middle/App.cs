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
            a.ControlledApplication.FileImporting += ControlledApplication_FileImporting;      
            return Result.Succeeded;
        }

        private void ControlledApplication_FileImporting(object sender, Autodesk.Revit.DB.Events.FileImportingEventArgs e)
        {
            
            throw new NotImplementedException();
        }

        public Result OnShutdown(UIControlledApplication a)
        {
            return Result.Succeeded;
        }
    }
}
