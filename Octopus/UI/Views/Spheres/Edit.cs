using System;
using System.Collections.Generic;
using System.Diagnostics.PerformanceData;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eto.Drawing;
using Eto.Forms;
using Octopus.Core.Data;
using Octopus.UI.Controllers;
using Rhino;

namespace Octopus.UI.Views.Spheres
{
    public class Edit : EditBase<SphereData>
    {
        // controls to add
        private Label lbl_Radius = new Label() { Text = "Radius", VerticalAlignment = VerticalAlignment.Center };
        private TextBox tB_Radius = new TextBox();

        public Edit()
        {
            // initialize eventhandlers
            btn_Update.Click += Update_Clicked;

            // initialize controls
            InitializeControls();

            // set up layout
            var layout = new DynamicLayout { DefaultSpacing = new Size(5, 5), Padding = new Padding(10) };

            layout.AddRow(lbl_Radius, tB_Radius);
            layout.AddSeparateRow(btn_Update, null);

            layout.Add(null);
            Content = layout;
        }

        public override void InitializeControls()
        {
            if (_data.Count == 0) return;
            if (_data.Count == 1 | SpheresController.AreSpheresRadiiEqual(_data))
            {
                tB_Radius.Text = _data.FirstOrDefault().Radius.ToString(CultureInfo.CurrentCulture);
                return;
            }

            tB_Radius.Text = "";
        }

        internal override void Update_Clicked(object sender, EventArgs e)
        {
            if (double.TryParse(tB_Radius.Text, NumberStyles.Any, CultureInfo.CurrentCulture, out var radius))
            {
                foreach (var sphereData in _data)
                {
                    sphereData.Radius = radius;
                    sphereData.Update();
                }
            }

            RhinoDoc.ActiveDoc.Views.Redraw();
        }

    }
}
