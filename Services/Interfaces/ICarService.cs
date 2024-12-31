using CarManagement.Dtos;

namespace CarManagement.Services.Interfaces
{
    public interface ICarService
    {
        List<CarDto> GetAllCars(string? makeFilter, string? modelFilter, int? garageId, int? fromYear, int? toYear, string? licensePlateFilter);
        CarDto? GetCarById(int id);
        CarDto CreateCar(CarDto dto);
        CarDto? UpdateCar(int id, CarDto dto);
        bool DeleteCar(int id);
    }
}
