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

namespace BuildASnowman
{
    [Transaction(TransactionMode.Manual)]
    public class Command : IExternalCommand
    {
        //https://thebuildingcoder.typepad.com/blog/2012/09/sphere-creation-for-avf-and-filtering.html
        public Result Execute(
          ExternalCommandData commandData,
          ref string message,
          ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;
            Document doc = uidoc.Document;

            Solid s1 = Command.CreateSphereAt(new XYZ(0, 0, 15), 15);

            Solid s2 = Command.CreateSphereAt(new XYZ(0,0, 32.5), 10);

            Solid s3 = Command.CreateSphereAt(new XYZ(0, 0, 47.5), 5);

            

            Transaction buildASnowman = new Transaction(doc, "Build a Snowman");
            buildASnowman.Start();
            var ds1 = DirectShape.CreateElement(doc, new ElementId(-2000151));
            ds1.SetShape(new GeometryObject[] {s1,s2,s3});

            buildASnowman.Commit();
            return Result.Succeeded;
        }
        /// <summary>
        /// Create and return a solid sphere with
        /// a given radius and centre point.
        /// </summary>
        public static Solid CreateSphereAt(XYZ centre, double radius)
        {
            // Use the standard global coordinate system 
            // as a frame, translated to the sphere centre.

            Frame frame = new Frame(centre,
                XYZ.BasisX, XYZ.BasisY, XYZ.BasisZ);

            // Create a vertical half-circle loop;
            // this must be in the frame location.

            Arc arc = Arc.Create(
                centre - radius * XYZ.BasisZ,
                centre + radius * XYZ.BasisZ,
                centre + radius * XYZ.BasisX);

            Line line = Line.CreateBound(
                arc.GetEndPoint(1),
                arc.GetEndPoint(0));

            CurveLoop halfCircle = new CurveLoop();
            halfCircle.Append(arc);
            halfCircle.Append(line);

            List<CurveLoop> loops = new List<CurveLoop>(1);
            loops.Add(halfCircle);

            return GeometryCreationUtilities
                .CreateRevolvedGeometry(
                    frame, loops, 0, 2 * Math.PI);
        }
    }
   
}
