using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eco.Model
{
    class ExcelReportObject
    {
        public int DOid { get; set; }
        public string DOName { get; set; }
        public int SiteId { get; set; }
        public string SiteName { get; set; }
        public int SourceId { get; set; }
        public string SourceName { get; set; }
        public int FuelId { get; set; }
        public string FuelName { get; set; }
        public int CoefficientId { get; set; }
        public int CoefficientName { get; set; }
        public string CoefficientValue { get; set; }

    }

    class DOObjectForReport
    {
        public DOObjectForReport()
        {
            Sites = new List<SiteObjectForReport>();
        }
        public int id { get;set; }
        public string Name { get; set; }
        public List<SiteObjectForReport> Sites { get; set; }
    }

    class SiteObjectForReport
    {
        public SiteObjectForReport()
        {
            Sources = new List<SourceObjectForReport>();
        }
        public int id { get; set; }
        public string Name { get; set; }
        public string AdministrativeArea { get; set; }
        public string Region { get; set; }
        public List<SourceObjectForReport> Sources { get; set; }
    }

    class SourceObjectForReport
    {
        public SourceObjectForReport()
        {
            Fuels = new List<FuelObjectForReport>();
        }
        public int id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public List<FuelObjectForReport> Fuels { get; set; }
    }

    class FuelObjectForReport
    {
        public FuelObjectForReport()
        {
            Results = new List<ResultObjectForReport>();
        }
        public int id { get; set; }
        public string FuelName { get; set; }
        public int CategoryId { get; set; }
        public List<ResultObjectForReport> Results { get; set; }
    }
    class ResultObjectForReport
    {
        public ResultObjectForReport()
        {
            Coefficients = new List<CoefficientsForReport>();
        }
        public int id { get; set; }
        public string Name { get; set; }
        public double Value { get; set; }
        public List<CoefficientsForReport> Coefficients { get; set; }
    }

    class CoefficientsForReport
    {
        public int id { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
    }
}
