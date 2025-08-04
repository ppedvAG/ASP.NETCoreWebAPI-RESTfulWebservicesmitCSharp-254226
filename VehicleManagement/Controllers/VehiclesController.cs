using BusinessModel.Contracts;
using BusinessModel.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace VehicleManagement.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class VehiclesController : ControllerBase
    {
        private readonly IVehicleService _vehicleService;
        private readonly ILogger<VehiclesController> _logger;

        public VehiclesController(IVehicleService vehicleService, ILogger<VehiclesController> logger)
        {
            _vehicleService = vehicleService;
            _logger = logger;
        }

        // GET: api/v1/<VehiclesController>
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var result = await _vehicleService.GetVehicles();
            return Ok(result);
        }

        // GET api/v1/<VehiclesController>/00000000-0000-0000-0000-000000000001
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<Auto>> GetAsync(Guid id)
        {
            var result = await _vehicleService.GetVehicleById(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        // POST api/v1/<VehiclesController>
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] Auto vehicle)
        {
            try
            {
                var id = await _vehicleService.AddVehicle(vehicle);
                return Ok(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                // Best Practice: Keine Fehlerdetails vom Server zurückgeben
                return BadRequest("An error occurred");
            }
        }

        // PUT api/v1/<VehiclesController>/5
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> PutAsync(Guid id, [FromBody] Auto vehicle)
        {
            try
            {
                var success = await _vehicleService.UpdateVehicle(id, vehicle);
                return success ? NoContent() : NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                // Best Practice: Keine Fehlerdetails vom Server zurückgeben
                return BadRequest("An error occurred");
            }
        }

        // DELETE api/v1/<VehiclesController>/5
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var success = await _vehicleService.DeleteVehicle(id);
                return success ? NoContent() : NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                // Best Practice: Keine Fehlerdetails vom Server zurückgeben
                return BadRequest("An error occurred");
            }
        }
    }
}
