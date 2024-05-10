namespace Garage.Models
{
    public class ParkingEvent
    {

        public int ParkingEventId { get; set; }
        public DateTime CheckInTime { get; set; }
        public DateTime? CheckOutTime { get; set; }
        public int VehicleId { get; set; }
        public Vehicle Vehicle { get; set; }
    }
}

