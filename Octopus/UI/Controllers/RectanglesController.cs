using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Octopus.Core.Data;
using Octopus.UI.Views.Rectangles;
using Rhino.DocObjects;

namespace Octopus.UI.Controllers
{
    // TODO: Extract SuperClass
    public static class RectanglesController
    {
        public static UI.Views.Rectangles.Edit Edit()
        {
            return new Edit();
        }

        public static Edit Edit(IEnumerable<RectangleData> data)
        {
            var edit = new Edit();
            edit.SetData(data);
            edit.InitializeControls();
            return edit;
        }

        public static IEnumerable<RectangleData> GetRectangles(IEnumerable<RhinoObject> objects)
        {
            List<RectangleData> rectangles = new List<RectangleData>();

            foreach (var rhinoObject in objects)
            {
                var data = rhinoObject.Attributes.UserData.Find(typeof(RectangleData)) as RectangleData;
                if (!(data is null)) rectangles.Add(data);
            }

            return rectangles;
        }
    }
}
