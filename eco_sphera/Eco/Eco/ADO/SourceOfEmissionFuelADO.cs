using Eco.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Eco.ADO
{
    class SourceOfEmissionFuelADO
    {
        public SqlConnection connection;

        public SourceOfEmissionFuelADO()
        {
            connection = new SqlConnection(ConfigurationManager.ConnectionStrings["EcologyConnectionString"].ConnectionString);
        }

        public int Add(int sourceOfEmission_id, int categoryOfFuel_id, int cypeOfFuelTable_id, string cypeOfFuelName)
        {
            try
            {

                connection.Open();
                string query = "INSERT INTO SourceOfEmissionFuel(SourceOfEmission_id,CategoryOfFuel_id,TypeOfFuelTable_id,TypeOfFuelName)VALUES(@sourceOfEmission_id,@categoryOfFuel_id,@cypeOfFuelTable_id,@cypeOfFuelName); SELECT SCOPE_IDENTITY()";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@sourceOfEmission_id", sourceOfEmission_id);
                cmd.Parameters.AddWithValue("@categoryOfFuel_id", categoryOfFuel_id);
                cmd.Parameters.AddWithValue("@cypeOfFuelTable_id", cypeOfFuelTable_id);
                cmd.Parameters.AddWithValue("@cypeOfFuelName", cypeOfFuelName);
                int x = Convert.ToInt32(cmd.ExecuteScalar());
                connection.Close();
                return x;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при добавлении");
                return 0;
            }
        }
        public SourceOfEmissionFuel getObject(int id)
        {
            SourceOfEmissionFuel obj = new SourceOfEmissionFuel();
            try
            {
                connection.Open();
                string query = "Select * from SourceOfEmissionFuel where id=@id";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@id", id);
                SqlDataReader dr = cmd.ExecuteReader();
                dr.Read();
                obj.id = (int)dr["id"];
                obj.CategoryOfFuel_id = (int)dr["CategoryOfFuel_id"];
                obj.SourceOfEmission_id = (int)dr["SourceOfEmission_id"];
                obj.TypeOfFuelTable_id = (int)dr["TypeOfFuelTable_id"];
                obj.TypeOfFuelName = (string)dr["TypeOfFuelName"];
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
        public bool Delete(int id)
        {
            try
            {
                connection.Open();
                string query = "UPDATE SourceOfEmissionFuel SET IsDeleted=1 WHERE id=@id";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
                connection.Close();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при удалении");
                return false;
            }
        }

    }
}
