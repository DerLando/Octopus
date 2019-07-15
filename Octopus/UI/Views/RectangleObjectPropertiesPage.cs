using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Octopus.Core.Data;
using Octopus.UI.Controllers;
using Octopus.UI.Views.Rectangles;
using Rhino;
using Rhino.UI;

namespace Octopus.Views
{
    class RectangleObjectPropertiesPage : Rhino.UI.ObjectPropertiesPage
    {
        private UI.Views.Rectangles.Edit _control { get; set; } = new Edit();

        public override string EnglishPageTitle => "Oc";
        public override object PageControl => _control ?? (_control = UI.Controllers.RectanglesController.Edit());

        public override bool ShouldDisplay(ObjectPropertiesPageEventArgs e)
        {
            if (e.ObjectCount == 0) return false;

            var rectangles = RectanglesController.GetRectangles(e.Objects).ToList();

            if (rectangles.Count == 0) return false;
            _control.SetData(rectangles);
            return true;

        }

        public override void UpdatePage(ObjectPropertiesPageEventArgs e)
        {
            var rectangles = RectanglesController.GetRectangles(e.Objects).ToList();

            if (rectangles.Count != 0)
            {
                _control.SetData(rectangles);
            }
        }
    }
}
