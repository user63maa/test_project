using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Eco.Model;

namespace Eco.ADO
{
    class TypeOfFuelWithCoefADO
    {
        public SqlConnection connection;

        public TypeOfFuelWithCoefADO()
        {
            connection = new SqlConnection(ConfigurationManager.ConnectionStrings["EcologyConnectionString"].ConnectionString);
        }
        public TypeOfFuelWithCoef getObject(int id, string table)
        {
            TypeOfFuelWithCoef obj = new TypeOfFuelWithCoef();
            try
            {
                connection.Open();
                string query = "Select * from "+ table +" where id=@id";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@id", id);
                SqlDataReader dr = cmd.ExecuteReader();
                dr.Read();
                obj.id = (int)dr["id"];
                obj.Name = dr["Name"] == DBNull.Value ? null : (string)dr["Name"];
                switch (table)
                {
                    case "dbo.TypeOfFuel":
                        obj.Dimension = dr["Dimension"] == DBNull.Value ? null : (string)dr["Dimension"];
                        obj.ConversionFactor1 = dr["ConversionFactor1"] == DBNull.Value ? null : (double?)dr["ConversionFactor1"];
                        obj.ConversionFactor2 = dr["ConversionFactor2"] == DBNull.Value ? null : (double?)dr["ConversionFactor2"];
                        obj.EmissionFactor1 = dr["EmissionFactor1"] == DBNull.Value ? null : (double?)dr["EmissionFactor1"];
                        obj.EmissionFactor2 = dr["EmissionFactor2"] == DBNull.Value ? null : (double?)dr["EmissionFactor2"];
                        obj.CarbonContent1 = dr["CarbonContent1"] == DBNull.Value ? null : (double?)dr["CarbonContent1"];
                        obj.CarbonContent2 = dr["CarbonContent2"] == DBNull.Value ? null : (double?)dr["CarbonContent2"];
                        obj.GroupOfFuel_id = dr["GroupOfFuel_id"] == DBNull.Value ? null : (int?)dr["GroupOfFuel_id"];
                        break;
                    case "dbo.TypeOfFuelForTransport":
                        obj.EmissionFactor1 = dr["EmissionFactor"] == DBNull.Value ? null : (double?)dr["EmissionFactor"];
                        obj.ConversionFactor1 = dr["ConversionFactor1"] == DBNull.Value ? null : (double?)dr["ConversionFactor1"];
                        obj.ConversionFactor2 = dr["ConversionFactor2"] == DBNull.Value ? null : (double?)dr["ConversionFactor2"];
                        obj.Density = dr["Density"] == DBNull.Value ? null : (double?)dr["Density"];
                        break;
                    case "dbo.TypeOfFuelForFugitivEmission":
                        obj.CO2Content = dr["CO2Content"] == DBNull.Value ? null : (double?)dr["CO2Content"];
                        obj.CH4Content = dr["CH4Content"] == DBNull.Value ? null : (double?)dr["CH4Content"];
                        break;
                    case "dbo.TypeOfFuelForFlareCombustion":
                        obj.EmissionFactorCH4 = dr["EmissionFactorCH4"] == DBNull.Value ? null : (double?)dr["EmissionFactorCH4"];
                        obj.EmissionFactorCO2 = dr["EmissionFactorCO2"] == DBNull.Value ? null : (double?)dr["EmissionFactorCO2"];
                        break;
                    case "dbo.EnergySystem":
                        obj.EnergySystemCoeff1 = (int)dr["Coef1"];
                        obj.EnergySystemCoeff2 = (int)dr["Coef2"];
                        break;
                }
                
                dr.Close();
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при получение объекта");
                return null;
            }
            return obj;
        }

        public Measurement getMeasurementConditions(int id)
        {
            Measurement obj = new Measurement();
            try
            {
                connection.Open();
                string query = "Select * from dbo.FlareMeasurementCondition where id=@id";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@id", id);
                SqlDataReader dr = cmd.ExecuteReader();
                dr.Read();
                obj.Condition = (string)dr["Condition"];
                obj.CO2Density = dr["DencityCO2"] == DBNull.Value ? null : (double?)dr["DencityCO2"];
                obj.CH4Density = dr["DencityCH4"] == DBNull.Value ? null : (double?)dr["DencityCH4"];

                dr.Close();
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при получение объекта");
                return null;
            }
            return obj;
        }

        public Combustion getCombustionType(int id)
        {
            Combustion obj = new Combustion();
            try
            {
                connection.Open();
                string query = "Select * from dbo.FlareCondition where id=@id";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@id", id);
                SqlDataReader dr = cmd.ExecuteReader();
                dr.Read();
                obj.Name = (string)dr["ConditionName"];
                obj.Coefficient = dr["CoefficientUnderburning"] == DBNull.Value ? null : (double?)dr["CoefficientUnderburning"];

                dr.Close();
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при получение объекта");
                return null;
            }
            return obj;
        }
    }
}
