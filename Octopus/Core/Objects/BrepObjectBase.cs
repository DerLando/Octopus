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
    public abstract class BrepObjectBase<T> : CustomBrepObject where T:UserData
    {
        public T Data => Attributes.UserData.Find(typeof(T)) as T;

        protected BrepObjectBase() { }

        protected BrepObjectBase(DataBase data, Brep brep) : base(brep)
        {
            this.Attributes.UserData.Add(data);
            data.Updated += OnDataUpdated;
        }

        internal abstract void OnDataUpdated(object sender, EventArgs e);
    }
}
