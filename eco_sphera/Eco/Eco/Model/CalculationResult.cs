using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eco.Model
{
    class CalculationResult
    {

        public int id { get; set; }
        public int SourceOfEmission_id { get; set; }
        public double ResultSum { get; set; }
        public DateTime SaveData { get; set; }
        public string PersonnelLogin { get; set; }

    }
}
