using System.Net.Http.Json;
using WebApp.Models;

namespace WebApp.Services
{
    public class InverterDataService
    {
        private readonly HttpClient _httpClient;

        public InverterDataService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<InverterData> GetLatestDataAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("http://192.168.1.219:5000/api/gateway/latest-data");
                response.EnsureSuccessStatusCode();
                var data = await response.Content.ReadFromJsonAsync<InverterData>();
                return data ?? new InverterData();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Errore nel recupero dei dati: {ex.Message}");
                return new InverterData();
            }
        }
    }
}