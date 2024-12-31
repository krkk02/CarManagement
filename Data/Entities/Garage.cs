using System.ComponentModel.DataAnnotations;

namespace CarManagement.Data.Entities
{
    public class Garage
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public required string Name { get; set; }

        [Required]
        public required string Location { get; set; }

        [Required]
        public required string City { get; set; }

        [Required]
        public int Capacity { get; set; }

    }
}
