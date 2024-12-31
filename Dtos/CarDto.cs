namespace CarManagement.Dtos
{
    public class CarDto
    {
        public int Id { get; set; }
        public string? Make { get; set; }
        public string? Model { get; set; }
        public int ProductionYear { get; set; }
        public string? LicensePlate { get; set; }
        public List<GarageDto> Garages { get; set; } = new List<GarageDto>();
        public List<int> GarageIds { get; set; } = new List<int>();
    }
}
