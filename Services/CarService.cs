using CarManagement.Data;
using CarManagement.Data.Entities;
using CarManagement.Dtos;
using CarManagement.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CarManagement.Services
{
    public class CarService : ICarService
    {
        private readonly AppDbContext _context;

        public CarService(AppDbContext context)
        {
            _context = context;
        }

        public List<CarDto> GetAllCars(string? makeFilter, string? modelFilter, int? garageId, int? fromYear, int? toYear, string? licensePlateFilter)
        {
            var query = _context.Cars
                .Include(c => c.CarGarages)
                    .ThenInclude(cg => cg.Garage)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(makeFilter))
                query = query.Where(c => c.Make == makeFilter);

            if (!string.IsNullOrWhiteSpace(modelFilter))
                query = query.Where(c => c.Model == modelFilter);

            if (garageId.HasValue)
                query = query.Where(c => c.CarGarages.Any(cg => cg.GarageId == garageId.Value));

            if (fromYear.HasValue)
                query = query.Where(c => c.ProductionYear >= fromYear.Value);

            if (toYear.HasValue)
                query = query.Where(c => c.ProductionYear <= toYear.Value);

            if (!string.IsNullOrWhiteSpace(licensePlateFilter))
                query = query.Where(c => c.LicensePlate == licensePlateFilter);

            return query.Select(c => new CarDto
            {
                Id = c.Id,
                Make = c.Make,
                Model = c.Model,
                ProductionYear = c.ProductionYear,
                LicensePlate = c.LicensePlate,
                Garages = c.CarGarages.Select(cg => new GarageDto
                {
                    Id = cg.Garage.Id,
                    Name = cg.Garage.Name,
                    City = cg.Garage.City,
                    Location = cg.Garage.Location,
                    Capacity = cg.Garage.Capacity
                }).ToList(),
                GarageIds = c.CarGarages.Select(cg => cg.GarageId).ToList()
            }).ToList();
        }

        public CarDto? GetCarById(int id)
        {
            var car = _context.Cars
                .Include(c => c.CarGarages)
                    .ThenInclude(cg => cg.Garage)
                .FirstOrDefault(c => c.Id == id);

            if (car == null)
                return null;

            return new CarDto
            {
                Id = car.Id,
                Make = car.Make,
                Model = car.Model,
                ProductionYear = car.ProductionYear,
                LicensePlate = car.LicensePlate,
                Garages = car.CarGarages.Select(cg => new GarageDto
                {
                    Id = cg.Garage.Id,
                    Name = cg.Garage.Name,
                    City = cg.Garage.City,
                    Location = cg.Garage.Location,
                    Capacity = cg.Garage.Capacity
                }).ToList(),
                GarageIds = car.CarGarages.Select(cg => cg.GarageId).ToList()
            };
        }

        public CarDto CreateCar(CarDto dto)
        {
            var car = new Car
            {
                Make = dto.Make,
                Model = dto.Model,
                ProductionYear = dto.ProductionYear,
                LicensePlate = dto.LicensePlate
            };

            _context.Cars.Add(car);
            _context.SaveChanges();

            if (dto.GarageIds != null)
            {
                foreach (var garageId in dto.GarageIds)
                {
                    _context.CarsGarages.Add(new CarGarage
                    {
                        CarId = car.Id,
                        GarageId = garageId
                    });
                }
                _context.SaveChanges();
            }

            return GetCarById(car.Id);
        }

        public CarDto? UpdateCar(int id, CarDto dto)
        {
            var car = _context.Cars.Include(c => c.CarGarages).FirstOrDefault(c => c.Id == id);
            if (car == null) return null;

            car.Make = dto.Make;
            car.Model = dto.Model;
            car.ProductionYear = dto.ProductionYear;
            car.LicensePlate = dto.LicensePlate;

            _context.CarsGarages.RemoveRange(car.CarGarages);
            if (dto.GarageIds != null)
            {
                foreach (var garageId in dto.GarageIds)
                {
                    _context.CarsGarages.Add(new CarGarage
                    {
                        CarId = car.Id,
                        GarageId = garageId
                    });
                }
            }

            _context.SaveChanges();

            return GetCarById(car.Id);
        }

        public bool DeleteCar(int id)
        {
            var car = _context.Cars.Find(id);
            if (car == null) return false;

            _context.Cars.Remove(car);
            _context.SaveChanges();
            return true;
        }
    }
}
