using System;
using Octopus.Core.Data;
using Rhino;
using Rhino.Commands;
using Rhino.Input;

namespace Octopus.Commands
{
    public class OcCreateBox : Command
    {
        static OcCreateBox _instance;
        public OcCreateBox()
        {
            _instance = this;
        }

        ///<summary>The only instance of the OcCreateBox command.</summary>
        public static OcCreateBox Instance
        {
            get { return _instance; }
        }

        public override string EnglishName
        {
            get { return "OcCreateBox"; }
        }

        protected override Result RunCommand(RhinoDoc doc, RunMode mode)
        {
            // get plane
            var rc = RhinoGet.GetPlane(out var plane);
            if (rc != Result.Success) return rc;

            // get length
            double length = 0;
            rc = RhinoGet.GetNumber("Specify length", false, ref length, 0.1, 1000000);
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
            var data = new BoxData(plane, length, width, height);
            // and sphere object
            var boxObject = data.CreateCustomObject();

            // add sphereObject to doc
            doc.Objects.AddRhinoObject(boxObject);

            //redraw views
            doc.Views.Redraw();
            // return success
            return Result.Success;
        }
    }
}