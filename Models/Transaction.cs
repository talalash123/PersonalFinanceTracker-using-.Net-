using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace PersonalFinanceTracker.Models
{
    public class Transaction
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; } // MongoDB unique ID string format mein

        [Required(ErrorMessage = "Please enter a description")]
        [StringLength(100, ErrorMessage = "Description is too long")]
        public string Description { get; set; } = string.Empty;

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than 0")]
        public decimal Amount { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; } = DateTime.Now;

        [Required]
        public string Category { get; set; } = "General";

        // Boolean approach: True = Expense, False = Income
        // Is se dashboard par income aur expense alag karna asaan hota hai
        [Required]
        public bool IsExpense { get; set; } = true;

        public string? Note { get; set; }
    }
}