using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eto.Drawing;
using Eto.Forms;
using Octopus.Core.Data;

namespace Octopus.UI.Views.Curves
{
    public class Edit : EditBase<CurveData>
    {

        public Edit()
        {
            // initialize eventhandlers
            btn_Update.Click += Update_Clicked;


            // set up layout
            var layout = new DynamicLayout { DefaultSpacing = new Size(5, 5), Padding = new Padding(10) };

            layout.Add(btn_Update);
            layout.Add(null);

            Content = layout;
        }

        public override void InitializeControls()
        {
            // Nothing to Initialize
        }

        internal override void Update_Clicked(object sender, EventArgs e)
        {
            foreach (var curveData in _data)
            {
                curveData.Update();
            }
        }
    }
}
