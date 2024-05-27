using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZPA_Meteostanice.data
{
    //tato trida slouzi pouze pro vypis dat do data grid view, nezobrazuje id
    public class DataProjection
    {
        public DateTime Timestamp { get; set; }
        public double Temperature { get; set; }
        public double WindSpeed { get; set; }
        public double Humidity { get; set; }
        public double Pressure { get; set; }
    }
}
