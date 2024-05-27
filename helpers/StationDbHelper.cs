using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZPA_Meteostanice.data;

namespace ZPA_Meteostanice.helpers
{
    internal class StationDbHelper
    {
        private readonly IMongoCollection<Station> _stationCollection;

        public StationDbHelper(string connectionString, string dbName) 
        {
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase(dbName);
            _stationCollection = database.GetCollection<Station>("stations");
        }

        public async Task<List<Station>> GetStationsAsync()
        {
            return await _stationCollection.Find(_ => true).ToListAsync();
        }

        public async Task AddStationAsync(Station station)
        {
            await _stationCollection.InsertOneAsync(station);
        }
    }
}
