using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Octopus.Core.Data;
using Octopus.UI.Views.Spheres;
using Rhino.DocObjects;

namespace Octopus.UI.Controllers
{
    // TODO: Extract SuperClass
    public static class SpheresController
    {
        public static Views.Spheres.Edit Edit()
        {
            return new Edit();
        }

        public static Views.Spheres.Edit Edit(IEnumerable<SphereData>data)
        {
            var edit = new Edit();
            edit.SetData(data);
            edit.InitializeControls();
            return edit;
        }

        public static IEnumerable<SphereData> GetSpheres(IEnumerable<RhinoObject> objects)
        {
            List<SphereData> spheres = new List<SphereData>();

            foreach (var rhinoObject in objects)
            {
                var data = rhinoObject.Attributes.UserData.Find(typeof(SphereData)) as SphereData;
                if (!(data is null)) spheres.Add(data);
            }

            return spheres;
        }

        public static bool AreSpheresRadiiEqual(IEnumerable<SphereData> data)
        {
            List<SphereData> spheres = data.ToList();

            // custom logic for empty collection
            if (spheres.Count == 0) return false;

            double radius = spheres[0].Radius;

            for (int i = 1; i < spheres.Count; i++)
            {
                // custom tolerance!!
                if (Math.Abs(spheres[i].Radius - radius) > 0.01) return false;
            }

            return true;
        }
    }
}
