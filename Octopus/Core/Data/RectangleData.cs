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
        public double Width { get; set; } = 1; // X
        public double Height { get; set; } = 1; // Y

        public Rectangle3d Rectangle { get; set; } = new Rectangle3d(Plane.WorldXY, 1, 1);

        public double Area => Width * Height;

        public RectangleData() { }

        public RectangleData(Plane plane, double width, double height)
        {
            Plane = plane;
            Width = width;
            Height = height;

            Update();
        }

        public RectangleObject CreateCustomObject()
        {
            return new RectangleObject(this, new Point(Plane.Origin));
        }

        internal override void UpdateAnnotations()
        {
            Annotations = new AnnotationBase[2];

            var widthDim = new LinearDimension();
            widthDim.Plane = Plane;
            widthDim.Prefix = "Width ";
            widthDim.DimensionLinePoint = new Point2d(0, -Settings.AnnotationOffset);
            widthDim.ExtensionLine1End = new Point2d(0, 0);
            widthDim.ExtensionLine2End = new Point2d(Width, 0);
            Annotations[0] = widthDim;

            var heightDim = new LinearDimension();
            heightDim.Plane = new Plane(Plane.Origin, Plane.YAxis, -Plane.XAxis);
            heightDim.Prefix = "Height ";
            heightDim.DimensionLinePoint = new Point2d(0, -Width - Settings.AnnotationOffset);
            heightDim.ExtensionLine1End = new Point2d(0, -Width);
            heightDim.ExtensionLine2End = new Point2d(Height, -Width);
            Annotations[1] = heightDim;

        }

        internal override void UpdateGeometry()
        {
            Rectangle = new Rectangle3d(Plane, Width, Height);
        }

    }
}
