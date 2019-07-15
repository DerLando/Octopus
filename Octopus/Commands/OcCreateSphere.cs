using System;
using Octopus.Core.Data;
using Rhino;
using Rhino.Commands;
using Rhino.Input;

namespace Octopus.Commands
{
    public class OcCreateSphere : Command
    {
        static OcCreateSphere _instance;
        public OcCreateSphere()
        {
            _instance = this;
        }

        ///<summary>The only instance of the OcCreateSphere command.</summary>
        public static OcCreateSphere Instance
        {
            get { return _instance; }
        }

        public override string EnglishName
        {
            get { return "OcCreateSphere"; }
        }

        protected override Result RunCommand(RhinoDoc doc, RunMode mode)
        {
            // get plane
            var rc = RhinoGet.GetPlane(out var plane);
            if (rc != Result.Success) return rc;

            // get radius
            double radius = 0;
            rc = RhinoGet.GetNumber("Specify Radius", false, ref radius, 0.1, 1000000);
            if (rc != Result.Success) return rc;

            // create spheredata
            var data = new SphereData(plane, radius);
            // and sphere object
            var sphereObject = data.CreateCustomObject();

            // add sphereObject to doc
            doc.Objects.AddRhinoObject(sphereObject);

            //redraw views
            doc.Views.Redraw();
            // return success
            return Result.Success;
        }
    }
}