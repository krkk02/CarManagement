using CarManagement.Dtos;
using CarManagement.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CarManagement.Controllers
{
    [ApiController]
    [Route("garages")]
    public class GarageController : ControllerBase
    {
        private readonly IGarageService _garageService;
        public GarageController(IGarageService garageService)
        {
            _garageService = garageService;
        }

        [HttpGet]
        public IActionResult GetAll([FromQuery] string? location)
        {
            var garages = _garageService.GetAllGarages(location);
            return Ok(garages);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var w = _garageService.GetGarageById(id);
            if (w == null) return NotFound();
            return Ok(w);
        }

        [HttpPost]
        public IActionResult Create(GarageDto dto)
        {
            try
            {
                var created = _garageService.CreateGarage(dto);
                return Ok(created);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, GarageDto dto)
        {
            try
            {
                var updated = _garageService.UpdateGarage(id, dto);
                if (updated == null) return NotFound();
                return Ok(updated);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var deleted = _garageService.DeleteGarage(id);
            if (!deleted) return NotFound();
            return Ok();
        }
    }
}
