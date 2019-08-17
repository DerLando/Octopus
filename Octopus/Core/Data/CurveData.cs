using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Octopus.Core.Objects;
using Rhino.Geometry;

namespace Octopus.Core.Data
{
    public class CurveData : DataBase
    {
        public Curve Curve { get; set; }

        public CurveData() { }

        public CurveData(Curve curve)
        {
            Curve = curve;
        }

        public CurveObject CreateCustomObject()
        {
            return new CurveObject(this, Curve);
        }

        internal override void UpdateAnnotations()
        {
            // Do Nothing
        }

        internal override void UpdateGeometry()
        {
            if (Calculation is null)
            {
                // Do nothing, since nothing changed
            }
            else
            {
                // re-calculate curve and re-assign
                Curve = ((CurveData) Calculation.Calculate()).Curve;
            }
        }
    }
}
