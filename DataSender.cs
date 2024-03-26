using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZPA_Meteostanice
{
    internal class DataSender
    {
        public MeteoData sendData()
        {
            DateTime time = DateTime.Now;
            var random = new Random();

            return new MeteoData
            {
                temperature = random.NextDouble(),
                humidity = random.NextDouble(),
                pressure = random.NextDouble() * 50 - 10,
                windSpeed = random.NextDouble() * 100,
            };

            
        }
    }
}
