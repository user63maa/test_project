using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Eco.Model
{
    public class ProductionSide
    {
        public int id { get; set; }
        public int CompanyDO_id { get; set; }
        public string Name { get; set; }
        public string Region { get; set; }
        public string AdministrativeArea { get; set; }


    }
}
