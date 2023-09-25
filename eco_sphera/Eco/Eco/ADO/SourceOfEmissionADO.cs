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
    class SourceOfEmissionADO
    {
        public SqlConnection connection;

        public SourceOfEmissionADO()
        {
            connection = new SqlConnection(ConfigurationManager.ConnectionStrings["EcologyConnectionString"].ConnectionString);
        }

        public List<SourceOfEmission> getAll()
        {
            connection.Open();

            string sourseQuery = "SELECT * FROM SourceOfEmission where isDeleted = 0";
            SqlCommand cmdSource = new SqlCommand(sourseQuery, connection);
            List<SourceOfEmission> listSource = new List<SourceOfEmission>();
            SqlDataReader sourceReader = cmdSource.ExecuteReader();
            while (sourceReader.Read())
            {
                SourceOfEmission soe = new SourceOfEmission();
                soe.id = (int)sourceReader["id"];
                soe.Code = sourceReader["Code"] == DBNull.Value ? null : (string)sourceReader["Name"];
                soe.Name = sourceReader["Name"] == DBNull.Value ? null : (string)sourceReader["Name"];
                soe.ProductionSite_id = (int)sourceReader["ProductionSite_id"];
                listSource.Add(soe);
            }
            sourceReader.Close();
            connection.Close();
            return listSource;
        }
        public SourceOfEmission getObject(int id)
        {
            try
            {
                connection.Open();

                string sourseQuery = "SELECT * FROM SourceOfEmission where id = @id";
                SqlCommand cmdSource = new SqlCommand(sourseQuery, connection);
                cmdSource.Parameters.AddWithValue("@id", id);
                SqlDataReader sourceReader = cmdSource.ExecuteReader();
                sourceReader.Read();
                SourceOfEmission soe = new SourceOfEmission();
                soe.id = (int)sourceReader["id"];
                soe.Code = sourceReader["Code"] == DBNull.Value ? null : (string)sourceReader["Name"];
                soe.Name = sourceReader["Name"] == DBNull.Value ? null : (string)sourceReader["Name"];
                soe.ProductionSite_id = (int)sourceReader["ProductionSite_id"];
                sourceReader.Close();
                connection.Close();
                return soe;
            }
            catch(Exception ex)
            {
                MessageBox.Show("Ошибка при получение объекта");
                return null;
            }
        }


        public int getType(int id)
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
                    res = sourceReader["CategoryOfFuel_id"] == DBNull.Value ? 0 : (int)sourceReader["CategoryOfFuel_id"];
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

        public int HaveCalculationResult(int id)
        {
            int res = 0;
            try
            {
                connection.Open();
                string query = @"SELECT cr.id as id
                    FROM SourceOfEmissionFuel s join CalculationResult cr ON s.id=cr.SourceOfEmissionFuel_id
                     where s.id=@id";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@id", id);
                SqlDataReader sourceReader = cmd.ExecuteReader();
                sourceReader.Read();
                if (sourceReader.Read())
                    res = sourceReader["id"] == DBNull.Value ? 0 : (int)sourceReader["id"];
                else
                    return 0;
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
        public void Add(int production_id,string name,string code)
        {
            try
            {
                connection.Open();
                string query = "INSERT INTO SourceOfEmission(ProductionSite_id,Name,Code) VALUES(@p_id,@name,@code)";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@p_id", production_id);
                cmd.Parameters.AddWithValue("@code", code);
                cmd.ExecuteNonQuery();
                connection.Close();
                MessageBox.Show("Источник добавлен");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при добавлении");
            }
        }
    }
}
