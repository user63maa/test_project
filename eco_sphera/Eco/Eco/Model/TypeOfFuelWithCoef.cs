using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eco.Model
{
    class TypeOfFuelWithCoef
    {
        public int id { get; set; }
        public string Name { get; set; }
        public string Dimension { get; set; }
        public double? ConversionFactor1 { get; set; }
        public double? ConversionFactor2 { get; set; }
        public double? EmissionFactor1 { get; set; }
        public double? EmissionFactor2 { get; set; }
        public double? CarbonContent1{ get; set; }
        public double? CarbonContent2 { get; set; }
        public int? GroupOfFuel_id { get; set; }
        public double? EmissionFactorCO2 { get; set; }
        public double? EmissionFactorCH4 { get; set; }
        public double? CO2Content { get; set; }
        public double? CH4Content { get; set; }
        public double? Density { get; set; }
        public int EnergySystemCoeff1 { get; set; }
        public int EnergySystemCoeff2 { get; set; }

        public TypeOfFuelWithCoef() {
            id = 0;
            Name = "";
            Dimension = "";
        }
    }
}
