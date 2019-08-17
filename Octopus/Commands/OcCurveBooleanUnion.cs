using System;
using System.Collections.Generic;
using System.Linq;
using Octopus.Core.Calculations.Boolean._2D;
using Octopus.Core.Data;
using Octopus.Core.Objects;
using Rhino;
using Rhino.Commands;
using Rhino.Input.Custom;

namespace Octopus.Commands
{
    public class OcCurveBooleanUnion : Command
    {
        static OcCurveBooleanUnion _instance;
        public OcCurveBooleanUnion()
        {
            _instance = this;
        }

        ///<summary>The only instance of the OcCurveBooleanUnion command.</summary>
        public static OcCurveBooleanUnion Instance
        {
            get { return _instance; }
        }

        public override string EnglishName
        {
            get { return "OcCurveBooleanUnion"; }
        }

        protected override Result RunCommand(RhinoDoc doc, RunMode mode)
        {
            // Get curves for boolean union
            BooleanUnion union = null;
            using (var go = new GetObject())
            {
                go.SetCommandPrompt("Select OcCurves or OcRectangles");
                go.SetCustomGeometryFilter((rho, geo, ci) => (rho is CurveObject | rho is RectangleObject));

                go.GetMultiple(2, 0);

                if (go.CommandResult() != Result.Success) return go.CommandResult();

                List<RectangleData> rects = new List<RectangleData>();
                List<CurveData> crvs = new List<CurveData>();

                foreach (var objRef in go.Objects())
                {
                    var rect = objRef.Object().Attributes.UserData.Find(typeof(RectangleData)) as RectangleData;
                    if (!(rect is null))
                    {
                        rects.Add(rect);
                        continue;
                    }

                    var crv = objRef.Object().Attributes.UserData.Find(typeof(CurveData)) as CurveData;
                    if (!(crv is null))
                    {
                        crvs.Add(crv);
                        continue;
                    }
                }

                union = new BooleanUnion(rects, crvs);
            }

            var unionObj = new CurveData(((CurveData) union.Calculate()).Curve) {Calculation = union};

            doc.Objects.AddRhinoObject(unionObj.CreateCustomObject(), unionObj.Curve);

            doc.Views.Redraw();

            return Result.Success;
        }
    }
}