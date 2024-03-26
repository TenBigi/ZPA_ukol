using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZPA_Meteostanice
{
    internal class MeteoData
    {
        //mapovani bson elementu v tomto pripade neni uplne nutne, je zde spis proto, abych si to osvojil

        [BsonId]
        public int id {  get; set; }
        [BsonElement]
        public DateTime timestamp {  get; set; }
        [BsonElement]
        public double temperature { get; set; }
        [BsonElement]
        public double windSpeed { get; set; }
        [BsonElement]
        public double humidity {  get; set; }
        [BsonElement]
        public double pressure { get; set; }

    }
}
