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
        private CheckBox cB_RadiusActive = new CheckBox();
        private Label lbl_Volume = new Label() {Text = "Volume", VerticalAlignment = VerticalAlignment.Center};
        private TextBox tB_Volume = new TextBox();
        private CheckBox cB_VolumeActive = new CheckBox();

        public Edit()
        {
            // initialize eventhandlers
            btn_Update.Click += Update_Clicked;
            cB_RadiusActive.CheckedChanged += RadiusActive_CheckedChanged;
            cB_VolumeActive.CheckedChanged += VolumeActive_CheckedChanged;

            // initialize controls
            InitializeControls();

            // set up layout
            var layout = new DynamicLayout { DefaultSpacing = new Size(5, 5), Padding = new Padding(10) };

            cB_RadiusActive.Checked = true;
            cB_VolumeActive.Checked = false;

            layout.AddRow(lbl_Radius, tB_Radius, cB_RadiusActive);
            layout.AddRow(lbl_Volume, tB_Volume, cB_VolumeActive);
            layout.AddSeparateRow(btn_Update, null);

            layout.Add(null);
            Content = layout;
        }

        private void VolumeActive_CheckedChanged(object sender, EventArgs e)
        {
            bool myValue = cB_VolumeActive.Checked.Value;
            cB_RadiusActive.Checked = !myValue;
            tB_Radius.Enabled = !myValue;
        }

        private void RadiusActive_CheckedChanged(object sender, EventArgs e)
        {
            bool myValue = cB_RadiusActive.Checked.Value;
            cB_VolumeActive.Checked = !myValue;
            tB_Volume.Enabled = !myValue;
        }

        public override void InitializeControls()
        {
            if (_data.Count == 0) return;
            if (_data.Count == 1 | SpheresController.AreSpheresRadiiEqual(_data))
            {
                tB_Radius.Text = _data[0].Radius.ToString(CultureInfo.CurrentCulture);
                tB_Volume.Text = _data[0].Volume.ToString(CultureInfo.CurrentCulture);
                return;
            }

            tB_Radius.Text = "";
        }

        internal override void Update_Clicked(object sender, EventArgs e)
        {
            RhinoDoc doc = RhinoDoc.ActiveDoc;

            if (cB_VolumeActive.Checked.Value)
            {
                if (double.TryParse(tB_Volume.Text, NumberStyles.Any, CultureInfo.CurrentCulture, out var volume))
                {
                    var calculatedRadius = OcMath.RadiusFromVolume(volume);
                    foreach (var sphereData in _data)
                    {
                        sphereData.Radius = calculatedRadius;
                        sphereData.Update();
                    }
                    doc.Views.Redraw();
                    return;
                }
            }

            if (double.TryParse(tB_Radius.Text, NumberStyles.Any, CultureInfo.CurrentCulture, out var radius))
            {
                foreach (var sphereData in _data)
                {
                    sphereData.Radius = radius;
                    sphereData.Update();
                }
            }

            doc.Views.Redraw();
        }

    }
}
