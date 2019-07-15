using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eto.Drawing;
using Eto.Forms;
using Octopus.Core.Data;
using Octopus.UI.Controllers;
using Rhino;

namespace Octopus.UI.Views.Rectangles
{
    public class Edit : EditBase<RectangleData>
    {
        // controls to add
        private Label lbl_Width = new Label { Text = "Width", VerticalAlignment = VerticalAlignment.Center };
        private TextBox tB_Width = new TextBox();
        private Label lbl_Height = new Label {Text = "Height", VerticalAlignment = VerticalAlignment.Center};
        private TextBox tB_Height = new TextBox();

        public Edit()
        {
            // initialize eventhandlers
            btn_Update.Click += Update_Clicked;

            // initialize controls
            InitializeControls();

            // set up layout
            var layout = new DynamicLayout { DefaultSpacing = new Size(5, 5), Padding = new Padding(10) };

            layout.AddRow(lbl_Width, tB_Width);
            layout.AddRow(lbl_Height, tB_Height);
            layout.AddSeparateRow(btn_Update, null);

            layout.Add(null);
            Content = layout;
        }

        public override void InitializeControls()
        {
            if (_data.Count == 0) return;
            if (_data.Count == 1)
            {
                tB_Width.Text = _data[0].Width.ToString(CultureInfo.CurrentCulture);
                tB_Height.Text = _data[0].Height.ToString(CultureInfo.CurrentCulture);
                return;
            }

            tB_Width.Text = "";
        }

        internal override void Update_Clicked(object sender, EventArgs e)
        {
            if (double.TryParse(tB_Width.Text, NumberStyles.Any, CultureInfo.CurrentCulture, out var width) && double.TryParse(tB_Height.Text, NumberStyles.Any, CultureInfo.CurrentCulture, out var height))
            {
                foreach (var rectangleData in _data)
                {
                    rectangleData.Width = width;
                    rectangleData.Height = height;
                    rectangleData.Update();
                }
            }

            RhinoDoc.ActiveDoc.Views.Redraw();
        }
    }
}
