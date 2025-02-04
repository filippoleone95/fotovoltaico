using System.Net.Http.Json;

namespace ApiGateway.Services
{
    public class DataCollectorService : BackgroundService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<DataCollectorService> _logger;

        public DataCollectorService(HttpClient httpClient, ILogger<DataCollectorService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    // Chiamata per leggere i dati dall'inverter
                    var inverterDataResponse = await _httpClient.GetAsync("http://inverter_reader_service/api/inverter/data", stoppingToken);

                    if (!inverterDataResponse.IsSuccessStatusCode)
                    {
                        _logger.LogError("Errore nella lettura dei dati dall'inverter.");
                        await Task.Delay(5000, stoppingToken); // Ritardo prima del prossimo tentativo
                        continue;
                    }

                    var inverterData = await inverterDataResponse.Content.ReadFromJsonAsync<Dictionary<string, object>>(cancellationToken: stoppingToken);

                    // Chiamata per leggere lo stato dell'inverter
                    var inverterStatusResponse = await _httpClient.GetAsync("http://inverter_reader_service/api/inverter/status", stoppingToken);

                    if (!inverterStatusResponse.IsSuccessStatusCode)
                    {
                        _logger.LogError("Errore nella lettura dello stato dell'inverter.");
                        await Task.Delay(5000, stoppingToken); // Ritardo prima del prossimo tentativo
                        continue;
                    }

                    var inverterStatus = await inverterStatusResponse.Content.ReadFromJsonAsync<Dictionary<string, object>>(cancellationToken: stoppingToken);

                    // Aggiunta dello stato dell'inverter ai dati dell'inverter
                    inverterData["status"] = inverterStatus["status"];

                    // Chiamata per salvare i dati nel Persistence Service
                    var persistenceResponse = await _httpClient.PostAsJsonAsync("http://persistence_service/api/InverterData", inverterData, stoppingToken);

                    if (!persistenceResponse.IsSuccessStatusCode)
                    {
                        _logger.LogError("Errore nel salvataggio dei dati.");
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Errore durante il ciclo di raccolta dati.");
                }

                await Task.Delay(60000, stoppingToken); // Intervallo di minuto
            }
        }
    }
}