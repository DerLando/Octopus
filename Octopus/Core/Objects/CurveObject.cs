using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Octopus.Core.Data;
using Rhino;
using Rhino.DocObjects;
using Rhino.Geometry;

namespace Octopus.Core.Objects
{
    public class CurveObject : CurveObjectBase<CurveData>
    {

        public CurveObject() { }

        public CurveObject(CurveData data, Curve curve) : base(data, curve) { }

        internal override void OnDataUpdated(object sender, EventArgs e)
        {
            var data = sender as CurveData;
            var doc = RhinoDoc.ActiveDoc;

            doc.Objects.Replace(new ObjRef(this), data.Curve);
        }

    }
}
