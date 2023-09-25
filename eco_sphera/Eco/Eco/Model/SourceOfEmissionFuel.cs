using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eco.Model
{
    class SourceOfEmissionFuel
    {
        public int id { get; set; }
        public int SourceOfEmission_id { get; set; }
        public int TypeOfFuelTable_id { get; set; }
        public int CategoryOfFuel_id { get; set; }
        public string CategoryName { get; set; }
        public string TypeOfFuelName { get; set; }


    }
}
