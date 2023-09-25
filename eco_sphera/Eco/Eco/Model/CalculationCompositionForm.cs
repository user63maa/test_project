using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eco.Model
{
    class CalculationCompositionForm
    {
        public CalculationCompositionForm()
        {
        }

        public  CalculationCompositionForm(string name, string value)
        {
            this.ControlName = name;
            this.ControlValue = value;  
        }
        public int id { get; set; }
        public string ControlName { get; set; }
        public string ControlValue { get; set; }
        public int CalculationResult_id { get; set; }

    }
}
