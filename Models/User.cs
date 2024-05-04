namespace Garage.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string PersonalNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }

        // Initialize list in the property declaration
        public List<Vehicle> Vehicles { get; set; } = new List<Vehicle>();


        public User()
        {
            Vehicles = new List<Vehicle>();
        }
    }
}
