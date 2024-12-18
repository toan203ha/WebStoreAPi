using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Data.SqlTypes;

namespace WebStore.Models
{
    public class Product
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]

        public string? _id { get; set; }
        public string? TEN { get; set; }
        public string? DES { get; set; }
        public string? IMG { get; set; }
        public string? Hinh_2 { get; set; }
        public string? Hinh_3 { get; set; }
        public string? Hinh_4 { get; set; }
        public decimal? PRICE { get; set; }
        public List<string>? CateID { get; set; }
        public int? SoluongTon { get; set; }
        public int? soLuongGG { get; set; }
        public string? MaNXB { get; set; }
        public DateTime? createdAt { get; set; }
        public DateTime? updatedAt { get; set; }

    }
}
