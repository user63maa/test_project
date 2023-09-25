using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eco.Model
{
    class CalcResult
    {

        [DisplayName("ДО")]
        public string DOName { get; set; }
        [DisplayName("Площадка")]
        public string SiteName { get; set; }

        [DisplayName("Агрегат")]
        public string SourceName { get; set; }

        [DisplayName("Топливо")]
        public string FuelName { get; set; }

        [DisplayName("Тип")]
        public string Category { get; set; }

        [DisplayName("Результат")]
        public double Result { get; set; }

        [DisplayName("Процент")]
        public double percentage { get; set; }

    }
}
