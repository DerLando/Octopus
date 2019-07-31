using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Octopus.Core.Objects;
using Rhino.Geometry;

namespace Octopus.Core.Data
{
    public class BoxData : DataBase
    {
        public double Length { get; set; } = 1; // X
        public double Width { get; set; } = 1; // Y
        public double Height { get; set; } = 1; // Z

        public Box Box { get; set; }

        public double Volume => Length * Width * Height;

        public BoxData() { }

        public BoxData(Plane plane, double length, double width, double height)
        {
            Plane = plane;
            Length = length;
            Width = width;
            Height = height;

            Update();
        }

        internal override void UpdateAnnotations()
        {
            throw new NotImplementedException();
        }

        internal override void UpdateGeometry()
        {
            Box = new Box(Plane, new Interval(0, Length), new Interval(0, Width), new Interval(0, Height));
        }

        public BoxObject CreateCustomObject()
        {
            return new BoxObject(this, this.Box.ToBrep());
        }
    }
}
