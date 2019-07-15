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

        #region Constructors

        public SphereData() { }

        public SphereData(Plane plane, double radius)
        {
            Plane = plane;
            Radius = radius;

            // Update Sphere
            UpdateSphere();
        }

        #endregion

        /// <summary>
        /// Creates a new Custom object of type SphereObject
        /// </summary>
        /// <returns>new instance of SphereObject</returns>
        public SphereObject CreateCustomObject()
        {
            return new SphereObject(this, new Point(Plane.Origin));
        }

        private void UpdateSphere()
        {
            Sphere = new Sphere(Plane, Radius);
        }

        public override void Update()
        {
            UpdateSphere();
        }
    }
}
