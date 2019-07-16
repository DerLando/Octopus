using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rhino.DocObjects.Custom;
using Rhino.Geometry;

namespace Octopus.Core.Data
{
    public abstract class DataBase : UserData
    {
        public Plane Plane { get; set; } = Plane.WorldXY;

        public virtual void Update()
        {
            OnUpdated(new EventArgs());
        }

        public event EventHandler Updated;

        internal void OnUpdated(EventArgs e)
        {
            EventHandler handler = Updated;
            handler?.Invoke(this, e);
        }

    }
}
