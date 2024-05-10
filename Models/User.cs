using System.ComponentModel.DataAnnotations;

namespace Garage.Models
{
    public class User
    {
        public int UserId { get; set; }
        [RegularExpression(@"^\d{6}-\d{4}$", ErrorMessage = "Personal number must be in the format YYMMDD-XXXX")]

        public string PersonalNumber { get; set; }
        [MaxLength(35)]
        public string FirstName { get; set; }
        [MaxLength(35)]
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
