using MongoDB.Bson.Serialization.Attributes;
using System;

namespace KostenloseKurse.Dienste.Katalog.Modelle
{
    public class Kurs
    {

        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string ID { get; set; }
        public string Name { get; set; }
        public string Bezeichnung { get; set; }
        [BsonRepresentation(MongoDB.Bson.BsonType.Decimal128)]
        public decimal Preis { get; set; }
        public string BenutzerID { get; set; }
        public string Picture { get; set; }
        [BsonRepresentation(MongoDB.Bson.BsonType.DateTime)]
        public DateTime Erstellungsdatum { get; set; }
        public Eigenschaft Eigenschaft {get; set;}
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string KategorieID { get; set; }
        [BsonIgnore]
        public Kategorie Kategorie { get; set; }
    }
}
