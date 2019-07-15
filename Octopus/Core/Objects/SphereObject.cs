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
    public class SphereObject : ObjectBase
    {
        public SphereData Data => Attributes.UserData.Find(typeof(SphereData)) as SphereData;

        public SphereObject() { }

        public SphereObject(SphereData data, Point point) : base(data, point) { }

        protected override void OnDraw(DrawEventArgs e)
        {
            var data = Data;
            e.Display.DrawSphere(data.Sphere, Color.Red);
            base.OnDraw(e);
        }
    }
}
