namespace Garage.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string PersonalNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public int VehicleId { get; set; }
        public List<Vehicle> Vehicles { get; set; }
        public User()
        {
            Vehicles = new List<Vehicle>();
        }
    }
}
