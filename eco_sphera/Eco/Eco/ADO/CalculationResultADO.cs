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
    class CalculationResultADO
    {
        public SqlConnection connection;

        public CalculationResultADO()
        {
            connection = new SqlConnection(ConfigurationManager.ConnectionStrings["EcologyConnectionString"].ConnectionString);
        }

        public int Add(int source_id, double ressum, string person)
        {
            try
            {
                connection.Open();
                string query = "INSERT INTO CalculationResult(SourceOfEmissionFuel_id,ResultSum,PersonnelLogin) VALUES(@source_id,@ressum,@person); SELECT SCOPE_IDENTITY()";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@source_id", source_id);
                cmd.Parameters.AddWithValue("@ressum", ressum);
                cmd.Parameters.AddWithValue("@person", person);
                int x = Convert.ToInt32(cmd.ExecuteScalar());                
                connection.Close();
                MessageBox.Show("Рассчёт добавлен");
                return x;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при добавлении");
                return 0;

            }
        }
        public List<CalculationCompositionForm> addToListForResForm(List<CalculationCompositionForm> list, string name, string value)
        {
            list.Add(new CalculationCompositionForm(name, value));
            return list;
        }
        public List<CalculationCompositionForExport> addToListForResForExport(List<CalculationCompositionForExport> list, string value, int col, int row)
        {
            list.Add(new CalculationCompositionForExport(value,col,row));
            return list;
        }

        public void CalculationCompositionForm(List<CalculationCompositionForm> listControls,int calcRes_id)
        {
            try
            {
                connection.Open();
                foreach(CalculationCompositionForm item in listControls)
                {
                    string query = "INSERT INTO CalculationCompositionForm(ControlName,ControlValue,CalculationResult_id) VALUES(@name,@value,@calcRes_id)";
                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@name", item.ControlName);
                    cmd.Parameters.AddWithValue("@value", item.ControlValue);
                    cmd.Parameters.AddWithValue("@calcRes_id", calcRes_id);
                    cmd.ExecuteNonQuery();
                }               
                connection.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при добавлении доп.результатов");

            }
        }
        //public List<CalculationCompositionForExport> getListForReport(int id)
        //{

        //}
        public bool HaveResult(int source_id)
        {
            try
            {
                connection.Open();

                string query = "SELECT COUNT(*) FROM CalculationResult where SourceOfEmissionFuel_id=@id";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@id", source_id);
                int rowCount = (int)cmd.ExecuteScalar();               
                connection.Close();
                return rowCount > 0;

            }
            catch (Exception ex)
            {

                MessageBox.Show("Ошибка ");
                return false;

            }
        }
        public int getIdResCalc(int source_id)
        {
            try
            {
                connection.Open();
                string query = "SELECT id FROM CalculationResult where SourceOfEmissionFuel_id=@id";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@id", source_id);
                SqlDataReader dr = cmd.ExecuteReader();
                dr.Read();
                int resid = (int)dr["id"];
                connection.Close();
                return resid;

            }
            catch (Exception ex)
            {

                MessageBox.Show("Ошибка ");
                return 0;

            }
        }
        public List<CalculationCompositionForExport> getListResultForReport(int id)
        {
            connection.Open();
            List<CalculationCompositionForExport> listcalcres = new List<CalculationCompositionForExport>();
            string sourseQuery = "SELECT * FROM CalculationCompositionForReport where CalculationResult_id = @id";
            SqlCommand cmdSource = new SqlCommand(sourseQuery, connection);
            cmdSource.Parameters.AddWithValue("@id", id);
            SqlDataReader sourceReader = cmdSource.ExecuteReader();
            while (sourceReader.Read())
            {
                CalculationCompositionForExport soe = new CalculationCompositionForExport();
                soe.id = (int)sourceReader["id"];
                soe.CellValue = sourceReader["CellValue"] == DBNull.Value ? null : (string)sourceReader["CellValue"];
                soe.ColNumber = sourceReader["ColNumber"] == DBNull.Value ? 0 : (int)sourceReader["ColNumber"];
                soe.RowNumber = sourceReader["RowNumber"] == DBNull.Value ? 0 : (int)sourceReader["RowNumber"];
                listcalcres.Add(soe);
            }
            sourceReader.Close();
            connection.Close();
            return listcalcres;
        }
        public List<CalculationCompositionForm> getListResContol(int id)
        {
            connection.Open();

            string sourseQuery = "SELECT * FROM CalculationCompositionForm where CalculationResult_id = @id";
            SqlCommand cmdSource = new SqlCommand(sourseQuery, connection);
            List<CalculationCompositionForm> listcalcres = new List<CalculationCompositionForm>();
            SqlDataReader sourceReader = cmdSource.ExecuteReader();
            while (sourceReader.Read())
            {
                CalculationCompositionForm soe = new CalculationCompositionForm();
                soe.id = (int)sourceReader["id"];
                soe.ControlName = sourceReader["ControlName"] == DBNull.Value ? null : (string)sourceReader["ControlName"];
                soe.ControlValue = sourceReader["ControlValue"] == DBNull.Value ? null : (string)sourceReader["ControlValue"];
                listcalcres.Add(soe);
            }
            sourceReader.Close();
            connection.Close();
            return listcalcres;
        }
        public void CalculationCompositionToReport(List<CalculationCompositionForExport> listRes, int calcRes_id)
        {
            try
            {
                connection.Open();
                foreach (CalculationCompositionForExport item in listRes)
                {
                    string query = "INSERT INTO CalculationCompositionForReport(CellValue,CalculationResult_id,ColNumber,RowNumber) VALUES(@value,@calcRes_id,@col,@row)";
                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@value", item.CellValue);
                    cmd.Parameters.AddWithValue("@col", item.ColNumber);
                    cmd.Parameters.AddWithValue("@row", item.RowNumber);
                    cmd.Parameters.AddWithValue("@calcRes_id", calcRes_id);
                    cmd.ExecuteNonQuery();
                }
                connection.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при добавлении доп.результатов");

            }
        }

        public List<CalcResult> GetDataForStatistics(int id, int level)
        {
            var res = new List<CalcResult>();
            try
            {
                connection.Open();

                string query3 = @"SELECT res.[ResultSum]
                                      ,res.[SaveDate]
                                      ,res.[PersonnelLogin]
	                                  ,ef.CategoryOfFuel_id
	                                  ,ef.TypeOfFuelName
	                                  ,cat.CategoryName
                                      ,s.Name as SourceName
                                  FROM[Ecosystem].[dbo].[CalculationResult] res
                                 inner join[dbo].[SourceOfEmissionFuel] ef on res.SourceOfEmissionFuel_id = ef.id
                                  inner join[dbo].[SourceOfEmission] s on ef.SourceOfEmission_id = s.id
                                  inner join[dbo].[CategoryOfFuel] cat on ef.CategoryOfFuel_id = cat.id
                                  where s.id = @SourceId";

                string query2 = @"SELECT res.[ResultSum]
                                  ,res.[SaveDate]
                                  ,res.[PersonnelLogin]
	                              ,ef.CategoryOfFuel_id
	                              ,ef.TypeOfFuelName
	                              ,cat.CategoryName
	                              ,ps.Name as SiteName, ps.AdministrativeArea, ps.Region
                                  ,s.Name as SourceName
                              FROM [Ecosystem].[dbo].[CalculationResult] res
                              inner join [dbo].[SourceOfEmissionFuel] ef on res.SourceOfEmissionFuel_id=ef.id
                              inner join [dbo].[SourceOfEmission] s on ef.SourceOfEmission_id=s.id
                              inner join [dbo].[CategoryOfFuel] cat on ef.CategoryOfFuel_id=cat.id
                              inner join [dbo].[ProductionSite] ps on ps.id=s.ProductionSite_id
                              where ps.id=@SourceId";

                string query1 = @"SELECT res.[ResultSum]
                                  ,res.[SaveDate]
                                  ,res.[PersonnelLogin]
	                              ,ef.CategoryOfFuel_id
	                              ,ef.TypeOfFuelName
	                              ,cat.CategoryName
	                              ,ps.Name as SiteName, ps.AdministrativeArea, ps.Region
	                              ,do.Name as DOName, do.ShortName
                                  ,s.Name as SourceName
                              FROM [Ecosystem].[dbo].[CalculationResult] res
                              inner join [dbo].[SourceOfEmissionFuel] ef on res.SourceOfEmissionFuel_id=ef.id
                              inner join [dbo].[SourceOfEmission] s on ef.SourceOfEmission_id=s.id
                              inner join [dbo].[CategoryOfFuel] cat on ef.CategoryOfFuel_id=cat.id
                              inner join [dbo].[ProductionSite] ps on ps.id=s.ProductionSite_id
                              inner join [dbo].[CompanyDO] do on do.id=ps.CompanyDO_id
                              where do.id=@SourceId";

                string query;
                switch (level)
                {
                    case 0:
                        query = query1;
                        break;
                    case 1:
                        query = query2;
                        break;
                    case 2:
                        query = query3;
                        break;
                    default:
                        query = query1;
                        break;
                }
                
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@SourceId", id);
                SqlDataReader sourceReader = cmd.ExecuteReader();
                while (sourceReader.Read())
                {
                    var item = new CalcResult();
                    item.FuelName = (string)sourceReader["TypeOfFuelName"];
                    item.Result = (double)sourceReader["ResultSum"];
                    item.Category = (string)sourceReader["CategoryName"];
                    item.SourceName = (string)sourceReader["SourceName"];
                    if (level <= 1)
                    {
                        item.SiteName = (string)sourceReader["SiteName"];
                    }
                    if (level == 0)
                    {
                        item.DOName = (string)sourceReader["DOName"];
                    }
                    res.Add(item);
                }


                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при добавлении доп.результатов");

            }
            return res;
        }

    }
}
