using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace WebStore.Models
{
    public class Product
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? _id { get; set; }

        [BsonElement("TEN")]
        public string? TEN { get; set; }

        [BsonElement("PRICE")]
        public int? PRICE { get; set; }

        [BsonElement("SoluongTon")]
        public int? SoluongTon { get; set; }

        [BsonElement("DES")]
        public string? DES { get; set; }

        [BsonElement("SoLuongGG")]
        public int? SoLuongGG { get; set; }

        [BsonElement("CateID")]
        public string CateID { get; set; }

        [BsonElement("MaNXB")]
        public string MaNXB { get; set; }

        [BsonElement("IMG")]
        public string? IMG { get; set; }
    }
}
