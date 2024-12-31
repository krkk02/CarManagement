using CarManagement.Dtos;

namespace CarManagement.Services.Interfaces
{
    public interface IGarageService
    {
        List<GarageDto> GetAllGarages(string? locationFilter);
        GarageDto? GetGarageById(int id);
        GarageDto CreateGarage(GarageDto dto);
        GarageDto? UpdateGarage(int id, GarageDto dto);
        bool DeleteGarage(int id);
    }
}
