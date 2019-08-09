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
            Annotations = new AnnotationBase[3];

            var lengthDim = new LinearDimension();
            lengthDim.Plane = Plane;
            lengthDim.Prefix = "Length ";
            lengthDim.DimensionLinePoint = new Point2d(0, -Settings.AnnotationOffset);
            lengthDim.ExtensionLine1End = new Point2d(0, 0);
            lengthDim.ExtensionLine2End = new Point2d(Length, 0);
            Annotations[0] = lengthDim;

            var widthDim = new LinearDimension();
            widthDim.Plane = new Plane(Plane.Origin, Plane.YAxis, -Plane.XAxis);
            widthDim.Prefix = "Width ";
            widthDim.DimensionLinePoint = new Point2d(0, Settings.AnnotationOffset);
            widthDim.ExtensionLine1End = new Point2d(0, 0);
            widthDim.ExtensionLine2End = new Point2d(Width, 0);
            Annotations[1] = widthDim;

            var heightDim = new LinearDimension();
            heightDim.Plane = new Plane(Plane.Origin, Vector3d.CrossProduct(Plane.XAxis, Plane.YAxis), Plane.XAxis);
            heightDim.Prefix = "Height ";
            heightDim.DimensionLinePoint = new Point2d(0, Settings.AnnotationOffset);
            heightDim.ExtensionLine1End = new Point2d(0, 0);
            heightDim.ExtensionLine2End = new Point2d(Height, 0);
            Annotations[2] = heightDim;
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
