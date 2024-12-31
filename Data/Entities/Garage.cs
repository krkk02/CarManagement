using System.ComponentModel.DataAnnotations;

namespace CarManagement.Data.Entities
{
    public class Garage
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; }

        [Required]
        public string? Location { get; set; }

        [Required]
        public string? City { get; set; }

        [Required]
        public int Capacity { get; set; }
    }
}
