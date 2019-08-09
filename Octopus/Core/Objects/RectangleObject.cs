using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Octopus.Core.Data;
using Rhino;
using Rhino.Display;
using Rhino.DocObjects;
using Rhino.Geometry;

namespace Octopus.Core.Objects
{
    public class RectangleObject : CurveObjectBase<RectangleData>
    {
        public RectangleObject() { }

        public RectangleObject(RectangleData data, Curve curve) : base(data, curve) { }

        protected override void OnDuplicate(RhinoObject source)
        {
            using (var other = source as RectangleObject)
            {
                this.Attributes.UserData.Add(other.Data);
            }
        }

        protected override void OnDraw(DrawEventArgs e)
        {
            var data = Data;

            foreach (var dataAnnotation in data.Annotations)
            {
                e.Display.DrawAnnotation(dataAnnotation, Color.Blue);
            }

            base.OnDraw(e);
        }

        internal override void OnDataUpdated(object sender, EventArgs e)
        {
            var data = sender as RectangleData;
            var doc = RhinoDoc.ActiveDoc;

            doc.Objects.Replace(new ObjRef(this), data.CreateCustomObject());
        }
    }
}
