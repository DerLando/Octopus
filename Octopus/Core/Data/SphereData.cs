using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Octopus.Core.Objects;
using Rhino.Geometry;

namespace Octopus.Core.Data
{
    public class SphereData : DataBase
    {
        public double Radius { get; set; } = 1;

        public Sphere Sphere { get; set; } = new Sphere(Plane.WorldXY, 1);

        public double Volume => OcMath.VolumeFromRadius(Radius);

        #region Constructors

        public SphereData() { }

        public SphereData(Plane plane, double radius)
        {
            Plane = plane;
            Radius = radius;

            // Update Sphere
            Update();
        }

        #endregion

        /// <summary>
        /// Creates a new Custom object of type SphereObject
        /// </summary>
        /// <returns>new instance of SphereObject</returns>
        public SphereObject CreateCustomObject()
        {
            return new SphereObject(this, Sphere.ToBrep());
        }


        internal override void UpdateAnnotations()
        {
            if(Annotations is null) Annotations = new AnnotationBase[1];

            var dim = new LinearDimension();
            dim.Plane = Plane;
            dim.Prefix = "Radius ";
            dim.DimensionLinePoint = new Point2d(0, 0);
            dim.ExtensionLine1End = new Point2d(0, 0);
            dim.ExtensionLine2End = new Point2d(Radius, 0);

            Annotations[0] = dim;
        }

        internal override void UpdateGeometry()
        {
            Sphere = new Sphere(Plane, Radius);
        }
    }
}
