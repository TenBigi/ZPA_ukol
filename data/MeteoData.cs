using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZPA_Meteostanice.data
{
    internal class MeteoData
    {
        public DateTime timestamp { get; set; }
        public double temperature { get; set; }
        public double windSpeed { get; set; }
        public double humidity { get; set; }
        public double pressure { get; set; }
    }
}
