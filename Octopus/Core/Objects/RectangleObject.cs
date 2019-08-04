using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Octopus.Core.Data;
using Rhino.Display;
using Point = Rhino.Geometry.Point;

namespace Octopus.Core.Objects
{
    public class RectangleObject : ObjectBase<RectangleData>
    {
        public RectangleObject() { }

        public RectangleObject(RectangleData data, Point point) : base(data, point) { }

        protected override void OnDraw(DrawEventArgs e)
        {
            var data = Data;
            e.Display.DrawCurve(data.Rectangle.ToNurbsCurve(), Color.Red);

            foreach (var dataAnnotation in data.Annotations)
            {
                e.Display.DrawAnnotation(dataAnnotation, Color.Blue);
            }

            base.OnDraw(e);
        }
    }
}
