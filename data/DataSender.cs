using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZPA_Meteostanice.data
{
    internal class DataSender
    {
        public MeteoData sendData()
        {
            var random = new Random();

            return new MeteoData
            {
                timestamp = DateTime.Now,
                temperature = random.NextDouble() * 50 - 10,
                humidity = random.NextDouble(),
                pressure = random.NextDouble(),
                windSpeed = random.NextDouble() * 100,
            };
        }
    }
}
