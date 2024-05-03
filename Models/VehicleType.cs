namespace Garage.Models
{
    public class VehicleType
    {
        public int VehicleTypeId { get; set; }
        public string TypeName { get; set; }
        public float SlotsOccupied { get; set; }

        // Navigation property for vehicles of this type
        // One-to-many relationship
        public List<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
    }
}

