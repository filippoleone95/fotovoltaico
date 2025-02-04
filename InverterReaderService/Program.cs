using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using InverterReaderService.Services;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration.GetSection("SerialConfig");

// Aggiungi i servizi
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<SerialHandler>(sp => {
    var portName = config["PortName"];
    var baudRate = config["BaudRate"];
    var timeout = config["Timeout"];

    if (string.IsNullOrEmpty(portName) || string.IsNullOrEmpty(baudRate) || string.IsNullOrEmpty(timeout))
    {
        throw new ArgumentException("SerialConfig settings cannot be null or empty");
    }

    return new SerialHandler(
        portName, 
        int.Parse(baudRate), 
        int.Parse(timeout)
    );
});

var app = builder.Build();

// Configura il middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/", () => "Hello, world!");
app.MapControllers();

app.Run();