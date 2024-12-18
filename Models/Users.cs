using System;
using System.Collections.Generic;  // Use List<T> instead of ArrayList
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace WebStore.Models
{
    public class Users
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? _id { get; set; }

        public string? fullNameCus { get; set; }

        public string? passWord { get; set; }

        // Use List<string> for roleIds
        public List<string>? roleIds { get; set; }

        public string? addressCus { get; set; }

        public string? emailCus { get; set; }

        public string? phoneNumCus { get; set; }

        public string? genderCus { get; set; }

        public string? rank { get; set; }

        public string? isActive { get; set; }

        // Use DateTime? for created and updated timestamps
        public DateTime? createdAt { get; set; }

        public DateTime? updatedAt { get; set; }

        public string? token { get; set; }
    }
}
