using Microsoft.AspNetCore.Mvc;
using InverterReaderService.Services;

namespace InverterReaderService.Controllers
{
    [ApiController]
    [Route("api/inverter")]
    public class InverterController : ControllerBase
    {
        private readonly SerialHandler _serialHandler;

        public InverterController(SerialHandler serialHandler)
        {
            _serialHandler = serialHandler;
        }

        [HttpGet("data")]
        public IActionResult GetInverterData()
        {
            try
            {
                string response = _serialHandler.SendCommand("QPIGS");
                var data = InverterDataParser.ParseInverterData(response);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpGet("status")]
        public IActionResult GetInverterStatus()
        {
            try
            {
                string response = _serialHandler.SendCommand("QMOD");

                if (string.IsNullOrEmpty(response))
                {
                    Console.WriteLine("Received empty response for status.");
                    return StatusCode(500, new { error = "Empty response from inverter" });
                }

                var status = InverterStatusParser.ParseInverterStatus(response);
                return Ok(new { status });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving inverter status: {ex.Message}");
                return StatusCode(500, new { error = ex.Message });
            }
        }
    }
}