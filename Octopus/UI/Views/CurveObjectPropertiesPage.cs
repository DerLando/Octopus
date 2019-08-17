using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Octopus.Core.Data;
using Octopus.UI.Controllers;
using Octopus.UI.Views.Curves;
using Rhino;
using Rhino.UI;

namespace Octopus.Views
{
    class CurveObjectPropertiesPage : Rhino.UI.ObjectPropertiesPage
    {
        private UI.Views.Curves.Edit _control { get; set; } = new Edit();

        public override string EnglishPageTitle => "Oc";
        public override object PageControl => _control ?? (_control = UI.Controllers.CurvesController.Edit());

        public override bool ShouldDisplay(ObjectPropertiesPageEventArgs e)
        {
            if (e.ObjectCount == 0) return false;

            var curves = CurvesController.GetCurves(e.Objects).ToList();

            if (curves.Count == 0) return false;
            _control.SetData(curves);
            return true;

        }

        public override void UpdatePage(ObjectPropertiesPageEventArgs e)
        {
            var curves = CurvesController.GetCurves(e.Objects).ToList();

            if (curves.Count != 0)
            {
                _control.SetData(curves);
            }
        }
    }
}
