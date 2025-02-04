using ApiGateway.Services;

var builder = WebApplication.CreateBuilder(args);

// Aggiungi i servizi necessari
builder.Services.AddControllers(); // Abilita i controller per l'API Gateway
builder.Services.AddHttpClient(); // Configura HttpClient per comunicare con i microservizi
builder.Services.AddHostedService<DataCollectorService>(); // Registra il servizio ciclico per la raccolta dati
builder.Services.AddEndpointsApiExplorer(); // Abilita l'esplorazione degli endpoint per Swagger
builder.Services.AddSwaggerGen(); // Configura Swagger per la documentazione API

var app = builder.Build();

// Configura la pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage(); // Mostra pagine di errore dettagliate in sviluppo
    app.UseSwagger(); // Abilita Swagger in ambiente di sviluppo
    app.UseSwaggerUI(); // Abilita l'interfaccia Swagger UI
}

app.UseHttpsRedirection(); // Reindirizza automaticamente a HTTPS
app.UseAuthorization(); // Configura middleware di autorizzazione

app.MapControllers(); // Mappa i controller per gestire le richieste API

app.Run(); // Avvia l'applicazione