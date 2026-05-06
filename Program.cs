using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

// --- MongoDB Configuration Section ---

// 1. appsettings.json se settings read karna
var mongoSettings = builder.Configuration.GetSection("MongoDB");
var connectionString = mongoSettings["ConnectionString"];
var databaseName = mongoSettings["DatabaseName"];

// 2. IMongoClient ko register karna (Singleton: Pure app ke liye aik client kafi hai)
builder.Services.AddSingleton<IMongoClient>(new MongoClient(connectionString));

// 3. IMongoDatabase ko register karna (Fixes image_758bb9.png error)
// Ye step system ko batata hai ke 'IMongoDatabase' kahan se fetch karna hai
builder.Services.AddScoped<IMongoDatabase>(sp =>
{
    var client = sp.GetRequiredService<IMongoClient>();
    return client.GetDatabase(databaseName);
});

// Razor Pages aur controllers ki support add karna
builder.Services.AddRazorPages();

var app = builder.Build();

// --- Middleware Pipeline ---

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();