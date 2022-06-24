using MongoDB.Bson.Serialization.Attributes;

namespace KostenloseKurse.Dienste.Katalog.Modelle
{
    public class Kategorie
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string ID { get; set; }
        public string Name { get; set; }
    }
}
