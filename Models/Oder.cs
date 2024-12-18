using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using WebStore.Models.Enum;

namespace WebStore.Models
{
    public class Oders
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? _id { get; set; }

        public string? idCus { get; set; }

        public DateTime? ngayDat { get; set; }

        public DateTime? ngayNhan { get; set; }

        public string? address { get; set; }

        public string? tenUser { get; set; }

        public bool? thongBao { get; set; }

        public double? thanhTien { get; set; }

        public bool? daGiao { get; set; }

        public bool? daHuy { get; set; }

        public bool? dangChoXacNhan { get; set; }

        public string? maKM { get; set; }

        public OrderStatus? status { get; set; }

    }
}
