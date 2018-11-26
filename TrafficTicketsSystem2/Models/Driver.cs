using System;
using System.ComponentModel.DataAnnotations;

namespace TrafficTicketsSystem2.Models
{
    [Serializable]
    public class Driver
    {
        [Display(Name = "Driver Id")]
        public int DriverId { get; set; }
        [Display(Name = "Drv. Lic. #")]
        public string DrvLicNumber { get; set; }
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string County { get; set; }
        public string State { get; set; }
        [Display(Name = "ZIP Code")]
        public string ZIPCode { get; set; }

        public override bool Equals(object obj)
        {
            Driver dvr = (Driver)obj;

            return dvr.DriverId == DriverId ? true : false;
        }

        public override int GetHashCode()
        {
            return DriverId;
        }
    }
}