namespace Garage.Models
{
    public class ParkingEvent
    {
        public int Id { get; set; }
        public DateTime CheckInTime { get; set; }
        public DateTime CheckoutTime { get; set; }
        public int VehicleId { get; set; }
        public Vehicle Vehicle { get; set; }
    }
}
