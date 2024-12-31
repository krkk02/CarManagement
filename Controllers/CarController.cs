using CarManagement.Dtos;
using CarManagement.Services.Interfaces;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace CarManagement.Controllers
{
    [ApiController]
    [Route("cars")]
    public class CarController : ControllerBase
    {
        private readonly ICarService _carService;
        public CarController(ICarService carService)
        {
            _carService = carService;
        }

        [HttpGet]
        public IActionResult GetAll(
            [FromQuery] string? make,
            [FromQuery] string? model,
            [FromQuery] int? productionYear,
            [FromQuery] string? licensePlate)
        {
            var cars = _carService.GetAllCars(make, model, productionYear, licensePlate);
            return Ok(cars);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var car = _carService.GetCarById(id);
            if (car == null) return NotFound();
            return Ok(car);
        }

        [HttpPost]
        public IActionResult Create(CarDto dto)
        {
            try
            {
                var created = _carService.CreateCar(dto);
                return Ok(created);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, CarDto dto)
        {
            try
            {
                var updated = _carService.UpdateCar(id, dto);
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
            var deleted = _carService.DeleteCar(id);
            if (!deleted) return NotFound();
            return Ok();
        }
    }
}
