﻿using MongoDB.Driver;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZPA_Meteostanice.data;

namespace ZPA_Meteostanice.helpers
{
    internal class DatabaseHelper<T>
    {
        public IMongoDatabase database;
        public IMongoCollection<T> collection;

        //nova instance rovnou pripoji k databazi a kolekci
        public DatabaseHelper(string connectionString, string dbName, string collName)
        {
            var client = new MongoClient(connectionString);
            database = client.GetDatabase(dbName);
            collection = database.GetCollection<T>(collName);
        }

        public string Search(string field, string value)
        {
            var filter = Builders<T>.Filter.Eq(field, value);
            var result = collection.Find(filter).First();
            return result.ToJson();
        }

        public async Task<List<T>> GetDataAsync()
        {
            var dataList = await collection.Find(_ => true).ToListAsync();
            return dataList;
        }

        public List<T> GetData()
        {
            var dataList = collection.Find(_ => true).ToList();
            return dataList;
        }
    }
}
