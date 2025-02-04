using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;

namespace ApiGateway.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GatewayController : ControllerBase
    {
        private readonly HttpClient _httpClient;

        public GatewayController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Endpoint per richiamare i dati dall'Inverter e inviarli al Persistence Service
        [HttpPost("collect-data")]
        public async Task<IActionResult> CollectData()
        {
            // Chiamata al microservizio InverterReaderService per leggere i dati
            var inverterDataResponse = await _httpClient.GetAsync("http://inverter_reader_service/api/inverter/data");

            if (!inverterDataResponse.IsSuccessStatusCode)
                return StatusCode((int)inverterDataResponse.StatusCode, "Errore nella lettura dei dati dall'inverter.");

            var inverterData = await inverterDataResponse.Content.ReadFromJsonAsync<Dictionary<string, object>>();

            // Chiamata al microservizio InverterReaderService per leggere lo stato dell'inverter
            var inverterStatusResponse = await _httpClient.GetAsync("http://inverter_reader_service/api/inverter/status");

            if (!inverterStatusResponse.IsSuccessStatusCode)
                return StatusCode((int)inverterStatusResponse.StatusCode, "Errore nella lettura dello stato dell'inverter.");

            var inverterStatus = await inverterStatusResponse.Content.ReadFromJsonAsync<Dictionary<string, object>>();

            // Aggiunta dello stato dell'inverter ai dati dell'inverter
            inverterData["status"] = inverterStatus["status"];

            // Chiamata al microservizio PersistenceService per salvare i dati
            var persistenceResponse = await _httpClient.PostAsJsonAsync("http://persistence_service/api/InverterData", inverterData);

            if (!persistenceResponse.IsSuccessStatusCode)
                return StatusCode((int)persistenceResponse.StatusCode, "Errore nel salvataggio dei dati.");

            return Ok("Dati raccolti e salvati con successo.");
        }

        // Endpoint per recuperare gli ultimi dati
        [HttpGet("latest-data")]
        public async Task<IActionResult> GetLatestData()
        {
            // Chiamata al microservizio PersistenceService per ottenere gli ultimi dati
            var response = await _httpClient.GetAsync("http://persistence_service/api/InverterData/latest");

            if (!response.IsSuccessStatusCode)
                return StatusCode((int)response.StatusCode, "Errore nel recupero dei dati.");

            var latestData = await response.Content.ReadFromJsonAsync<object>();
            return Ok(latestData);
        }
    }
}