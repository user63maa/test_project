using Eco.ADO;
using Eco.Model;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Eco
{
    //Добавить для каждого отчета
    class ExcelWork
    {
        string ReportsDirectory = Application.StartupPath.ToString() + "\\Reports";
        public void getFileFromResult(int id, string report)
        {
            CalculationResultADO clcADO = new CalculationResultADO();
            if (clcADO.HaveResult(id))
            {
                //Копирвание темплейта и переименование
                if (!Directory.Exists(ReportsDirectory))
                    Directory.CreateDirectory(ReportsDirectory);
                string filename = "\\Отчет_" + DateTime.Now.Date + ".xlsx"; //Заменить на вменяемый текст?
                File.Copy("Templates\\Template.xlsx", ReportsDirectory + filename, true);

                //Получение данных?
                int idCalcResult = clcADO.getIdResCalc(id);
                List<CalculationCompositionForExport> list = clcADO.getListResultForReport(idCalcResult);

                //Изменение эксельки
                string filepath = ReportsDirectory + filename;
                FileInfo fileInfo = new FileInfo(filepath);
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                using (ExcelPackage package = new ExcelPackage(fileInfo))
                {
                    var sheet = package.Workbook.Worksheets[report];
                    sheet.Cells[10, 20].Value = "1";
                    sheet.Cells[10, 21].Value = "1,8393";
                    foreach (CalculationCompositionForExport item in list)
                    {
                        var cell = sheet.Cells[item.RowNumber, item.ColNumber];
                        cell.Value = item.CellValue;

                    }
                    package.Save();
                }
            }
        }

        public void getFullReport(int id, int level)
        {
            CalculationResultADO cADO = new CalculationResultADO();
            var obj = cADO.GetDataForReport(id, level);
            if (!Directory.Exists(ReportsDirectory))
                Directory.CreateDirectory(ReportsDirectory);
            string filename = "\\Отчет_" + DateTime.Now.Hour.ToString() + "." + DateTime.Now.Minute.ToString() + ".xlsx"; //Заменить на вменяемый текст?
            File.Copy("Templates\\Отчет.xlsx", ReportsDirectory + filename, true);

            string filepath = ReportsDirectory + filename;
            FileInfo fileInfo = new FileInfo(filepath);
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (ExcelPackage package = new ExcelPackage(fileInfo))
            {
                int case1 = 0, case2 = 0, case4 = 0, case5 = 0, case10 = 0, case11 = 0;
                foreach (var site in obj.Sites)
                {
                    foreach (var source in site.Sources)
                    {
                        foreach (var fuel in source.Fuels)
                        {
                            ExcelWorksheet sheet;
                            switch (fuel.CategoryId)
                            {
                                case 1:
                                    //1Стац. сж. газ. топл.
                                    foreach (var result in fuel.Results)
                                    {
                                        sheet = package.Workbook.Worksheets["1Стац. сж. газ. топл."];
                                        int firstrow1 = 4;
                                        int lastrow1 = 14;
                                        int tableSize1 = 11;

                                        sheet.Cells["B" + firstrow1 + ":V" + lastrow1].Copy(sheet.Cells["B" + (firstrow1 + tableSize1 * case1) + ":V" + (lastrow1 + tableSize1 * case1)]);

                                        sheet.Cells["I" + (4 + tableSize1 * case1)].Value = site.Region;
                                        sheet.Cells["I" + (5 + tableSize1 * case1)].Value = site.AdministrativeArea;
                                        sheet.Cells["I" + (6 + tableSize1 * case1)].Value = site.Name;
                                        sheet.Cells["I" + (7 + tableSize1 * case1)].Value = source.Code;
                                        sheet.Cells["L" + (7 + tableSize1 * case1)].Value = source.Name;
                                        sheet.Cells["I" + (8 + tableSize1 * case1)].Value = fuel.FuelName;

                                        if (result.Coefficients.Any(d => d.Name == "mixPercent1")) sheet.Cells["I" + (10 + tableSize1 * case1)].Value = Double.Parse(result.Coefficients.First(d => d.Name == "mixPercent1").Value);
                                        if (result.Coefficients.Any(d => d.Name == "mixPercent2")) sheet.Cells["J" + (10 + tableSize1 * case1)].Value = Double.Parse(result.Coefficients.First(d => d.Name == "mixPercent2").Value);
                                        if (result.Coefficients.Any(d => d.Name == "mixPercent3")) sheet.Cells["K" + (10 + tableSize1 * case1)].Value = Double.Parse(result.Coefficients.First(d => d.Name == "mixPercent3").Value);
                                        if (result.Coefficients.Any(d => d.Name == "mixPercent4")) sheet.Cells["L" + (10 + tableSize1 * case1)].Value = Double.Parse(result.Coefficients.First(d => d.Name == "mixPercent4").Value);
                                        if (result.Coefficients.Any(d => d.Name == "mixPercent5")) sheet.Cells["M" + (10 + tableSize1 * case1)].Value = Double.Parse(result.Coefficients.First(d => d.Name == "mixPercent5").Value);
                                        if (result.Coefficients.Any(d => d.Name == "mixPercent6")) sheet.Cells["N" + (10 + tableSize1 * case1)].Value = Double.Parse(result.Coefficients.First(d => d.Name == "mixPercent6").Value);
                                        if (result.Coefficients.Any(d => d.Name == "mixPercent7")) sheet.Cells["O" + (10 + tableSize1 * case1)].Value = Double.Parse(result.Coefficients.First(d => d.Name == "mixPercent7").Value);
                                        if (result.Coefficients.Any(d => d.Name == "mixPercent8")) sheet.Cells["P" + (10 + tableSize1 * case1)].Value = Double.Parse(result.Coefficients.First(d => d.Name == "mixPercent8").Value);
                                        if (result.Coefficients.Any(d => d.Name == "mixPercent9")) sheet.Cells["Q" + (10 + tableSize1 * case1)].Value = Double.Parse(result.Coefficients.First(d => d.Name == "mixPercent9").Value);
                                        if (result.Coefficients.Any(d => d.Name == "mixPercent10")) sheet.Cells["R" + (10 + tableSize1 * case1)].Value = Double.Parse(result.Coefficients.First(d => d.Name == "mixPercent10").Value);

                                        if (result.Coefficients.Any(d => d.Name == "gasesUsageTB")) sheet.Cells["V" + (10 + tableSize1 * case1)].Value = Double.Parse(result.Coefficients.First(d => d.Name == "gasesUsageTB").Value);
                                        if (result.Coefficients.Any(d => d.Name == "CO2DensityGasTB")) sheet.Cells["U" + (10 + tableSize1 * case1)].Value = Double.Parse(result.Coefficients.First(d => d.Name == "CO2DensityGasTB").Value);
                                        if (result.Coefficients.Any(d => d.Name == "emissionsResult1")) sheet.Cells["U" + (12 + tableSize1 * case1)].Value = Double.Parse(result.Coefficients.First(d => d.Name == "emissionsResult1").Value);
                                        if (result.Coefficients.Any(d => d.Name == "emissionsResult1")) sheet.Cells["V" + (12 + tableSize1 * case1)].Value = Double.Parse(result.Coefficients.First(d => d.Name == "emissionsResult1").Value);
                                        if (result.Coefficients.Any(d => d.Name == "emissionsResult1") && result.Coefficients.Any(d => d.Name == "gasesUsageTB"))
                                            sheet.Cells["T" + (12 + tableSize1 * case1)].Value = Double.Parse(result.Coefficients.First(d => d.Name == "emissionsResult1").Value) / Double.Parse(result.Coefficients.First(d => d.Name == "gasesUsageTB").Value);

                                        sheet.Row(4 + tableSize1 * case1).Height = 20;
                                        sheet.Row(5 + tableSize1 * case1).Height = 20;
                                        sheet.Row(6 + tableSize1 * case1).Height = 20;
                                        sheet.Row(7 + tableSize1 * case1).Height = 20;
                                        sheet.Row(8 + tableSize1 * case1).Height = 40;
                                        sheet.Row(9 + tableSize1 * case1).Height = 75;
                                        sheet.Row(10 + tableSize1 * case1).Height = 20;
                                        sheet.Row(11 + tableSize1 * case1).Height = 95;
                                        sheet.Row(12 + tableSize1 * case1).Height = 20;

                                        case1++;
                                    }
                                    break;
                                case 2:
                                    //1Стац. сж. жид. топл.
                                    foreach (var result in fuel.Results)
                                    {
                                        sheet = package.Workbook.Worksheets["1Стац. сж. жид. топл."];
                                        int firstrow2 = 4;
                                        int lastrow2 = 13;
                                        int tableSize2 = 10;

                                        sheet.Cells["B" + firstrow2 + ":V" + lastrow2].Copy(sheet.Cells["B" + (firstrow2 + tableSize2 * case2) + ":V" + (lastrow2 + tableSize2 * case2)]);

                                        sheet.Cells["I" + (4 + tableSize2 * case2)].Value = site.Region;
                                        sheet.Cells["I" + (5 + tableSize2 * case2)].Value = site.AdministrativeArea;
                                        sheet.Cells["I" + (6 + tableSize2 * case2)].Value = site.Name;
                                        sheet.Cells["I" + (7 + tableSize2 * case2)].Value = source.Code;
                                        sheet.Cells["L" + (7 + tableSize2 * case2)].Value = source.Name;
                                        sheet.Cells["I" + (8 + tableSize2 * case2)].Value = fuel.FuelName;

                                        if (result.Coefficients.Any(d => d.Name == "fluidLowerHeatTB")) sheet.Cells["K" + (11 + tableSize2 * case2)].Value = Double.Parse(result.Coefficients.First(d => d.Name == "fluidLowerHeatTB").Value);
                                        if (result.Coefficients.Any(d => d.Name == "dictLowerHeatTB")) sheet.Cells["M" + (11 + tableSize2 * case2)].Value = Double.Parse(result.Coefficients.First(d => d.Name == "dictLowerHeatTB").Value);
                                        if (result.Coefficients.Any(d => d.Name == "fluidUsageTB")) sheet.Cells["I" + (11 + tableSize2 * case2)].Value = Double.Parse(result.Coefficients.First(d => d.Name == "fluidUsageTB").Value);
                                        if (result.Coefficients.Any(d => d.Name == "fluidUsageTJTB")) sheet.Cells["S" + (11 + tableSize2 * case2)].Value = Double.Parse(result.Coefficients.First(d => d.Name == "fluidUsageTJTB").Value);
                                        if (result.Coefficients.Any(d => d.Name == "tbCoefEmission")) sheet.Cells["O" + (11 + tableSize2 * case2)].Value = Double.Parse(result.Coefficients.First(d => d.Name == "tbCoefEmission").Value);
                                        if (result.Coefficients.Any(d => d.Name == "emissionsResult2")) sheet.Cells["U" + (11 + tableSize2 * case2)].Value = Double.Parse(result.Coefficients.First(d => d.Name == "emissionsResult2").Value);

                                        sheet.Row(4 + tableSize2 * case2).Height = 20;
                                        sheet.Row(5 + tableSize2 * case2).Height = 20;
                                        sheet.Row(6 + tableSize2 * case2).Height = 20;
                                        sheet.Row(7 + tableSize2 * case2).Height = 20;
                                        sheet.Row(8 + tableSize2 * case2).Height = 40;
                                        sheet.Row(9 + tableSize2 * case2).Height = 95;
                                        sheet.Row(10 + tableSize2 * case2).Height = 25;
                                        sheet.Row(11 + tableSize2 * case2).Height = 25;

                                        case2++;
                                    }
                                    break;
                                case 4:
                                    //2Факел. горение
                                    foreach (var result in fuel.Results)
                                    {
                                        sheet = package.Workbook.Worksheets["2Факел. горение"];
                                        int firstrow3 = 4;
                                        int lastrow3 = 16;
                                        int tableSize3 = 13;

                                        sheet.Cells["B" + firstrow3 + ":AA" + lastrow3].Copy(sheet.Cells["B" + (firstrow3 + tableSize3 * case4) + ":AA" + (lastrow3 + tableSize3 * case4)]);

                                        sheet.Cells["I" + (4 + tableSize3 * case4)].Value = site.Region;
                                        sheet.Cells["I" + (5 + tableSize3 * case4)].Value = site.AdministrativeArea;
                                        sheet.Cells["I" + (6 + tableSize3 * case4)].Value = site.Name;
                                        sheet.Cells["I" + (7 + tableSize3 * case4)].Value = source.Code;
                                        sheet.Cells["K" + (7 + tableSize3 * case4)].Value = source.Name;
                                        sheet.Cells["I" + (8 + tableSize3 * case4)].Value = fuel.FuelName;

                                        if (result.Coefficients.Any(d => d.Name == "fuelUsageFlareTB")) sheet.Cells["I" + (12 + tableSize3 * case4)].Value = Double.Parse(result.Coefficients.First(d => d.Name == "fuelUsageFlareTB").Value);

                                        if (result.Coefficients.Any(d => d.Name == "flareTB1")) sheet.Cells["J" + (12 + tableSize3 * case4)].Value = Double.Parse(result.Coefficients.First(d => d.Name == "flareTB1").Value);
                                        if (result.Coefficients.Any(d => d.Name == "flareTB2")) sheet.Cells["K" + (12 + tableSize3 * case4)].Value = Double.Parse(result.Coefficients.First(d => d.Name == "flareTB2").Value);
                                        if (result.Coefficients.Any(d => d.Name == "flareTB3")) sheet.Cells["L" + (12 + tableSize3 * case4)].Value = Double.Parse(result.Coefficients.First(d => d.Name == "flareTB3").Value);
                                        if (result.Coefficients.Any(d => d.Name == "flareTB4")) sheet.Cells["M" + (12 + tableSize3 * case4)].Value = Double.Parse(result.Coefficients.First(d => d.Name == "flareTB4").Value);
                                        if (result.Coefficients.Any(d => d.Name == "flareTB5")) sheet.Cells["N" + (12 + tableSize3 * case4)].Value = Double.Parse(result.Coefficients.First(d => d.Name == "flareTB5").Value);
                                        if (result.Coefficients.Any(d => d.Name == "flareTB6")) sheet.Cells["O" + (12 + tableSize3 * case4)].Value = Double.Parse(result.Coefficients.First(d => d.Name == "flareTB6").Value);
                                        if (result.Coefficients.Any(d => d.Name == "flareTB7")) sheet.Cells["P" + (12 + tableSize3 * case4)].Value = Double.Parse(result.Coefficients.First(d => d.Name == "flareTB7").Value);
                                        if (result.Coefficients.Any(d => d.Name == "flareTB8")) sheet.Cells["Q" + (12 + tableSize3 * case4)].Value = Double.Parse(result.Coefficients.First(d => d.Name == "flareTB8").Value);
                                        if (result.Coefficients.Any(d => d.Name == "flareTB9")) sheet.Cells["R" + (12 + tableSize3 * case4)].Value = Double.Parse(result.Coefficients.First(d => d.Name == "flareTB9").Value);

                                        if (result.Coefficients.Any(d => d.Name == "FlareCO2DensityTB")) sheet.Cells["T" + (12 + tableSize3 * case4)].Value = Double.Parse(result.Coefficients.First(d => d.Name == "FlareCO2DensityTB").Value);
                                        if (result.Coefficients.Any(d => d.Name == "tbDensityMetan")) sheet.Cells["U" + (12 + tableSize3 * case4)].Value = Double.Parse(result.Coefficients.First(d => d.Name == "tbDensityMetan").Value);
                                        if (result.Coefficients.Any(d => d.Name == "tbKoefNedoj")) sheet.Cells["V" + (12 + tableSize3 * case4)].Value = Double.Parse(result.Coefficients.First(d => d.Name == "tbKoefNedoj").Value);
                                        if (result.Coefficients.Any(d => d.Name == "tbKoefVibrosCO2")) sheet.Cells["W" + (12 + tableSize3 * case4)].Value = Double.Parse(result.Coefficients.First(d => d.Name == "tbKoefVibrosCO2").Value);
                                        if (result.Coefficients.Any(d => d.Name == "tbKoefVibrosCH4")) sheet.Cells["x" + (12 + tableSize3 * case4)].Value = Double.Parse(result.Coefficients.First(d => d.Name == "tbKoefVibrosCH4").Value);
                                        if (result.Coefficients.Any(d => d.Name == "tbVibrosCO2")) sheet.Cells["Y" + (12 + tableSize3 * case4)].Value = Double.Parse(result.Coefficients.First(d => d.Name == "tbVibrosCO2").Value);
                                        if (result.Coefficients.Any(d => d.Name == "tbVibrosCH4")) sheet.Cells["Z" + (12 + tableSize3 * case4)].Value = Double.Parse(result.Coefficients.First(d => d.Name == "tbVibrosCH4").Value);

                                        if (result.Coefficients.Any(d => d.Name == "emissionsResult3")) sheet.Cells["AA" + (12 + tableSize3 * case4)].Value = Double.Parse(result.Coefficients.First(d => d.Name == "emissionsResult3").Value);

                                        if (result.Coefficients.Any(d => d.Name == "combustionFlareCB")) sheet.Cells["T" + (14 + tableSize3 * case4)].Value = result.Coefficients.First(d => d.Name == "combustionFlareCB").Value;

                                        sheet.Row(4 + tableSize3 * case4).Height = 20;
                                        sheet.Row(5 + tableSize3 * case4).Height = 20;
                                        sheet.Row(6 + tableSize3 * case4).Height = 20;
                                        sheet.Row(7 + tableSize3 * case4).Height = 20;
                                        sheet.Row(8 + tableSize3 * case4).Height = 40;
                                        sheet.Row(9 + tableSize3 * case4).Height = 20;
                                        sheet.Row(10 + tableSize3 * case4).Height = 105;
                                        sheet.Row(11 + tableSize3 * case4).Height = 20;
                                        sheet.Row(12 + tableSize3 * case4).Height = 20;
                                        sheet.Row(13 + tableSize3 * case4).Height = 40;
                                        sheet.Row(14 + tableSize3 * case4).Height = 40;

                                        case4++;
                                    }
                                    break;
                                case 5:
                                    //3Фугитивные выбросы свечи
                                    foreach (var result in fuel.Results)
                                    {
                                        sheet = package.Workbook.Worksheets["3Фугитивные выбросы свечи"];
                                        int firstrow4 = 4;
                                        int lastrow4 = 13;
                                        int tableSize4 = 10;

                                        sheet.Cells["B" + firstrow4 + ":AC" + lastrow4].Copy(sheet.Cells["B" + (firstrow4 + tableSize4 * case5) + ":AC" + (lastrow4 + tableSize4 * case5)]);

                                        sheet.Cells["I" + (4 + tableSize4 * case5)].Value = site.Region;
                                        sheet.Cells["I" + (5 + tableSize4 * case5)].Value = site.AdministrativeArea;
                                        sheet.Cells["I" + (6 + tableSize4 * case5)].Value = site.Name;
                                        sheet.Cells["I" + (7 + tableSize4 * case5)].Value = source.Code;
                                        sheet.Cells["K" + (7 + tableSize4 * case5)].Value = source.Name;
                                        sheet.Cells["I" + (8 + tableSize4 * case5)].Value = fuel.FuelName;

                                        if (result.Coefficients.Any(d => d.Name == "gasUsageFugitiveTB")) sheet.Cells["I" + (11 + tableSize4 * case5)].Value = Double.Parse(result.Coefficients.First(d => d.Name == "gasUsageFugitiveTB").Value);
                                        if (result.Coefficients.Any(d => d.Name == "CH4ShareTB")) sheet.Cells["L" + (11 + tableSize4 * case5)].Value = Double.Parse(result.Coefficients.First(d => d.Name == "CH4ShareTB").Value);
                                        if (result.Coefficients.Any(d => d.Name == "CO2ShareTB")) sheet.Cells["N" + (11 + tableSize4 * case5)].Value = Double.Parse(result.Coefficients.First(d => d.Name == "CO2ShareTB").Value);

                                        if (result.Coefficients.Any(d => d.Name == "textBox5")) sheet.Cells["P" + (11 + tableSize4 * case5)].Value = Double.Parse(result.Coefficients.First(d => d.Name == "textBox5").Value);
                                        if (result.Coefficients.Any(d => d.Name == "FugitiveCO2DensityTB")) sheet.Cells["Q" + (11 + tableSize4 * case5)].Value = Double.Parse(result.Coefficients.First(d => d.Name == "FugitiveCO2DensityTB").Value);

                                        if (result.Coefficients.Any(d => d.Name == "FugitiveCO2DensityTB")) sheet.Cells["R" + (11 + tableSize4 * case5)].Value = Double.Parse(result.Coefficients.First(d => d.Name == "FugitiveCO2DensityTB").Value);
                                        if (result.Coefficients.Any(d => d.Name == "tbCO2Density")) sheet.Cells["T" + (11 + tableSize4 * case5)].Value = Double.Parse(result.Coefficients.First(d => d.Name == "tbCO2Density").Value);

                                        if (result.Coefficients.Any(d => d.Name == "tbFigusiveCH4")) sheet.Cells["V" + (11 + tableSize4 * case5)].Value = Double.Parse(result.Coefficients.First(d => d.Name == "tbFigusiveCH4").Value);
                                        if (result.Coefficients.Any(d => d.Name == "tbFigusiveCO2")) sheet.Cells["X" + (11 + tableSize4 * case5)].Value = Double.Parse(result.Coefficients.First(d => d.Name == "tbFigusiveCO2").Value);

                                        if (result.Coefficients.Any(d => d.Name == "emissionsResult4")) sheet.Cells["Z" + (11 + tableSize4 * case5)].Value = Double.Parse(result.Coefficients.First(d => d.Name == "emissionsResult4").Value);

                                        sheet.Row(4 + tableSize4 * case5).Height = 20;
                                        sheet.Row(5 + tableSize4 * case5).Height = 20;
                                        sheet.Row(6 + tableSize4 * case5).Height = 20;
                                        sheet.Row(7 + tableSize4 * case5).Height = 20;
                                        sheet.Row(8 + tableSize4 * case5).Height = 40;
                                        sheet.Row(9 + tableSize4 * case5).Height = 120;
                                        sheet.Row(10 + tableSize4 * case5).Height = 20;
                                        sheet.Row(11 + tableSize4 * case5).Height = 20;

                                        case5++;
                                    }
                                    break;
                                case 10:
                                    //6Транспорт
                                    foreach (var result in fuel.Results)
                                    {
                                        sheet = package.Workbook.Worksheets["6Транспорт"];
                                        int firstrow5 = 4;
                                        int lastrow5 = 9;
                                        int tableSize5 = 6;

                                        sheet.Cells["B" + firstrow5 + ":J" + lastrow5].Copy(sheet.Cells["B" + (firstrow5 + tableSize5 * case10) + ":J" + (lastrow5 + tableSize5 * case10)]);

                                        if (result.Coefficients.Any(d => d.Name == "tUsageTransportTB")) sheet.Cells["C" + (7 + tableSize5 * case10)].Value = Double.Parse(result.Coefficients.First(d => d.Name == "tUsageTransportTB").Value);
                                        if (result.Coefficients.Any(d => d.Name == "lUsageTransportTB")) sheet.Cells["D" + (7 + tableSize5 * case10)].Value = Double.Parse(result.Coefficients.First(d => d.Name == "lUsageTransportTB").Value);
                                        if (result.Coefficients.Any(d => d.Name == "tbRasxodToplivaRaschet")) sheet.Cells["E" + (7 + tableSize5 * case10)].Value = Double.Parse(result.Coefficients.First(d => d.Name == "tbRasxodToplivaRaschet").Value);
                                        if (result.Coefficients.Any(d => d.Name == "tbPlotnostTopliva")) sheet.Cells["F" + (7 + tableSize5 * case10)].Value = Double.Parse(result.Coefficients.First(d => d.Name == "tbPlotnostTopliva").Value);
                                        if (result.Coefficients.Any(d => d.Name == "tbKoefVibrosov")) sheet.Cells["G" + (7 + tableSize5 * case10)].Value = Double.Parse(result.Coefficients.First(d => d.Name == "tbKoefVibrosov").Value);
                                        if (result.Coefficients.Any(d => d.Name == "emissionsResult5")) sheet.Cells["I" + (7 + tableSize5 * case10)].Value = Double.Parse(result.Coefficients.First(d => d.Name == "emissionsResult5").Value);
                                        sheet.Cells["E" + (4 + tableSize5 * case10)].Value = fuel.FuelName;

                                        sheet.Row(4 + tableSize5 * case10).Height = 20;
                                        sheet.Row(5 + tableSize5 * case10).Height = 55;
                                        sheet.Row(6 + tableSize5 * case10).Height = 20;
                                        sheet.Row(7 + tableSize5 * case10).Height = 20;

                                        case10++;
                                    }
                                    break;
                                case 11:
                                    //7Косвенные выбросы (2 охват)
                                    foreach (var result in fuel.Results)
                                    {
                                        sheet = package.Workbook.Worksheets["7Косвенные выбросы (2 охват)"];
                                        int firstrow6 = 4;
                                        int lastrow6 = 9;
                                        int tableSize6 = 6;

                                        sheet.Cells["B" + firstrow6 + ":H" + lastrow6].Copy(sheet.Cells["B" + (firstrow6 + tableSize6 * case11) + ":H" + (lastrow6 + tableSize6 * case11)]);

                                        if (result.Coefficients.Any(d => d.Name == "electroUsageTB")) sheet.Cells["D" + (6 + tableSize6 * case11)].Value = Double.Parse(result.Coefficients.First(d => d.Name == "electroUsageTB").Value);
                                        if (result.Coefficients.Any(d => d.Name == "heatUsageTB")) sheet.Cells["D" + (7 + tableSize6 * case11)].Value = Double.Parse(result.Coefficients.First(d => d.Name == "heatUsageTB").Value);

                                        if (result.Coefficients.Any(d => d.Name == "electroUsageTB")) sheet.Cells["F" + (6 + tableSize6 * case11)].Value = Double.Parse(result.Coefficients.First(d => d.Name == "electroUsageTB").Value);
                                        if (result.Coefficients.Any(d => d.Name == "heatUsageTB")) sheet.Cells["G" + (6 + tableSize6 * case11)].Value = Double.Parse(result.Coefficients.First(d => d.Name == "heatUsageTB").Value);

                                        sheet.Cells["E" + (6 + tableSize6 * case11)].Value = fuel.FuelName;
                                        sheet.Cells["E" + (7 + tableSize6 * case11)].Value = fuel.FuelName;

                                        if (result.Coefficients.Any(d => d.Name == "emissionsResult6")) sheet.Cells["H" + (6 + tableSize6 * case11)].Value = Double.Parse(result.Coefficients.First(d => d.Name == "emissionsResult6").Value);

                                        sheet.Row(4 + tableSize6 * case11).Height = 90;
                                        sheet.Row(5 + tableSize6 * case11).Height = 20;
                                        sheet.Row(6 + tableSize6 * case11).Height = 30;
                                        sheet.Row(7 + tableSize6 * case11).Height = 30;

                                        case11++;
                                    }
                                    break;
                            }
                        }
                    }
                }


                var reportsheet = package.Workbook.Worksheets["Общий отчет"];
                var finalResults = cADO.GetDataForStatistics(id, level, true, true).OrderByDescending(d => d.Result).ToList();

                var row = 4;
                foreach (var res in finalResults)
                {
                    reportsheet.Cells["D2"].Value = finalResults.First().DOName;

                    reportsheet.Cells["B" + row].Value = res.SiteName;
                    reportsheet.Cells["C" + row].Value = res.SourceName;
                    reportsheet.Cells["D" + row].Value = res.FuelName;
                    reportsheet.Cells["E" + row].Value = res.Category;
                    reportsheet.Cells["F" + row].Value = res.Result;
                    //reportsheet.Cells["G" + row].Value = res.percentage;
                    reportsheet.Cells["G" + row].Formula = "=F" + row + "/SUM(F4:F"+(4+finalResults.Count-1)+")*100";

                    row++;
                }

                reportsheet.Cells["E" + row].Value = "Всего:";
                reportsheet.Cells["E" + row].Style.Font.Bold = true;
                reportsheet.Cells["E" + row].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                reportsheet.Cells["E" + row].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                reportsheet.Cells["F" + row].Formula = "=SUM(F4:F" + (row - 1) + ")";
                reportsheet.Cells["G" + row].Formula = "=SUM(G4:G" + (row - 1) + ")";

                package.Save();
            }

            Process.Start(ReportsDirectory + filename);

        }


    }
}
