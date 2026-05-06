using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace PersonalFinanceTracker.Models
{
    public class Category
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        public string Color { get; set; } = "#28a745"; // Default green

        public bool IsForExpense { get; set; } = true;
    }
}