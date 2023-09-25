using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Eco.Model
{
    class CalculationCompositionForExport
    {
        public CalculationCompositionForExport(string value, int col, int row)
        {
            this.CellValue = value;
            this.ColNumber = col;
            this.RowNumber = row;
        }
        public CalculationCompositionForExport()
        {
        }

        public int id { get; set; }
        public string CellValue { get; set; }
        public int ColNumber { get; set; }
        public int RowNumber { get; set; }
        public int CalculationResult_id { get; set; }


    }
}
