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
            if(value!="")
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
                var x = rowCount > 0;
                return rowCount > 0;

            }
            catch (Exception ex)
            {

                MessageBox.Show("Ошибка ");
                return false;

            }
        }
        public CalculationResult getObject(int calc_id)
        {
            CalculationResult obj = new CalculationResult();
            try
            {
                connection.Open();
                string query = "SELECT *,CONVERT(varchar,SaveDate,104) as ShortDate FROM CalculationResult where id=@id";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@id", calc_id);
                SqlDataReader dr = cmd.ExecuteReader();
                dr.Read();
                obj.id = (int)dr["id"];
                obj.PersonnelLogin = (string)dr["PersonnelLogin"];
                obj.SaveData = dr["SaveDate"].ToString();
                obj.ShortDate = dr["ShortDate"].ToString();
                obj.ResultSum = double.Parse(dr["ResultSum"].ToString());
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
        public int getIdResCalc(int source_id)
        {
            try
            {
                connection.Open();
                string query = "SELECT id FROM CalculationResult where SourceOfEmissionFuel_id=@id order by SaveDate desc";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@id", source_id);
                SqlDataReader dr = cmd.ExecuteReader();
                dr.Read();
                int resid = dr["id"] == DBNull.Value ? 0 : (int)dr["id"];
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

        public List<CalcResult> GetDataForStatistics(int id, int level, bool primary, bool indirect)
        {
            string showAll = primary && indirect ? "" : indirect ? "and ef.CategoryOfFuel_id = 11" : "and ef.CategoryOfFuel_id != 11";
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
                                  FROM [dbo].[SourceOfEmissionFuel] ef
                                  Cross apply
                                        (
                                        SELECT top(1) id,[SaveDate],[ResultSum],[PersonnelLogin],SourceOfEmissionFuel_id
                                        FROM CalculationResult c
                                        where SourceOfEmissionFuel_id = ef.id    
                                        order by SaveDate desc
	                                     ) res
                                  inner join[dbo].[SourceOfEmission] s on ef.SourceOfEmission_id = s.id
                                  inner join[dbo].[CategoryOfFuel] cat on ef.CategoryOfFuel_id = cat.id
                                  where s.id = @SourceId " + showAll;

                string query2 = @"SELECT res.[ResultSum]
                                  ,res.[SaveDate]
                                  ,res.[PersonnelLogin]
	                              ,ef.CategoryOfFuel_id
	                              ,ef.TypeOfFuelName
	                              ,cat.CategoryName
	                              ,ps.Name as SiteName, ps.AdministrativeArea, ps.Region
                                  ,s.Name as SourceName
                              FROM [dbo].[SourceOfEmissionFuel] ef
                              Cross apply
                                        (
                                        SELECT top(1) id,[SaveDate],[ResultSum],[PersonnelLogin],SourceOfEmissionFuel_id
                                        FROM CalculationResult c
                                        where SourceOfEmissionFuel_id = ef.id    
                                        order by SaveDate desc
	                                     ) res
                              inner join [dbo].[SourceOfEmission] s on ef.SourceOfEmission_id=s.id
                              inner join [dbo].[CategoryOfFuel] cat on ef.CategoryOfFuel_id=cat.id
                              inner join [dbo].[ProductionSite] ps on ps.id=s.ProductionSite_id
                              where ps.id=@SourceId " + showAll;

                string query1 = @"SELECT res.[ResultSum]
                                  ,res.[SaveDate]
                                  ,res.[PersonnelLogin]
	                              ,ef.CategoryOfFuel_id
	                              ,ef.TypeOfFuelName
	                              ,cat.CategoryName
	                              ,ps.Name as SiteName, ps.AdministrativeArea, ps.Region
	                              ,do.Name as DOName, do.ShortName
                                  ,s.Name as SourceName
                              FROM [dbo].[SourceOfEmissionFuel] ef
                              Cross apply
                                        (
                                        SELECT top(1) id,[SaveDate],[ResultSum],[PersonnelLogin],SourceOfEmissionFuel_id
                                        FROM CalculationResult c
                                        where SourceOfEmissionFuel_id = ef.id    
                                        order by SaveDate desc
	                                     ) res
                              inner join [dbo].[SourceOfEmission] s on ef.SourceOfEmission_id=s.id
                              inner join [dbo].[CategoryOfFuel] cat on ef.CategoryOfFuel_id=cat.id
                              inner join [dbo].[ProductionSite] ps on ps.id=s.ProductionSite_id
                              inner join [dbo].[CompanyDO] do on do.id=ps.CompanyDO_id
                              where do.id=@SourceId " + showAll;                                       

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

        public List<FormField> getDataForFields(int id)
        {
            connection.Open();

            string sourseQuery1 = "SELECT * FROM CalculationResult where SourceOfEmissionFuel_id = @id order by SaveDate desc";
            SqlCommand cmdSource1 = new SqlCommand(sourseQuery1, connection);
            cmdSource1.Parameters.AddWithValue("@id", id);
            SqlDataReader sourceReader1 = cmdSource1.ExecuteReader();
            sourceReader1.Read();
            int resid = (int)sourceReader1["id"];
            sourceReader1.Close();

            List<FormField> result = new List<FormField>();
            string sourseQuery = "SELECT * FROM CalculationCompositionForm where CalculationResult_id = @id";
            SqlCommand cmdSource = new SqlCommand(sourseQuery, connection);
            cmdSource.Parameters.AddWithValue("@id", resid);
            SqlDataReader sourceReader = cmdSource.ExecuteReader();
            while (sourceReader.Read())
            {
                FormField form = new FormField();
                form.Name = (string)sourceReader["ControlName"];
                form.Value = (string)sourceReader["ControlValue"];
                result.Add(form);
            }
            sourceReader.Close();
            connection.Close();
            return result;
        }

        public DOObjectForReport GetDataForReport(int id, int level)
        {
            DOObjectForReport doObj = new DOObjectForReport();
            string table = "cdo.id";
            switch (level)
            {
                case 0:
                    table = "cdo.id";
                    break;
                case 1:
                    table = "ps.id";
                    break;
                case 2:
                    table = "soe.id";
                    break;
                case 3:
                    table = "soef.id";
                    break;
                case 4:
                    table = "cr.id";
                    break;
            }
            connection.Open();
            List<CalculationCompositionForExport> listcalcres = new List<CalculationCompositionForExport>();
            string sourseQuery = @"SELECT cdo.id as CmpnyID
                                    ,cdo.Name as CmpnyName
                                    ,ps.id as PsId
                                    ,ps.Name as PsName
                                    ,ps.Region as PsRegion
                                    ,ps.AdministrativeArea as PsAdmArea
                                    ,soe.id as SoeId
                                    ,soe.Name as SoeName
                                    ,soe.Code as SoeCode
                                    ,soef.Id as SoefId
                                    ,soef.TypeOfFuelName as SoefFuelName
                                    ,cr.id as CrId
                                    ,cr.ResultSum as CrRes
                                    ,ccf.id as CcfId
                                    ,ccf.ControlName as CcfName
                                    ,ccf.ControlValue as CcfValue
                                    ,soef.CategoryOfFuel_id as CategoryId
                                    FROM CompanyDO cdo
                                    JOIN ProductionSite ps ON cdo.id = ps.CompanyDO_id
                                    JOIN SourceOfEmission soe ON soe.ProductionSite_id = ps.id
                                    JOIN SourceOfEmissionFuel soef ON soef.SourceOfEmission_id = soe.id
                                    Cross apply
                                        (
                                        SELECT top(1) id,ResultSum
                                        FROM CalculationResult c
                                        where SourceOfEmissionFuel_id = soef.id    
                                        order by SaveDate desc
	                                    ) cr
                                    JOIN CalculationCompositionForm ccf ON ccf.CalculationResult_id = cr.id
                                    where
                                    cdo.IsDeleted = 0
                                    AND ps.IsDeleted = 0
                                    AND soe.IsDeleted = 0
                                    AND soef.isDeleted = 0
                                    AND " + table + " = @id ORDER BY cdo.id, ps.id, soe.id, soef.id, cr.id, ccf.id";
            SqlCommand cmdSource = new SqlCommand(sourseQuery, connection);
            cmdSource.Parameters.AddWithValue("@id", id);
            int doId = -1;
            int prdsiteId = -1;
            int soeId = -1;
            int soefId = -1;
            int crId = -1;
            int ccfId = -1;
            SiteObjectForReport site = null;
            SourceObjectForReport source = null;
            FuelObjectForReport fuel = null;
            CoefficientsForReport coef = null;
            ResultObjectForReport res = null;

            SqlDataReader sourceReader = cmdSource.ExecuteReader();
            while (sourceReader.Read())
            {
                if (doId == -1)
                {
                    doObj.id = (int)sourceReader["CmpnyID"];
                    doObj.Name = sourceReader["CmpnyName"].ToString();
                    doId = doObj.id;
                }
                if((int)sourceReader["PsId"] !=prdsiteId)
                {
                    site = new SiteObjectForReport();
                    site.id= (int)sourceReader["PsId"];
                    site.Name= sourceReader["PsName"].ToString();
                    site.Region= sourceReader["PsRegion"].ToString();
                    site.AdministrativeArea = sourceReader["PsAdmArea"].ToString();
                    prdsiteId = site.id;
                    doObj.Sites.Add(site);
                }
                if ((int)sourceReader["SoeId"] != soeId)
                {
                    source = new SourceObjectForReport();
                    source.id = (int)sourceReader["SoeId"];
                    source.Name = sourceReader["SoeName"].ToString();
                    source.Code = sourceReader["SoeCode"].ToString();
                    soeId = source.id;
                    site.Sources.Add(source);
                    
                }
                if ((int)sourceReader["SoefId"] != soefId)
                {
                    fuel = new FuelObjectForReport();
                    fuel.id = (int)sourceReader["SoefId"];
                    fuel.FuelName = sourceReader["SoefFuelName"].ToString();
                    fuel.CategoryId = (int)sourceReader["CategoryId"];
                    soefId = fuel.id;
                    source.Fuels.Add(fuel);
                }
                if((int)sourceReader["CrId"] != crId)
                {
                    res = new ResultObjectForReport();
                    res.id = (int)sourceReader["CrId"];                   
                    res.Value = double.Parse(sourceReader["CrRes"].ToString());
                    crId = res.id;
                    fuel.Results.Add(res);
                }
                if ((int)sourceReader["CcfId"] != ccfId)
                {
                    coef = new CoefficientsForReport();
                    coef.id = (int)sourceReader["CcfId"];
                    coef.Name = sourceReader["CcfName"].ToString();
                    coef.Value = sourceReader["CcfValue"].ToString();
                    ccfId = coef.id;
                    res.Coefficients.Add(coef);
                }
            }
            sourceReader.Close();
            connection.Close();
            return doObj;
        }

    }
}
