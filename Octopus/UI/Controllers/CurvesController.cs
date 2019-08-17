using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Octopus.Core.Data;
using Octopus.UI.Views.Curves;
using Rhino.DocObjects;

namespace Octopus.UI.Controllers
{
    // TODO: Extract SuperClass
    public static class CurvesController
    {
        public static UI.Views.Curves.Edit Edit()
        {
            return new Edit();
        }

        public static Edit Edit(IEnumerable<CurveData> data)
        {
            var edit = new Edit();
            edit.SetData(data);
            edit.InitializeControls();
            return edit;
        }

        public static IEnumerable<CurveData> GetCurves(IEnumerable<RhinoObject> objects)
        {
            List<CurveData> curves = new List<CurveData>();

            foreach (var rhinoObject in objects)
            {
                var data = rhinoObject.Attributes.UserData.Find(typeof(CurveData)) as CurveData;
                if (!(data is null)) curves.Add(data);
            }

            return curves;
        }
    }
}
