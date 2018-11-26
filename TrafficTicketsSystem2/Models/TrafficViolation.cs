using System;
using System.ComponentModel.DataAnnotations;

namespace TrafficTicketsSystem2.Models
{
    [Serializable]
    public class TrafficViolation
    {
        [Display(Name = "Violation Id")]
        public int TrafficViolationId { get; set; }
        [Display(Name = "Traffic Violation #")]
        public int TrafficViolationNumber { get; set; }
        [Display(Name = "Camera #")]
        public int CameraId { get; set; }
        [Display(Name = "Vehicle Id")]
        public int VehicleId { get; set; }
        [Display(Name = "Violation Id")]
        public int ViolationTypeId { get; set; }
        [Display(Name = "Violation Date")]
        public string ViolationDate { get; set; }
        [Display(Name = "Violation Time")]
        public string ViolationTime { get; set; }
        [Display(Name = "Photo Available")]
        public string PhotoAvailable { get; set; }
        [Display(Name = "Video Available")]
        public string VideoAvailable { get; set; }
        [Display(Name = "Payment Due Date")]
        public string PaymentDueDate { get; set; }
        [Display(Name = "Payment Date")]
        public string PaymentDate { get; set; }
        [Display(Name = "Payment Amount")]
        public double PaymentAmount { get; set; }
        [Display(Name = "Payment Status")]
        public string PaymentStatus { get; set; }

        public override bool Equals(object obj)
        {
            TrafficViolation tv = (TrafficViolation)obj;

            return tv.TrafficViolationId == TrafficViolationId ? true : false;
        }

        public override int GetHashCode()
        {
            return TrafficViolationId;
        }
    }
}