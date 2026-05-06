using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PersonalFinanceTracker.Models
{
    public class Budget
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public decimal TargetAmount { get; set; }
        public string Month { get; set; } = DateTime.Now.ToString("MMMM yyyy");
    }
}