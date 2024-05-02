namespace Garage3.Models
{
    public class User
    {
        public int Id { get; set; }
        public string PersonalNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public List<Vehicle> Vehicles { get; set; } // Navigation property for vehicles owned by the member

        // Constructor
        public User()
        {
            Vehicles = new List<Vehicle>();
        }
    }
}
