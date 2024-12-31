using CarManagement.Dtos;

namespace CarManagement.Services.Interfaces
{
    public interface ICarService
    {
        List<CarDto> GetAllCars(string? make, string? model, int? productionYear, string? licensePlate);
        CarDto? GetCarById(int id);
        CarDto CreateCar(CarDto dto);
        CarDto? UpdateCar(int id, CarDto dto);
        bool DeleteCar(int id);
    }
}
