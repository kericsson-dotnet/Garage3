﻿using System.ComponentModel.DataAnnotations;

namespace Garage.Models
{
    public class Vehicle
    {
        
        public int VehicleId { get; set; }
        
        public int UserId { get; set; }
       
        public User Owner { get; set; }
       
        public string RegNumber { get; set; }
       
        public string Brand { get; set; }
      
        public string Model { get; set; }
       
        
        public string Color { get; set; }
       
        public int NumberOfWheels { get; set; }

        // Foreign key property
        public int VehicleTypeId { get; set; }

        public VehicleType VehicleType { get; set; }

        // Navigation property for the parking event (one-to-one)
        public ParkingEvent ParkingEvent { get; set; }

        

        // Navigation property for the owning user
        public User User { get; set; }

           }
}
