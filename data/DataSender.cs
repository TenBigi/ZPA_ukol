using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace ZPA_Meteostanice.data
{
    internal class DataSender
    {
        private Random random = new Random();
        private ObjectId[] stationIds;

        //pocatecni hodnoty
        private double currentTemperature = 20.0; 
        private double currentHumidity = 50.0; 
        private double currentPressure = 1013.25; 
        private double currentWindSpeed = 10.0; 

        public DataSender(ObjectId[] stationIds)
        {
            this.stationIds = stationIds;
        }

        public MeteoData sendData()
        {
            //vygenerovani novych hodnot s malymi odchylkami
            currentTemperature += random.NextDouble() * 2 - 1; 
            currentHumidity += random.NextDouble() * 2 - 1; 
            currentPressure += random.NextDouble() * 2 - 1; 
            currentWindSpeed += random.NextDouble() * 2 - 1; 

            //zajisteni realistickych hodnot
            currentTemperature = Math.Max(-10, Math.Min(40, currentTemperature));
            currentHumidity = Math.Max(0, Math.Min(100, currentHumidity));
            currentPressure = Math.Max(950, Math.Min(1050, currentPressure));
            currentWindSpeed = Math.Max(0, Math.Min(150, currentWindSpeed));

            //prirazeni dat k nahodne stanici
            var stationId = stationIds[random.Next(stationIds.Length)];

            return new MeteoData
            {
                timestamp = DateTime.Now,
                temperature = Math.Round(currentTemperature, 2),
                humidity = Math.Round(currentHumidity, 2),
                pressure = Math.Round(currentPressure, 2),
                windSpeed = Math.Round(currentWindSpeed, 2),
                StationId = stationId
            };
        }
    }
}

