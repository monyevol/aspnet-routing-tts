using System;
using System.ComponentModel.DataAnnotations;

namespace TrafficTicketsSystem2.Models
{
    [Serializable]
    public class Vehicle
    {
        [Display(Name = "Vehicle Id")]
        public int VehicleId { get; set; }
        [Display(Name = "Tag #")]
        public string TagNumber { get; set; }
        [Display(Name = "Driver Id")]
        public int DriverId { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        [Display(Name = "Vehicle Year")]
        public string VehicleYear { get; set; }
        public string Color { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is Vehicle)
            {
                Vehicle car = (Vehicle)obj;

                if (car.VehicleId == VehicleId)
                    return true;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return VehicleId;
        }
    }
}