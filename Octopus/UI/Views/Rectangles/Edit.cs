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
        private CheckBox cB_WidthActive = new CheckBox();
        private Label lbl_Height = new Label {Text = "Height", VerticalAlignment = VerticalAlignment.Center};
        private TextBox tB_Height = new TextBox();
        private CheckBox cB_HeightActive = new CheckBox();
        private Label lbl_Area = new Label {Text = "Area", VerticalAlignment = VerticalAlignment.Center};
        private TextBox tB_Area = new TextBox();
        private CheckBox cB_AreaActive = new CheckBox();

        public Edit()
        {
            // initialize eventhandlers
            btn_Update.Click += Update_Clicked;
            cB_WidthActive.CheckedChanged += Width_CheckedChanged;
            cB_HeightActive.CheckedChanged += Height_CheckedChanged;
            cB_AreaActive.CheckedChanged += Area_CheckedChanged;

            // initialize controls
            InitializeControls();

            // set up layout
            var layout = new DynamicLayout { DefaultSpacing = new Size(5, 5), Padding = new Padding(10) };

            cB_HeightActive.Checked = true;
            cB_WidthActive.Checked = true;
            cB_AreaActive.Checked = false;
            tB_Area.Enabled = false;

            layout.AddRow(lbl_Width, tB_Width, cB_WidthActive);
            layout.AddRow(lbl_Height, tB_Height, cB_HeightActive);
            layout.AddRow(lbl_Area, tB_Area, cB_AreaActive);

            layout.AddSeparateRow(btn_Update, null);

            layout.Add(null);
            Content = layout;
        }

        private void Width_CheckedChanged(object sender, EventArgs e)
        {
            bool myValue = cB_WidthActive.Checked.Value;
            tB_Width.Enabled = myValue;
        }

        private void Height_CheckedChanged(object sender, EventArgs e)
        {
            bool myValue = cB_HeightActive.Checked.Value;
            tB_Height.Enabled = myValue;
        }

        private void Area_CheckedChanged(object sender, EventArgs e)
        {
            bool myValue = cB_AreaActive.Checked.Value;
            tB_Area.Enabled = myValue;
        }

        public override void InitializeControls()
        {
            if (_data.Count == 0) return;
            if (_data.Count == 1)
            {
                tB_Width.Text = _data[0].Width.ToString(CultureInfo.CurrentCulture);
                tB_Height.Text = _data[0].Height.ToString(CultureInfo.CurrentCulture);
                tB_Area.Text = _data[0].Area.ToString(CultureInfo.CurrentCulture);
                return;
            }

            tB_Width.Text = "";
        }

        internal override void Update_Clicked(object sender, EventArgs e)
        {

            if (cB_AreaActive.Checked.Value)
            {
                if (double.TryParse(tB_Area.Text, NumberStyles.Any, CultureInfo.CurrentCulture, out var area))
                {
                    double calculatedHeight = 0;
                    double calculatedWidth = 0;

                    // Only width (x)
                    if (cB_WidthActive.Checked.Value && !cB_HeightActive.Checked.Value)
                    {
                        if (double.TryParse(tB_Width.Text, NumberStyles.Any, CultureInfo.CurrentCulture, out var width))
                        {
                            calculatedHeight = area / width;
                            foreach (var rectangleData in _data)
                            {
                                rectangleData.Height = calculatedHeight;
                                rectangleData.Width = width;
                                rectangleData.Update();
                            }

                            RhinoDoc.ActiveDoc.Views.Redraw();
                            return;
                        }
                    }

                    // only height (y)
                    if (cB_HeightActive.Checked.Value && !cB_WidthActive.Checked.Value)
                    {
                        if (double.TryParse(tB_Height.Text, NumberStyles.Any, CultureInfo.CurrentCulture, out var height))
                        {
                            calculatedWidth = area / height;
                            foreach (var rectangleData in _data)
                            {
                                rectangleData.Height = height;
                                rectangleData.Width = calculatedWidth;
                                rectangleData.Update();
                            }
                            RhinoDoc.ActiveDoc.Views.Redraw();
                            return;
                        }
                    }

                    // nothing selected, make it square
                    if (!(cB_HeightActive.Checked.Value || cB_WidthActive.Checked.Value))
                    {
                        calculatedWidth = Math.Sqrt(area);
                        foreach (var rectangleData in _data)
                        {
                            rectangleData.Height = calculatedWidth;
                            rectangleData.Width = calculatedWidth;
                            rectangleData.Update();
                        }
                        RhinoDoc.ActiveDoc.Views.Redraw();
                        return;
                    }
                }
            }

            else
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
}
