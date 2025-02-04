using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PersistenceService.Persistence;
using PersistenceService.Services; // Aggiungi questa riga

var builder = WebApplication.CreateBuilder(args);

// Leggiamo la connection string da appsettings o variabili d’ambiente
string? connectionString = builder.Configuration.GetConnectionString("PostgresConnection")
    ?? Environment.GetEnvironmentVariable("POSTGRES_CONNSTRING");

// Stampa la connection string per debug (opzionale)
Console.WriteLine("Connection string in uso: " + connectionString);

// Registra il DbContext
builder.Services.AddDbContext<InverterDataContext>(options =>
{
    options.UseNpgsql(connectionString);
});

// REGISTRA InverterDataService
builder.Services.AddScoped<InverterDataService>();

// Aggiungi i controller
builder.Services.AddControllers();

// Costruisci l'app
var app = builder.Build();

// Applichiamo eventuali migrazioni all'avvio
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<InverterDataContext>();
    db.Database.Migrate(); 
}

// Mappa i controller
app.MapControllers();

app.Run();