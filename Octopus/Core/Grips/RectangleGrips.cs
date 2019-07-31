using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Octopus.Core.Data;
using Rhino.DocObjects.Custom;
using Rhino.Geometry;

namespace Octopus.Core.Grips
{
    public class RectangleGrips : CustomObjectGrips
    {
        private readonly RectangleGrip rectangleGrip;
        private Plane plane;
        private Point3d activeRectangle;
        private Point3d originalRectangle;
        private bool drawRectangle;

        public RectangleGrips()
        {
            drawRectangle = false;
            rectangleGrip = new RectangleGrip();
        }

        public bool CreateGrips(RectangleData data)
        {
            if (GripCount > 0) return false;

            plane = data.Plane;

            activeRectangle = data.Rectangle.Corner(2);
            originalRectangle = new Point3d(activeRectangle);

            rectangleGrip.OriginalLocation = new Point3d(activeRectangle);
            rectangleGrip.Active = true;

            AddGrip(rectangleGrip);

            return true;
        }

        private void UpdateGrips()
        {
            if (NewLocation)
            {
                if (rectangleGrip.Active && rectangleGrip.Moved)
                {
                    var worldToPlane = Transform.ChangeBasis(Plane.WorldXY, plane);
                    var planeToWorld = Transform.ChangeBasis(plane, Plane.WorldXY);
                }
            }
        }
    }
}
