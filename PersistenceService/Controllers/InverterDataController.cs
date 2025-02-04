using Microsoft.AspNetCore.Mvc;
using PersistenceService.Models;
using PersistenceService.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PersistenceService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InverterDataController : ControllerBase
    {
        private readonly InverterDataService _inverterDataService;

        public InverterDataController(InverterDataService inverterDataService)
        {
            _inverterDataService = inverterDataService;
        }

        // 1) GET /api/inverterdata
        // Restituisce tutti i dati
        [HttpGet]
        public async Task<ActionResult<IEnumerable<InverterData>>> GetAll()
        {
            var allData = await _inverterDataService.GetAllDataAsync();
            return Ok(allData);
        }

        // 2) GET /api/inverterdata/latest
        // Restituisce il dato più recente
        [HttpGet("latest")]
        public async Task<ActionResult<InverterData>> GetLatest()
        {
            var latest = await _inverterDataService.GetLatestDataAsync();
            if (latest == null)
            {
                return NotFound("Nessun dato presente.");
            }
            return Ok(latest);
        }

        // 3) POST /api/inverterdata
        // Inserisce i dati ricevuti nel payload
        [HttpPost]
        public async Task<ActionResult<InverterData>> InsertData([FromBody] InverterData data)
        {
            if (data == null)
            {
                return BadRequest("Payload non valido o mancante.");
            }

            await _inverterDataService.InsertDataAsync(data);
            
            return NoContent();
        }
    }
}