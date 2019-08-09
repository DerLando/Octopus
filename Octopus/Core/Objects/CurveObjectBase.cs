using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Octopus.Core.Data;
using Rhino;
using Rhino.DocObjects;
using Rhino.DocObjects.Custom;
using Rhino.Geometry;

namespace Octopus.Core.Objects
{
    public abstract class CurveObjectBase<T> : CustomCurveObject where T:UserData
    {
        public T Data => Attributes.UserData.Find(typeof(T)) as T;

        protected CurveObjectBase() { }

        protected CurveObjectBase(DataBase data, Curve curve) : base(curve)
        {
            this.Attributes.UserData.Add(data);
            data.Updated += OnDataUpdated;
        }

        internal abstract void OnDataUpdated(object sender, EventArgs e);
    }
}
