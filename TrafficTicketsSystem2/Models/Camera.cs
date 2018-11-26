using System;
using System.ComponentModel.DataAnnotations;

namespace TrafficTicketsSystem2.Models
{
    [Serializable]
    public class Camera
    {
        [Display(Name = "Camera Id")]
        public int CameraId { get; set; }
        [Display(Name = "Camera #")]
        public string CameraNumber { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string Location { get; set; }

        // We need to override the Equals() method so we can compare one object to another for equality
        public override bool Equals(object obj)
        {
            Camera cmr = (Camera)obj;

            if (cmr.CameraId == CameraId)
                return true;

            return false;
        }

        public override int GetHashCode()
        {
            return CameraId;
        }
    }
}