using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Octopus.Core.Data;
using Rhino.DocObjects.Custom;
using Rhino.Geometry;

namespace Octopus.Core.Objects
{
    public abstract class ObjectBase : CustomPointObject
    {
        protected ObjectBase() { }

        protected ObjectBase(DataBase data, Point point) : base(point)
        {
            this.Attributes.UserData.Add(data);
        }
    }
}
