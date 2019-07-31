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
    public class BoxObject : BrepObjectBase<BoxData>
    {
        public BoxObject() { }

        public BoxObject(BoxData data, Brep brep) : base(data, brep) { }

        protected override void OnDraw(DrawEventArgs e)
        {
            var data = Data;
            e.Display.DrawBox(data.Box, Color.Red);
            base.OnDraw(e);
        }

        internal override void OnDataUpdated(object sender, EventArgs e)
        {
            var data = sender as BoxData;
            var doc = RhinoDoc.ActiveDoc;

            doc.Objects.Replace(new ObjRef(this), data.CreateCustomObject());
        }

    }
}
