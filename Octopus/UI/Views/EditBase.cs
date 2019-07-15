using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eto.Forms;
using Octopus.Core.Data;

namespace Octopus.UI.Views
{
    public abstract class EditBase<T> : Panel where T : DataBase
    {
        internal List<T> _data = new List<T>();

        internal Button btn_Update = new Button() { Text = "Update" };

        public virtual void SetData(IEnumerable<T> data)
        {
            _data = data.ToList();
            InitializeControls();
            Invalidate();
        }

        public abstract void InitializeControls();

        internal abstract void Update_Clicked(object sender, EventArgs e);
    }
}
