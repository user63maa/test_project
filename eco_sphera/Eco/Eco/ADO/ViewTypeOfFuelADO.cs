using Eco.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eco.ADO
{
    class ViewTypeOfFuelADO
    {
        public SqlConnection connection;

        public ViewTypeOfFuelADO()
        {
            connection = new SqlConnection(ConfigurationManager.ConnectionStrings["EcologyConnectionString"].ConnectionString);
        }
        public List<ViewTypeOfFuel> getForSourceFromTypeOfFuel(int id)
        {
            connection.Open();
            string query = @"SELECT  tof.id as id, tof.Name as Name
                    FROM [Ecosystem].[dbo].[TypeOfFuel] tof JOIN TypesForCategoryFuel tfcf ON tfcf.TypeOfFuel_id=tof.id
                     JOIN CategoryOfFuel cof ON cof.id=tfcf.CategoryOfFuel_id
                    where cof.id=@id";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@id", id);
            SqlDataReader dr = cmd.ExecuteReader();

            List<ViewTypeOfFuel> listvtof = new List<ViewTypeOfFuel>();
            while (dr.Read())
            {
                ViewTypeOfFuel obj = new ViewTypeOfFuel();
                obj.id = (int)dr["id"];
                obj.Name = (string)dr["Name"];
                listvtof.Add(obj);
            }
            dr.Close();
            connection.Close();
            return listvtof;
        }
        public List<ViewTypeOfFuel> getForSourceFromTypeOfFuelForFlareCombustion()
        {
            connection.Open();
            string query = @"SELECT  id, Name
                    FROM TypeOfFuelForFlareCombustion";
            SqlCommand cmd = new SqlCommand(query, connection);
            SqlDataReader dr = cmd.ExecuteReader();

            List<ViewTypeOfFuel> listvtof = new List<ViewTypeOfFuel>();
            while (dr.Read())
            {
                ViewTypeOfFuel obj = new ViewTypeOfFuel();
                obj.id = (int)dr["id"];
                obj.Name = (string)dr["Name"];
                listvtof.Add(obj);
            }
            dr.Close();
            connection.Close();
            return listvtof;
        }
        public List<ViewTypeOfFuel> getForSourceFromTypeOfFuelForFugitivEmission()
        {
            connection.Open();
            string query = @"SELECT  id, Name
                    FROM TypeOfFuelForFugitivEmission";
            SqlCommand cmd = new SqlCommand(query, connection);
            SqlDataReader dr = cmd.ExecuteReader();

            List<ViewTypeOfFuel> listvtof = new List<ViewTypeOfFuel>();
            while (dr.Read())
            {
                ViewTypeOfFuel obj = new ViewTypeOfFuel();
                obj.id = (int)dr["id"];
                obj.Name = (string)dr["Name"];
                listvtof.Add(obj);
            }
            dr.Close();
            connection.Close();

            return listvtof;
        }
        public List<ViewTypeOfFuel> getForSourceFromTypeOfFuelForTransport()
        {
            connection.Open();
            string query = @"SELECT  id, Name
                    FROM TypeOfFuelForTransport";
            SqlCommand cmd = new SqlCommand(query, connection);
            SqlDataReader dr = cmd.ExecuteReader();

            List<ViewTypeOfFuel> listvtof = new List<ViewTypeOfFuel>();
            while (dr.Read())
            {
                ViewTypeOfFuel obj = new ViewTypeOfFuel();
                obj.id = (int)dr["id"];
                obj.Name = (string)dr["Name"];
                listvtof.Add(obj);
            }
            dr.Close();
            connection.Close();
            return listvtof;
        }
        public List<ViewTypeOfFuel> getForSourceFromEnergySystem()
        {
            connection.Open();
            string query = @"SELECT  id, Name
                    FROM EnergySystem";
            SqlCommand cmd = new SqlCommand(query, connection);
            SqlDataReader dr = cmd.ExecuteReader();

            List<ViewTypeOfFuel> listvtof = new List<ViewTypeOfFuel>();
            while (dr.Read())
            {
                ViewTypeOfFuel obj = new ViewTypeOfFuel();
                obj.id = (int)dr["id"];
                obj.Name = (string)dr["Name"];
                listvtof.Add(obj);
            }
            dr.Close();
            connection.Close();
            return listvtof;


        }
    }
}
