using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Eco.Model;
using System.Configuration;

namespace Eco.ADO
{
    class CategoryFuelADO
    {
        public SqlConnection connection;

        public CategoryFuelADO()
        {
            connection = new SqlConnection(ConfigurationManager.ConnectionStrings["EcologyConnectionString"].ConnectionString);
        }
        public List<CategoryOfFuel> getAll()
        {
            connection.Open();
            string query = "Select * from CategoryOfFuel";
            SqlCommand cmd = new SqlCommand(query, connection);
            SqlDataReader dr = cmd.ExecuteReader();
            
            List<CategoryOfFuel> listCategory = new List<CategoryOfFuel>();
            while (dr.Read())
            {
                CategoryOfFuel obj = new CategoryOfFuel();                
                obj.id = (int)dr["id"];
                obj.CategoryName = (string)dr["CategoryName"];
                obj.FuelTableName = dr["FuelTableName"] == DBNull.Value ? null : (string)dr["FuelTableName"];
                listCategory.Add(obj);
            }
            dr.Close();
            connection.Close();
            return listCategory;
        }
        public CategoryOfFuel getTable(int id)
        {
            CategoryOfFuel obj = new CategoryOfFuel();
            try
            {
                connection.Open();
                string query = "Select * from CategoryOfFuel where id=@id";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@id", id);
                SqlDataReader dr = cmd.ExecuteReader();
                dr.Read();
                obj.id = (int)dr["id"];
                obj.CategoryName = (string)dr["CategoryName"];
                obj.FuelTableName = (string)dr["FuelTableName"];
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
        public CategoryOfFuel getObjectFromName(string categname)
        {
            CategoryOfFuel obj = new CategoryOfFuel();
            try
            {
                connection.Open();
                string query = "Select * from CategoryOfFuel where dbo.CategoryName LIKE @categname";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@categname", "%"+categname+"%");
                SqlDataReader dr = cmd.ExecuteReader();
                dr.Read();
                obj.id = (int)dr["id"];
                obj.CategoryName = (string)dr["CategoryName"];
                obj.FuelTableName = (string)dr["FuelTableName"];
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
        public CategoryOfFuel getObject(int id)
        {
            CategoryOfFuel obj = new CategoryOfFuel();
            try
            {
                connection.Open();
                string query = "Select * from CategoryOfFuel where id=@id";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@id",id);
                SqlDataReader dr = cmd.ExecuteReader();
                dr.Read();
                obj.id = (int)dr["id"];
                obj.CategoryName = (string)dr["CategoryName"];
                obj.FuelTableName = (string)dr["FuelTableName"];
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
        public List<Fuel> getFuels(string table)
        {
            List<Fuel> result = new List<Fuel>();
            try
            {
                connection.Open();
                string query = "Select * from " + table;
                SqlCommand cmd = new SqlCommand(query, connection);
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    Fuel obj = new Fuel();
                    obj.id = (int)dr["id"];
                    obj.Name = (string)dr["Name"];
                    result.Add(obj);
                }
                dr.Close();
                connection.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при получение объекта");
                return null;
            }
            return result;
        }

        public int getFuel(int id)
        {
            int res = 0;
            try
            {
                connection.Open();
                string query = @"SELECT * FROM dbo.SourceOfEmissionFuel where id=@id ";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@id", id);
                SqlDataReader sourceReader = cmd.ExecuteReader();
                while (sourceReader.Read())
                {
                    res = sourceReader["TypeOfFuelTable_id"] == DBNull.Value ? 0 : (int)sourceReader["TypeOfFuelTable_id"];
                }
                sourceReader.Close();
                connection.Close();
                return res;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка");
                return 0;
            }
        }
        public int getFuelFromTable(int id)
        {
            int res = 0;
            try
            {
                connection.Open();
                string query = @"SELECT * FROM dbo.TypeOfFuel where id=@id ";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@id", id);
                SqlDataReader sourceReader = cmd.ExecuteReader();
                while (sourceReader.Read())
                {
                    res = sourceReader["TypeOfFuelTable_id"] == DBNull.Value ? 0 : (int)sourceReader["TypeOfFuelTable_id"];
                }
                sourceReader.Close();
                connection.Close();
                return res;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка");
                return 0;
            }
        }
    }
}
