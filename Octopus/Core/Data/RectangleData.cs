using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Octopus.Core.Objects;
using Rhino.Geometry;

namespace Octopus.Core.Data
{
    public class RectangleData : DataBase
    {
        public double Width { get; set; } = 1;
        public double Height { get; set; } = 1;

        public Rectangle3d Rectangle { get; set; } = new Rectangle3d(Plane.WorldXY, 1, 1);

        public RectangleData() { }

        public RectangleData(Plane plane, double width, double height)
        {
            Plane = plane;
            Width = width;
            Height = height;

            UpdateRectangle();
        }

        public RectangleObject CreateCustomObject()
        {
            return new RectangleObject(this, new Point(Plane.Origin));
        }

        private void UpdateRectangle()
        {
            Rectangle = new Rectangle3d(Plane, Width, Height);
        }

        public override void Update()
        {
            UpdateRectangle();
        }
    }
}
