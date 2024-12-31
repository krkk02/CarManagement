using System.ComponentModel.DataAnnotations;

namespace CarManagement.Data.Entities
{
    public class Car
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string? Make { get; set; }

        [Required]
        public string? Model { get; set; }

        [Required]
        public int ProductionYear { get; set; }

        [Required]
        public string? LicensePlate { get; set; }

        public ICollection<CarGarage> CarGarages { get; set; } = new HashSet<CarGarage>();
    }
}
