using Microsoft.AspNetCore.Mvc;
using WebApp.Services;
using System.Diagnostics;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly InverterDataService _inverterDataService;

        public HomeController(InverterDataService inverterDataService)
        {
            _inverterDataService = inverterDataService;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var data = await _inverterDataService.GetLatestDataAsync();
                if (data != null)
                {
                    // Avvolgere l'oggetto in una lista per la visualizzazione
                    return View(new List<InverterData> { data });
                }
                return View(new List<InverterData>());
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Errore nel recupero dei dati: {ex.Message}");
                return View(new List<InverterData>()); // Visualizza una tabella vuota in caso di errore
            }
        }

        // Action per la privacy page
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new Models.ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}