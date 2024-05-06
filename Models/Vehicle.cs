using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Garage.Models
{

    [Index(nameof(RegNumber), IsUnique = true)]
    public class Vehicle
    {
        public int VehicleId { get; set; }
        public int UserId { get; set; }
        public User Owner { get; set; }
        [RegularExpression(@"^[A-Z]{3}\d{3}$", ErrorMessage = "Registration number must be in the format ABC123")]
        public string RegNumber { get; set; }
        [MaxLength(20)]
        public string Brand { get; set; } = "";
        [MaxLength(20)]
        public string? Model { get; set; } = "";
        [MaxLength(20)]
        public string Color { get; set; } = "";
        public int NumberOfWheels { get; set; }
        public int VehicleTypeId { get; set; }
        public VehicleType VehicleType { get; set; }        
    }
}
