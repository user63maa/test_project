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
    class CompanyDODADO
    {
        public SqlConnection connection;

        public CompanyDODADO()
        {
            connection = new SqlConnection(ConfigurationManager.ConnectionStrings["EcologyConnectionString"].ConnectionString);
        }

        public List<CompanyDO> getAll()
        {
            connection.Open();
            string subcomparyQuery = "SELECT * FROM CompanyDO where isDeleted = 0";
            SqlCommand cmdSubCompany = new SqlCommand(subcomparyQuery, connection);
            SqlDataReader subcompanyReader = cmdSubCompany.ExecuteReader();
            List<CompanyDO> listCompamyDO = new List<CompanyDO>();
            while (subcompanyReader.Read())
            {
                CompanyDO cdo = new CompanyDO();
                cdo.id = (int)subcompanyReader["id"];
                cdo.Name = (string)subcompanyReader["Name"];
                cdo.ShortName = (string)subcompanyReader["ShortName"];
                listCompamyDO.Add(cdo);
            }
            subcompanyReader.Close();
            connection.Close();
            return listCompamyDO;
        }

        public CompanyDO getObject(int id)
        {
            CompanyDO obj = new CompanyDO();
            try
            {
                connection.Open();
                string query = "Select * from CompanyDO where id=@id";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@id", id);
                SqlDataReader dr = cmd.ExecuteReader();
                dr.Read();
                obj.id = (int)dr["id"];
                obj.Name = (string)dr["Name"];
                obj.ShortName = (string)dr["ShortName"];
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
       
        public int Add(string name,string shortname)
        {
            try
            {
                connection.Open();
                string query = "INSERT INTO CompanyDO(Name,ShortName)VALUES(@name,@shortname); SELECT SCOPE_IDENTITY()";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@shortname", shortname);
                int x = Convert.ToInt32(cmd.ExecuteScalar());
                connection.Close();
                return x;
            }
            catch(Exception ex)
            {                
                MessageBox.Show("Ошибка при добавлении");
                return 0;
            }
        }
        public void Edit(int id,string name, string shortname)
        {
            try
            {
                connection.Open();
                string query = "UPDATE CompanyDO SET Name=@name,ShortName=@shortname where id=@id";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@shortname", shortname);
                cmd.Parameters.AddWithValue("@id", id);
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
                string query = "UPDATE CompanyDO SET IsDeleted=1 where id=@id";
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
