namespace Garage.Models
{
    public class Receipt
    {
        public int Id { get; set; }
        public DateTime CheckInTime { get; set; }
        public DateTime? CheckOutTime { get; set; }
        public TimeSpan ParkedTime { get; set; }
        public Vehicle Vehicle { get; set; }

        /* Add addtiotional price info:
         * 1 parking slot + hourely price * time parked
         * 2 parking slot * 1.3 + hourely price * 1.4 * time parked
         * 3 parking slot * 1.6 + hourely price * 1.5 * time parked
         * */

    }
}

