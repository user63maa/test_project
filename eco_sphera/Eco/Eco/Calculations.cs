using Eco.ADO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eco
{
    class Calculations
    {
        TypeOfFuelWithCoefADO coefficientDB = new TypeOfFuelWithCoefADO();

        public double GasFuel(int fueltype, Gases gases, double usage, int measurements = 3)
        {
            var coefficients = coefficientDB.getObject(fueltype, "dbo.TypeOfFuel");
            var measurementconditions = coefficientDB.getMeasurementConditions(measurements);
            var coeff = gases.Sum() == 0 ? (double)(coefficients.ConversionFactor1 * coefficients.EmissionFactor1) : gases.SumWithMolar() * measurementconditions.CO2Density * Math.Pow(10, -2);
            var result = (double)(coeff * usage);
            return result;
        }

        public double FluidFuel(int fueltype, double usage, double lowerHeat = 0)
        {
            var coefficients = coefficientDB.getObject(fueltype, "dbo.TypeOfFuel");
            var JlowerHeat = lowerHeat == 0 ? (double)coefficients.ConversionFactor2 : lowerHeat * 0.0041868;
            var result = (usage * JlowerHeat * 0.001) * (double)coefficients.EmissionFactor2;
            return result;
        }

        public double FlareCombustion(int fueltype, int combustionType, FlareGases gases, double usage, int measurements = 3)
        {
            var coefficients = coefficientDB.getObject(fueltype, "dbo.TypeOfFuelForFlareCombustion");
            var measurementconditions = coefficientDB.getMeasurementConditions(measurements);
            var combustion = coefficientDB.getCombustionType(combustionType);
            var co2Emmishions = usage * (gases.Sum() == 0 ? coefficients.EmissionFactorCO2 : gases.SumWithMolar() * (1 - combustion.Coefficient) * measurementconditions.CO2Density * Math.Pow(10, -2));
            var ch4Emmishions = usage * (gases.Sum() == 0 ? coefficients.EmissionFactorCH4 : gases.getMethane() * combustion.Coefficient * measurementconditions.CH4Density * Math.Pow(10, -2));
            return (double)(co2Emmishions + (ch4Emmishions * 25));
        }

        public double FugitiveEmissions(int fueltype, double usage, double ch4Share, double co2Share, int measurements = 3)
        {
            var coefficients = coefficientDB.getObject(fueltype, "dbo.TypeOfFuelForFugitivEmission");
            var measurementconditions = coefficientDB.getMeasurementConditions(measurements);
            var co2Emmishions = co2Share == 0 ? usage * coefficients.CO2Content * measurementconditions.CO2Density * Math.Pow(10, -2) : usage * co2Share * measurementconditions.CO2Density * Math.Pow(10, -2);
            var ch4Emmishions = ch4Share == 0 ? usage * coefficients.CH4Content * measurementconditions.CH4Density * Math.Pow(10, -2) : usage * ch4Share * measurementconditions.CH4Density * Math.Pow(10, -2);
            return (double)(co2Emmishions + (ch4Emmishions * 25));
        }

        public double Transport(int fueltype, double tUsage, double lUsage)
        {
            var coefficients = coefficientDB.getObject(fueltype, "dbo.TypeOfFuelForTransport");
            var result = tUsage == 0 ? (coefficients.Density * lUsage * 0.001) * coefficients.EmissionFactor1 : tUsage * coefficients.EmissionFactor1;
            return (double)result;
        }

        public double IndirectWorks(int fueltype1, int fueltype2, double usage1, double usage2)
        {
            var coefficients1 = coefficientDB.getObject(fueltype1, "dbo.EnergySystem");
            var coefficients2 = coefficientDB.getObject(fueltype2, "dbo.EnergySystem");
            var emissionFromElectricity = usage1 * coefficients1.EnergySystemCoeff1 * 0.001;
            var emissionFromHeat = usage2 * coefficients2.EnergySystemCoeff2 * 0.001;
            return (double)(emissionFromElectricity + emissionFromHeat);
        }
    }

    //Вспомогательные объекты
    class Gases
    {
        double CarbonDioxide { get; } //Диоксид углерода
        public const int MolarCarbonDioxide = 1;
        double Oxygen { get; } //Кислород
        public const int MolarOxygen = 0;
        double Ethane { get; } //Этан
        public const int MolarEthane = 2;
        double Nitrogen { get; } //Азот
        public const int MolarNitrogen = 0;
        double Methane { get; } //Метан
        public const int MolarMethane = 1;
        double HydrogenSulfide { get; } //Сероводород
        public const int MolarHydrogenSulfide = 0;
        double Propane { get; } //Пропан
        public const int MolarPropane = 3;
        double Bhutane { get; } //Бутан
        public const int MolarBhutane = 4;
        double Pentane { get; } //Пентан
        public const int MolarPentane = 5;
        double Hexane { get; } //Гексан
        public const int MolarHexane = 6;

        public Gases()
        {
            CarbonDioxide = 0;
            Oxygen = 0;
            Ethane = 0;
            Nitrogen = 0;
            Methane = 0;
            HydrogenSulfide = 0;
            Propane = 0;
            Bhutane = 0;
            Pentane = 0;
            Hexane = 0;
        }

        public Gases(double co2, double o, double c2h6,
            double n2, double ch4, double h2s, double c3h8,
            double c4h10, double c5h12, double c6h14)
        {
            CarbonDioxide = co2;
            Oxygen = o;
            Ethane = c2h6;
            Nitrogen = n2;
            Methane = ch4;
            HydrogenSulfide = h2s;
            Propane = c3h8;
            Bhutane = c4h10;
            Pentane = c5h12;
            Hexane = c6h14;
        }
        public double Sum()
        {
            return CarbonDioxide +
                Oxygen  +
                Ethane  +
                Nitrogen  +
                Methane  +
                HydrogenSulfide  +
                Propane  +
                Bhutane  +
                Pentane  +
                Hexane ;
        }

        public double SumWithMolar()
        {
            return CarbonDioxide * MolarCarbonDioxide +
                Oxygen * MolarOxygen +
                Ethane * MolarEthane +
                Nitrogen * MolarNitrogen +
                Methane * MolarMethane +
                HydrogenSulfide * MolarHydrogenSulfide +
                Propane * MolarPropane +
                Bhutane * MolarBhutane +
                Pentane * MolarPentane +
                Hexane * MolarHexane;
        }
    }

    class FlareGases
    {
        double CarbonDioxide { get; } //Диоксид углерода
        public const int MolarCarbonDioxide = 1;
        double Methane { get; } //Метан
        public const int MolarMethane = 1;
        double Ethane { get; } //Этан
        public const int MolarEthane = 2;
        double Propane { get; } //Пропан
        public const int MolarPropane = 3;
        double Bhutane { get; } //Бутан
        public const int MolarBhutane = 4;
        double Pentane { get; } //Пентан
        public const int MolarPentane = 5;
        double Hexane { get; } //Гексан
        public const int MolarHexane = 6;
        double Nitrogen { get; } //Азот
        public const int MolarNitrogen = 0;
        double Others { get; } //Другие
        public const int MolarOthers = 0;

        public FlareGases()
        {
            CarbonDioxide = 0;
            Others = 0;
            Ethane = 0;
            Nitrogen = 0;
            Methane = 0;
            Propane = 0;
            Bhutane = 0;
            Pentane = 0;
            Hexane = 0;
        }

        public FlareGases(double dioxide,
            double methane, double ethane,
            double propane, double buthane, double penthane,
            double hexane, double nitrogen, double others)
        {
            CarbonDioxide = dioxide;
            Others = others;
            Ethane = ethane;
            Nitrogen = nitrogen;
            Methane = methane;
            Propane = propane;
            Bhutane = buthane;
            Pentane = penthane;
            Hexane = hexane;
        }

        public double Sum()
        {
            return CarbonDioxide  +
                Others  +
                Ethane +
                Nitrogen  +
                Methane  +
                Propane  +
                Bhutane  +
                Pentane  +
                Hexane ;
        }

        public double SumWithMolar()
        {
            return CarbonDioxide * MolarCarbonDioxide +
                Others * MolarOthers +
                Ethane * MolarEthane +
                Nitrogen * MolarNitrogen +
                Methane * MolarMethane +
                Propane * MolarPropane +
                Bhutane * MolarBhutane +
                Pentane * MolarPentane +
                Hexane * MolarHexane;
        }

        public double getMethane()
        {
            return this.Methane;
        }
    }
}

