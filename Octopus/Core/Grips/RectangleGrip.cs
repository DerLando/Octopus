using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rhino.DocObjects.Custom;

namespace Octopus.Core.Grips
{
    public class RectangleGrip : CustomGripObject
    {
        public bool Active { get; set; }

        public RectangleGrip()
        {
            Active = true;
        }

        public override string ShortDescription(bool plural)
        {
            return plural ? "rectangle points" : "rectangle point";
        }
    }
}
