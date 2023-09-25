using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eco.Model
{
    class SourceOfEmission
    {
        public int id { get; set; }
        public int ProductionSite_id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }

    }
}
