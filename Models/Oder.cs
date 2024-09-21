using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace WebStore.Models
{
    public class Oders
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? _id { get; set; }

        [BsonElement("idCus")]
        public string? idCus { get; set; }

        [BsonElement("ngayDat")]
        public DateTime? ngayDat { get; set; }

        [BsonElement("ngayNhan")]
        public DateTime? ngayNhan { get; set; }

       

        [BsonElement("address")]
        public string? address { get; set; }

        [BsonElement("tenUser")]
        public string? tenUser { get; set; }

        [BsonElement("thongBao")]
        public bool? thongBao { get; set; }

        [BsonElement("thanhTien")]
        public double? thanhTien { get; set; }

        [BsonElement("daGiao")]
        public bool? daGiao { get; set; }

        [BsonElement("daHuy")]
        public bool? daHuy { get; set; }

        [BsonElement("dangChoXacNhan")]
        public bool? dangChoXacNhan { get; set; }

        [BsonElement("maKM")]
        public string? maKM { get; set; }
    }
}
