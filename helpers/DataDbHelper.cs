using MongoDB.Driver;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZPA_Meteostanice.data;

namespace ZPA_Meteostanice.helpers
{
    internal class DataDbHelper
    {
        public IMongoDatabase database;
        public IMongoCollection<MeteoData> _dataCollection;

        //nova instance rovnou pripoji k databazi a kolekci
        public DataDbHelper(string connectionString, string dbName)
        {
            var client = new MongoClient(connectionString);
            database = client.GetDatabase(dbName);
            _dataCollection = database.GetCollection<MeteoData>("data");
        }

        public async Task<List<MeteoData>> GetDataByStationAsync(ObjectId stationId)
        {
            var filter = Builders<MeteoData>.Filter.Eq("StationId", stationId);
            return await _dataCollection.Find(filter).ToListAsync();
        }

        public async Task InsertDataAsync(MeteoData data)
        {
            await _dataCollection.InsertOneAsync(data);
        }


    }
}
