using MongoDB.Driver;
using Microsoft.Extensions.Configuration;
// YE LINE SABSE ZAROORI HAI: Models folder ko link karne ke liye
using PersonalFinanceTracker.Models;

namespace PersonalFinanceTracker.Data
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;

        public MongoDbContext(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("MongoDb")
                                   ?? "mongodb://localhost:27017";
            var client = new MongoClient(connectionString);
            _database = client.GetDatabase("PersonalFinanceDb");
        }

        // Transactions Collection
        // Agar error barkarar rahe, toh 'Models.Transaction' likh kar check karein
        public IMongoCollection<Transaction> Transactions =>
            _database.GetCollection<Transaction>("Transactions");

        // Categories Collection
        public IMongoCollection<Category> Categories =>
            _database.GetCollection<Category>("Categories");

        // Budgets Collection
        public IMongoCollection<Budget> Budgets =>
            _database.GetCollection<Budget>("Budgets");
    }
}