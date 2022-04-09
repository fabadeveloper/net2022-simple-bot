using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleBotCore.Logic
{
    public class SimpleUser
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; }
        [BsonElement]
        public string Nome { get; set; }
        [BsonElement]
        public int Idade { get; set; }
        [BsonElement]
        public string Cor { get; set; }

        public SimpleUser(string userId)
        {
            this.id = userId ?? throw new ArgumentNullException(nameof(userId));
        }
    }
}
