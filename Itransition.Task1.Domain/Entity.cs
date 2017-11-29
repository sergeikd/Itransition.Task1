using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Itransition.Task1.Domain
{
    public class Entity
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } //for MongoDb
        //public  int Id { get; set; } //for SQL
    }
}
