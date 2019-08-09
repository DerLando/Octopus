using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Rhino.Collections;
using Rhino.DocObjects.Custom;
using Rhino.FileIO;
using Rhino.Geometry;

namespace Octopus.Core.Data
{
    [Guid("7C6ECF77-7180-4856-95D1-C242358762C8")]
    public abstract class DataBase : UserData
    {
        public Plane Plane { get; set; } = Plane.WorldXY;

        // Annotations for selected Display
        public AnnotationBase[] Annotations { get; set; }

        internal abstract void UpdateAnnotations();
        internal abstract void UpdateGeometry();

        public virtual void Update()
        {
            UpdateGeometry();
            UpdateAnnotations();
            OnUpdated(new EventArgs());
        }

        public event EventHandler Updated;

        internal void OnUpdated(EventArgs e)
        {
            EventHandler handler = Updated;
            handler?.Invoke(this, e);
        }

        #region Read Write

        public override bool ShouldWrite => true;

        public virtual ArchivableDictionary DeserializeToDictionary()
        {
            var dict = new ArchivableDictionary();

            // Set Component Plane
            dict.Set("Plane", Plane);

            return dict;
        }

        #endregion

    }
}
