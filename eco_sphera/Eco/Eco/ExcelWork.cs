using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OfficeOpenXml;
using Eco.Model;
using Eco.ADO;
using System.IO;
using System.Windows.Forms;

namespace Eco
{
    class ExcelWork
    {
        public void getFileFromResult(int id)
        {
            CalculationResultADO clcADO = new CalculationResultADO();
            if(clcADO.HaveResult(id))
            {
                int idCalcResult = clcADO.getIdResCalc(id);
                List<CalculationCompositionForExport> list = clcADO.getListResultForReport(idCalcResult);

                string filepath = Application.StartupPath.ToString() + "\\Shablon.xlsx";
                FileInfo fileInfo = new FileInfo(filepath);
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;                
                using (ExcelPackage package = new ExcelPackage(fileInfo))
                {
                    using (var newPack = new OfficeOpenXml.ExcelPackage())
                    {
                        var wsh = package.Workbook.Worksheets["1Стац. сж. газ. топл."];
                        var newWsh = newPack.Workbook.Worksheets.Add("Отчёт", wsh);
                        newWsh.Cells[10, 20].Value = "1";
                        newWsh.Cells[10, 21].Value = "1,8393";
                        foreach (CalculationCompositionForExport item in list)
                        {
                            var cell = newWsh.Cells[item.RowNumber, item.ColNumber];                            
                            cell.Value = item.CellValue;

                        }                        
                        newPack.SaveAs(fileInfo);
                    }
                }

            }

        }
    }
}
