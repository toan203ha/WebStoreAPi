using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace WebStore.Models
{
    public class Users
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? _id { get; set; }   
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Img { get; set; }
        public string? tenuser { get; set; }
        public string? diaChi { get; set; }
        public string? genderCus { get; set; }
    }
}
