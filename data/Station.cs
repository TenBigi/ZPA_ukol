using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZPA_Meteostanice.data
{
    internal class Station
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
    }
}
