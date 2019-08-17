using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Octopus.Core.Data;
using Rhino;
using Rhino.Geometry;

namespace Octopus.Core.Calculations.Boolean._2D
{
    public class BooleanUnion : CalculationBase
    {
        private RectangleData[] Rectangles { get; set; }
        private CurveData[] Curves { get; set; }

        public BooleanUnion(IEnumerable<RectangleData> rectangles, IEnumerable<CurveData> curves)
        {
            Rectangles = rectangles.ToArray();
            Curves = curves.ToArray();
        }

        public BooleanUnion(IEnumerable<DataBase> data)
        {
            List<RectangleData> rects = new List<RectangleData>();
            List<CurveData> curves = new List<CurveData>();

            foreach (var dataBase in data)
            {
                var rect = data as RectangleData;
                if (!(rect is null))
                {
                    rects.Add(rect);
                    continue;
                }

                var curve = data as CurveData;
                if (!(curve is null))
                {
                    curves.Add(curve);
                    continue;
                }

            }

            Rectangles = rects.ToArray();
            Curves = curves.ToArray();
        }

        public override DataBase Calculate()
        {
            var tol = RhinoDoc.ActiveDoc.ModelAbsoluteTolerance;

            List<Curve> curves = new List<Curve>(from crv in Curves select crv.Curve);
            curves.AddRange(from rect in Rectangles select rect.Rectangle.ToPolyline().ToPolylineCurve());

            if (curves.Count < 2) return null;

            Curve union = Curve.CreateBooleanUnion(new[] { curves[0], curves[1] }, tol)[0];
            for (int i = 2; i < curves.Count; i++)
            {
                union = Curve.CreateBooleanUnion(new[] {union, curves[i]}, tol)[0];
            }

            return new CurveData(union);
        }
    }
}
