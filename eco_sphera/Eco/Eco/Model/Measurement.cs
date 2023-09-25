using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eco.Model
{
    class Measurement
    {
        public string Condition { get; set; }
        public double? CO2Density { get; set; }
        public double? CH4Density { get; set; }

        public Measurement() { }
    }

    class Combustion
    {
        public string Name { get; set; }
        public double? Coefficient { get; set; }
    }
}
