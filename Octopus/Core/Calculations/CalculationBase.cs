using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Octopus.Core.Data;

namespace Octopus.Core.Calculations
{
    public abstract class CalculationBase
    {
        public abstract DataBase Calculate();
    }
}
