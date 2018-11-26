using System;
using System.ComponentModel.DataAnnotations;

namespace TrafficTicketsSystem2.Models
{
    [Serializable]
    public class ViolationType
    {
        [Display(Name = "Violation Type Id")]
        public int ViolationTypeId { get; set; }
        [Display(Name = "Violation Name")]
        public string ViolationName { get; set; }
        public string Description { get; set; }

        public override bool Equals(object obj)
        {
            ViolationType vt = (ViolationType)obj;

            return vt.ViolationTypeId == ViolationTypeId ? true : false;
        }

        public override int GetHashCode()
        {
            return ViolationTypeId;
        }
    }
}