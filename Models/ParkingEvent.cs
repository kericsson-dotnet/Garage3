﻿using System.ComponentModel.DataAnnotations.Schema;

namespace Garage.Models
{
    public class ParkingEvent
    {
        public int ParkingEventId { get; set; }
        public DateTime CheckInTime { get; set; }
        public DateTime CheckOutTime { get; set; }
        public Vehicle Vehicle { get; set; }
       
    }
}
