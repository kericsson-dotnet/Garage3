namespace Garage3.Models
{
    public class ParkingEvent
    {
        public int Id { get; set; }
        public DateTime CheckInTime { get; set; }
        public DateTime CheckoutTime { get; set; }
        public Vehicle vehicle { get; set; }
    }
}
