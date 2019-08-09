using System;
using Octopus.Core.Data;
using Rhino;
using Rhino.Commands;
using Rhino.Input;

namespace Octopus.Commands
{
    public class OcCreateRectangle : Command
    {
        static OcCreateRectangle _instance;
        public OcCreateRectangle()
        {
            _instance = this;
        }

        ///<summary>The only instance of the OcCreateRectangle command.</summary>
        public static OcCreateRectangle Instance
        {
            get { return _instance; }
        }

        public override string EnglishName
        {
            get { return "OcCreateRectangle"; }
        }

        protected override Result RunCommand(RhinoDoc doc, RunMode mode)
        {
            // get plane
            var rc = RhinoGet.GetPlane(out var plane);
            if (rc != Result.Success) return rc;

            // get width
            double width = 0;
            rc = RhinoGet.GetNumber("Specify width", false, ref width, 0.1, 1000000);
            if (rc != Result.Success) return rc;

            // get height
            double height = 0;
            rc = RhinoGet.GetNumber("Specify height", false, ref height, 0.1, 1000000);
            if (rc != Result.Success) return rc;

            // create rectangleData
            var data = new RectangleData(plane, width, height);
            // and sphere object
            var rectangleObject = data.CreateCustomObject();

            // add sphereObject to doc
            doc.Objects.AddRhinoObject(rectangleObject, rectangleObject.CurveGeometry);

            //redraw views
            doc.Views.Redraw();
            // return success
            return Result.Success;
        }
    }
}