using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Octopus.Core.Data;
using Octopus.UI.Controllers;
using Octopus.UI.Views.Spheres;
using Rhino;
using Rhino.UI;

namespace Octopus.Views
{
    class SphereObjectPropertiesPage : Rhino.UI.ObjectPropertiesPage
    {
        private UI.Views.Spheres.Edit _control { get; set; } = new Edit();

        public override string EnglishPageTitle => "Oc";
        public override object PageControl => _control ?? (_control = UI.Controllers.SpheresController.Edit());

        public override bool ShouldDisplay(ObjectPropertiesPageEventArgs e)
        {
            if (e.ObjectCount == 0) return false;

            var spheres = SpheresController.GetSpheres(e.Objects).ToList();

            if (spheres.Count == 0) return false;
            _control.SetData(spheres);
            return true;

        }

        public override void UpdatePage(ObjectPropertiesPageEventArgs e)
        {
            var spheres = SpheresController.GetSpheres(e.Objects).ToList();

            if (spheres.Count != 0)
            {
                _control.SetData(spheres);
            }
        }
    }
}
