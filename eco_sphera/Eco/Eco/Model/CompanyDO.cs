using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Eco.Model
{
    class CompanyDO
    {
        public CompanyDO()
        {
            ListProductionSites = new List<ProductionSide>();
        }
        public int id { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public List<ProductionSide> ListProductionSites { get; set; }
    }
}
