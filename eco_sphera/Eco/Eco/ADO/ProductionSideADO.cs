using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Eco.Model;

namespace Eco.ADO
{
    class ProductionSideADO
    {
        public SqlConnection connection;

        public ProductionSideADO()
        {
            connection = new SqlConnection(ConfigurationManager.ConnectionStrings["EcologyConnectionString"].ConnectionString);
        }

        public List<ProductionSide> getAll()
        {
            connection.Open();
            string platformsQuery = "SELECT * FROM ProductionSite where isDeleted = 0";
            SqlCommand cmdPlatforms = new SqlCommand(platformsQuery, connection);
            List<ProductionSide> listProdSites = new List<ProductionSide>();
            SqlDataReader platformReader = cmdPlatforms.ExecuteReader();
            while (platformReader.Read())
            {
                ProductionSide ps = new ProductionSide();
                ps.id = (int)platformReader["id"];
                ps.Name = (string)platformReader["Name"];
                ps.CompanyDO_id = (int)platformReader["CompanyDO_id"];
                listProdSites.Add(ps);
            }
            platformReader.Close();
            connection.Close();
            return listProdSites;
        }
        public ProductionSide getObject(int id)
        {
            ProductionSide obj = new ProductionSide();
            try
            {
                connection.Open();
                string query = "Select * from ProductionSite where id=@id";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@id", id);
                SqlDataReader dr = cmd.ExecuteReader();
                dr.Read();
                obj.id = (int)dr["id"];
                obj.Name = (string)dr["Name"];
                obj.Region = dr["Region"] == DBNull.Value ?  null : (string)dr["Region"];
                obj.AdministrativeArea = dr["AdministrativeArea"] == DBNull.Value ? null : (string)dr["AdministrativeArea"];
                obj.CompanyDO_id = (int)dr["CompanyDO_id"];
                dr.Close();
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при получении объекта");
                return null;
            }
            return obj;
        }
        public int Add(int companydo_id,string name,string region,string administrativeArea)
        {
            try
            {
                if (companydo_id != 0)
                {
                    connection.Open();
                    string query = "INSERT INTO ProductionSite(CompanyDO_id,Name,Region,AdministrativeArea)VALUES(@companydo_id,@name,@region,@administrativeArea); SELECT SCOPE_IDENTITY()";
                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@companydo_id", companydo_id);
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@region", region);
                    cmd.Parameters.AddWithValue("@administrativeArea", administrativeArea);
                    int x = Convert.ToInt32(cmd.ExecuteScalar());
                    connection.Close();
                    return x;
                }
                else
                    
                     MessageBox.Show("Не указана дочерняя компания");
                return 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при добавлении");
                return 0;
            }
        }
        public void Edit(int id, string name, string region , string administrativeArea)
        {
            try
            {
                connection.Open();
                string query = "UPDATE ProductionSite SET Name=@name,Region=@region ,AdministrativeArea=@administrativeArea WHERE id=@id";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@region", region);
                cmd.Parameters.AddWithValue("@administrativeArea", administrativeArea);
                cmd.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при изменении");
            }
        }
        public bool Delete(int id)
        {
            try
            {
                connection.Open();
                string query = "UPDATE ProductionSite SET IsDeleted=1 WHERE id=@id";
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
