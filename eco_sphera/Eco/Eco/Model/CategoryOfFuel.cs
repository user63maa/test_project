using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eco.Model
{
    class CategoryOfFuel
    {
        public int id { get; set; }
        public string CategoryName { get; set; }
        public string FuelTableName { get; set; }

    }

    class Fuel
    {
        public int id { get; set; }
        public string Name { get; set; }
    }

}
