using MongoDB.Driver;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZPA_Meteostanice
{
    internal class DatabaseHelper
    {
        public IMongoDatabase database;
        public IMongoCollection<BsonDocument> collection;

        public DatabaseHelper(string connectionString, string dbName, string collName)
        {
            var client = new MongoClient(connectionString);
            database = client.GetDatabase(dbName);
            collection = database.GetCollection<BsonDocument>(collName);
        }

        public string Search(string field, string value)
        {
            var filter = Builders<BsonDocument>.Filter.Eq(field, value);
            var result = collection.Find(filter).First();
            return result.ToJson();
        }
    }
}
