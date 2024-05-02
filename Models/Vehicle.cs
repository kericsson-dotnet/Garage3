namespace Garage.Models
{
    public class Vehicle
    {
        public User Owner { get; set; }
        public int Id { get; set; }
        public string RegNumber { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Color { get; set; }
        public int NumberOfWheels { get; set; }
        public VehicleType VehicleType { get; set; }        
    }
}
