using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarManagement.Data.Entities
{
    public class CarGarage
    {
        public int CarId { get; set; }

        [ForeignKey(nameof(CarId))]
        public Car? Car { get; set; }

        public int GarageId { get; set; }

        [ForeignKey(nameof(GarageId))]
        public Garage? Garage { get; set; }
    }
}
