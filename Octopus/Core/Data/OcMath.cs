using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Octopus.Core.Data
{
    public static class OcMath
    {
        public static double VolumeFromRadius(double radius)
        {
            return (4.0 / 3.0) * Math.PI * Math.Pow(radius, 3);
        }

        public static double RadiusFromVolume(double volume)
        {
            return Math.Pow(volume * 3.0 / (4.0 * Math.PI), (1.0 / 3.0));
        }
    }
}
