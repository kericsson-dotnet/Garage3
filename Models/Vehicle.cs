using System.ComponentModel.DataAnnotations;

namespace Garage.Models
{
    public class Vehicle
    {
        public int VehicleId { get; set; }
        public int UserId { get; set; }
        public User Owner { get; set; }
        public string RegNumber { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Color { get; set; }
        public int NumberOfWheels { get; set; }
        public VehicleType VehicleType { get; set; }
        
    }
}
