using BusinessModel.Contracts;
using BusinessModel.Models;
using Microsoft.AspNetCore.Mvc;
using VehicleManagement.Mappers;
using VehicleManagement.Models;

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

        // GET: api/v1/vehicles
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var result = await _vehicleService.GetVehicles();
            return Ok(result.Select(x => x.ToDto()));

            // Alternative: anonymous type verwenden
            // Dann sparen wir uns die explizite Definition von VehicleResultDto
            //return Ok(result.Select(x => new
            //{
            //    x.Id,
            //    x.Fuel,
            //    x.Manufacturer,
            //    x.Registered,
            //    Color = x.Color.ToString()
            //}));
        }

        // GET api/v1/vehicles/00000000-0000-0000-0000-000000000001
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<VehicleResultDto>> GetAsync(Guid id)
        {
            var result = await _vehicleService.GetVehicleById(id);
            if (result == null)
            {
                return NotFound();
            }
            
            //return Ok(VehiclesMapper.ToDto(result));
            return Ok(result.ToDto());
        }

        // POST api/v1/vehicles
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] CreatedVehicleDto vehicle)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var id = await _vehicleService.AddVehicle(vehicle.ToDomainModel());
                    return Ok(id);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                // Best Practice: Keine Fehlerdetails vom Server zurückgeben
                return BadRequest("An error occurred");
            }
            return BadRequest(ModelState);
        }

        // PUT api/v1/vehicles/5
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> PutAsync(Guid id, [FromBody] CreatedVehicleDto vehicle)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var success = await _vehicleService.UpdateVehicle(id, vehicle.ToDomainModel());
                    return success ? NoContent() : NotFound();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                // Best Practice: Keine Fehlerdetails vom Server zurückgeben
                return BadRequest("An error occurred");
            }
            return BadRequest(ModelState);
        }

        // DELETE api/v1/vehicles/5
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
