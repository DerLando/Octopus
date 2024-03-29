﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Octopus.Core.Data;
using Rhino.DocObjects.Custom;
using Rhino.Geometry;

namespace Octopus.Core.Objects
{
    public abstract class PointObjectBase<T> : CustomPointObject where T:DataBase
    {
        public T Data => Attributes.UserData.Find(typeof(T)) as T;

        protected PointObjectBase() { }

        protected PointObjectBase(DataBase data, Point point) : base(point)
        {
            this.Attributes.UserData.Add(data);
            data.Updated += OnDataUpdated;
        }

        private void OnDataUpdated(object sender, EventArgs e)
        {
            var data = sender as T;
            
        }
    }
}
