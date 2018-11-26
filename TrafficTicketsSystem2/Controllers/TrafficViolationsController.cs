using System;
using System.IO;
using System.Net;
using System.Web.Mvc;
using System.Collections.Generic;
using TrafficTicketsSystem2.Models;
using System.Runtime.Serialization.Formatters.Binary;

namespace TrafficTicketsSystem2.Controllers
{
    public class TrafficViolationsController : Controller
    {
        // GET: TrafficViolations
        public ActionResult Index()
        {
            FileStream fsTrafficViolations = null;
            BinaryFormatter bfTrafficViolations = new BinaryFormatter();
            List<TrafficViolation> violations = new List<TrafficViolation>();

            string strTrafficViolationsFile = Server.MapPath("~/App_Data/TrafficViolations.tts");

            if (System.IO.File.Exists(strTrafficViolationsFile))
            {
                using (fsTrafficViolations = new FileStream(strTrafficViolationsFile, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    violations = (List<TrafficViolation>)bfTrafficViolations.Deserialize(fsTrafficViolations);
                }
            }

            return View(violations);
        }

        // GET: TrafficViolations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            TrafficViolation violation = null;
            FileStream fsTrafficViolations = null;
            BinaryFormatter bfTrafficViolations = new BinaryFormatter();
            List<TrafficViolation> violations = new List<TrafficViolation>();
            string strTrafficViolationsFile = Server.MapPath("~/App_Data/TrafficViolations.tts");

            if (System.IO.File.Exists(strTrafficViolationsFile))
            {
                using (fsTrafficViolations = new FileStream(strTrafficViolationsFile, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    violations = (List<TrafficViolation>)bfTrafficViolations.Deserialize(fsTrafficViolations);
                }

                violation = violations.Find(tv => tv.TrafficViolationId == id);
            }

            if (violation == null)
            {
                return HttpNotFound();
            }

            return View(violation);
        }

        // GET: TrafficViolations/Create
        public ActionResult Create()
        {
            Random rndNumber = new Random();
            BinaryFormatter bfCameras = new BinaryFormatter();
            FileStream fsCameras = null, fsViolationsTypes = null;
            List<Camera> cameras = new List<Camera>();
            BinaryFormatter bfViolationsTypes = new BinaryFormatter();
            string strCamerasFile = Server.MapPath("~/App_Data/Cameras.tts");
            List<ViolationType> violationsTypes = new List<ViolationType>();
            string strViolationsTypesFile = Server.MapPath("~/App_Data/ViolationsTypes.tts");

            List<SelectListItem> itemsCameras = new List<SelectListItem>();
            List<SelectListItem> paymentsStatus = new List<SelectListItem>();
            List<SelectListItem> itemsPhotoAvailable = new List<SelectListItem>();
            List<SelectListItem> itemsVideoAvailable = new List<SelectListItem>();
            List<SelectListItem> itemsViolationsTypes = new List<SelectListItem>();

            if (System.IO.File.Exists(strCamerasFile))
            {
                using (fsCameras = new FileStream(strCamerasFile, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    cameras = (List<Camera>)bfCameras.Deserialize(fsCameras);

                    foreach (Camera cmr in cameras)
                    {
                        itemsCameras.Add(new SelectListItem() { Text = cmr.CameraNumber + " - " + cmr.Location, Value = cmr.CameraNumber });
                    }
                }
            }

            if (System.IO.File.Exists(strViolationsTypesFile))
            {
                using (fsViolationsTypes = new FileStream(strViolationsTypesFile, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    violationsTypes = (List<ViolationType>)bfViolationsTypes.Deserialize(fsViolationsTypes);

                    foreach (ViolationType vt in violationsTypes)
                    {
                        itemsViolationsTypes.Add(new SelectListItem() { Text = vt.ViolationName, Value = vt.ViolationName });
                    }
                }
            }

            itemsPhotoAvailable.Add(new SelectListItem() { Value = "Unknown" });
            itemsPhotoAvailable.Add(new SelectListItem() { Text = "No", Value = "No" });
            itemsPhotoAvailable.Add(new SelectListItem() { Text = "Yes", Value = "Yes" });
            itemsVideoAvailable.Add(new SelectListItem() { Value = "Unknown" });
            itemsVideoAvailable.Add(new SelectListItem() { Text = "No", Value = "No" });
            itemsVideoAvailable.Add(new SelectListItem() { Text = "Yes", Value = "Yes" });

            paymentsStatus.Add(new SelectListItem() { Value = "Unknown" });
            paymentsStatus.Add(new SelectListItem() { Text = "Pending", Value = "Pending" });
            paymentsStatus.Add(new SelectListItem() { Text = "Rejected", Value = "Rejected" });
            paymentsStatus.Add(new SelectListItem() { Text = "Cancelled", Value = "Cancelled" });
            paymentsStatus.Add(new SelectListItem() { Text = "Paid Late", Value = "Paid Late" });
            paymentsStatus.Add(new SelectListItem() { Text = "Paid On Time", Value = "Paid On Time" });

            ViewBag.CameraNumber = itemsCameras;
            ViewBag.PaymentStatus = paymentsStatus;
            ViewBag.PhotoAvailable = itemsPhotoAvailable;
            ViewBag.VideoAvailable = itemsVideoAvailable;
            ViewBag.ViolationName = itemsViolationsTypes;
            ViewBag.TrafficViolationNumber = rndNumber.Next(10000001, 99999999);

            return View();
        }

        // POST: TrafficViolations/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here
                int trafficViolationId = 0;
                bool vehicleFound = false;
                List<Camera> cameras = new List<Camera>();
                List<Vehicle> vehicles = new List<Vehicle>();
                BinaryFormatter bfCameras = new BinaryFormatter();
                BinaryFormatter bfDrivers = new BinaryFormatter();
                int cameraId = 0, vehicleId = 0, violationTypeId = 0;
                BinaryFormatter bfViolationsTypes = new BinaryFormatter();
                List<ViolationType> violationsTypes = new List<ViolationType>();
                BinaryFormatter bfTrafficViolations = new BinaryFormatter();
                string strCamerasFile = Server.MapPath("~/App_Data/Cameras.tts");
                string strVehiclesFile = Server.MapPath("~/App_Data/Vehicles.tts");
                List<TrafficViolation> trafficViolations = new List<TrafficViolation>();
                string strViolationsTypesFile = Server.MapPath("~/App_Data/ViolationsTypes.tts");
                string strTrafficViolationsFile = Server.MapPath("~/App_Data/TrafficViolations.tts");
                FileStream fsTrafficViolations = null, fsVehicles = null, fsCameras = null, fsViolationsTypes = null;


                // Open the file for the cameras
                if (System.IO.File.Exists(strCamerasFile))
                {
                    using (fsCameras = new FileStream(strCamerasFile, FileMode.Open, FileAccess.Read, FileShare.Read))
                    {
                        /* After opening the file, get the list of cameras from it
                         * and store it in the List<Camera> variable. */
                        cameras = (List<Camera>)bfCameras.Deserialize(fsCameras);

                        // Check each record
                        foreach (Camera cmr in cameras)
                        {
                            /* When you get to a record, find out if its CameraNumber value 
                             * is the same as the camera number the user selected. */
                            if (cmr.CameraNumber == collection["CameraNumber"])
                            {
                                /* If you find a record with that tag number, make a note. */
                                cameraId = cmr.CameraId;
                                break;
                            }
                        }
                    }
                }

                // Open the file for the vehicles
                if (System.IO.File.Exists(strVehiclesFile))
                {
                    using (fsVehicles = new FileStream(strVehiclesFile, FileMode.Open, FileAccess.Read, FileShare.Read))
                    {
                        /* After opening the file, get the list of vehicles from it
                         * and store it in the List<Vehicle> variable. */
                        vehicles = (List<Vehicle>)bfDrivers.Deserialize(fsVehicles);

                        // Check each record
                        foreach (Vehicle car in vehicles)
                        {
                            /* When you get to a record, find out if its TagNumber value 
                             * is the same as the tag number the user provided. */
                            if (car.TagNumber == collection["TagNumber"])
                            {
                                /* If you find a record with that tag number, make a note. */
                                vehicleId = car.VehicleId;
                                vehicleFound = true;
                                break;
                            }
                        }
                    }
                }
                
                if (System.IO.File.Exists(strViolationsTypesFile))
                {
                    using (fsViolationsTypes = new FileStream(strViolationsTypesFile, FileMode.Open, FileAccess.Read, FileShare.Read))
                    {
                        violationsTypes = (List<ViolationType>)bfViolationsTypes.Deserialize(fsViolationsTypes);
                        
                        foreach (ViolationType vt in violationsTypes)
                        {
                            if (vt.ViolationName == collection["ViolationName"])
                            {
                                violationTypeId = vt.ViolationTypeId;
                                break;
                            }
                        }
                    }
                }

                /* If the vehicleFound is true, it means the car that committed the infraction has been identified. */
                if (vehicleFound == true)
                {
                    // Check whether a file for traffic violations was created already.
                    if (System.IO.File.Exists(strTrafficViolationsFile))
                    {
                        // If a file for traffic violations exists already, open it ...
                        using (fsTrafficViolations = new FileStream(strTrafficViolationsFile, FileMode.Open, FileAccess.Read, FileShare.Read))
                        {
                            // ... and store the records in our trafficViolations variable
                            trafficViolations = (List<TrafficViolation>)bfTrafficViolations.Deserialize(fsTrafficViolations);
                        }

                        foreach (TrafficViolation tv in trafficViolations)
                            trafficViolationId = tv.TrafficViolationId;
                    }

                    trafficViolationId++;

                    TrafficViolation citation = new TrafficViolation()
                    {
                        TrafficViolationId = trafficViolationId,
                        TrafficViolationNumber = int.Parse(collection["TrafficViolationNumber"]),
                        CameraId = cameraId,
                        VehicleId = vehicleId,
                        ViolationTypeId = violationTypeId,
                        ViolationDate = collection["ViolationDate"],
                        ViolationTime = collection["ViolationTime"],
                        PhotoAvailable = collection["PhotoAvailable"],
                        VideoAvailable = collection["VideoAvailable"],
                        PaymentDueDate = collection["PaymentDueDate"],
                        PaymentDate = collection["PaymentDate"],
                        PaymentAmount = double.Parse(collection["PaymentAmount"]),
                        PaymentStatus = collection["PaymentStatus"]
                    };

                    trafficViolations.Add(citation);

                    using (fsTrafficViolations = new FileStream(strTrafficViolationsFile, FileMode.Create, FileAccess.Write, FileShare.Write))
                    {
                        bfTrafficViolations.Serialize(fsTrafficViolations, trafficViolations);
                    }
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: TrafficViolations/Edit/5
        public ActionResult Edit(int ? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            TrafficViolation violation = null;
            FileStream fsTrafficViolations = null;
            BinaryFormatter bfTrafficViolations = new BinaryFormatter();
            List<TrafficViolation> violations = new List<TrafficViolation>();
            string strTrafficViolationsFile = Server.MapPath("~/App_Data/TrafficViolations.tts");

            if (System.IO.File.Exists(strTrafficViolationsFile))
            {
                using (fsTrafficViolations = new FileStream(strTrafficViolationsFile, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    violations = (List<TrafficViolation>)bfTrafficViolations.Deserialize(fsTrafficViolations);
                }

                violation = violations.Find(viol => viol.TrafficViolationId == id);

                List<SelectListItem> itemsCameras = new List<SelectListItem>();
                List<SelectListItem> paymentsStatus = new List<SelectListItem>();
                List<SelectListItem> itemsPhotoAvailable = new List<SelectListItem>();
                List<SelectListItem> itemsVideoAvailable = new List<SelectListItem>();
                List<SelectListItem> itemsViolationsTypes = new List<SelectListItem>();

                List<Camera> cameras = new List<Camera>();
                BinaryFormatter bfCameras = new BinaryFormatter();
                FileStream fsCameras = null, fsViolationsTypes = null;
                string strCamerasFile = Server.MapPath("~/App_Data/Cameras.tts");

                if (System.IO.File.Exists(strCamerasFile))
                {
                    using (fsCameras = new FileStream(strCamerasFile, FileMode.Open, FileAccess.Read, FileShare.Read))
                    {
                        cameras = (List<Camera>)bfCameras.Deserialize(fsCameras);

                        foreach (Camera cmr in cameras)
                        {
                            itemsCameras.Add(new SelectListItem() { Text = cmr.CameraNumber, Value = cmr.CameraNumber, Selected = (violation.CameraId == cmr.CameraId) });
                        }
                    }
                }

                BinaryFormatter bfViolationsTypes = new BinaryFormatter();
                List<ViolationType> violationsTypes = new List<ViolationType>();
                string strViolationsTypesFile = Server.MapPath("~/App_Data/ViolationsTypes.tts");

                if (System.IO.File.Exists(strViolationsTypesFile))
                {
                    using (fsViolationsTypes = new FileStream(strViolationsTypesFile, FileMode.Open, FileAccess.Read, FileShare.Read))
                    {
                        violationsTypes = (List<ViolationType>)bfViolationsTypes.Deserialize(fsViolationsTypes);

                        foreach (ViolationType vt in violationsTypes)
                        {
                            itemsViolationsTypes.Add(new SelectListItem() { Text = vt.ViolationName, Value = vt.ViolationName, Selected = (violation.ViolationTypeId == vt.ViolationTypeId) });
                        }
                    }
                }

                itemsPhotoAvailable.Add(new SelectListItem() { Value = "Unknown" });
                itemsPhotoAvailable.Add(new SelectListItem() { Text = "No", Value = "No", Selected = (violation.PhotoAvailable == "No") });
                itemsPhotoAvailable.Add(new SelectListItem() { Text = "Yes", Value = "Yes", Selected = (violation.PhotoAvailable == "Yes") });
                itemsVideoAvailable.Add(new SelectListItem() { Value = "Unknown" });
                itemsVideoAvailable.Add(new SelectListItem() { Text = "No", Value = "No", Selected = (violation.VideoAvailable == "No") });
                itemsVideoAvailable.Add(new SelectListItem() { Text = "Yes", Value = "Yes", Selected = (violation.VideoAvailable == "Yes") });

                paymentsStatus.Add(new SelectListItem() { Value = "Unknown" });
                paymentsStatus.Add(new SelectListItem() { Text = "Pending", Value = "Pending", Selected = (violation.PaymentStatus == "Pending") });
                paymentsStatus.Add(new SelectListItem() { Text = "Rejected", Value = "Rejected", Selected = (violation.PaymentStatus == "Rejected") });
                paymentsStatus.Add(new SelectListItem() { Text = "Cancelled", Value = "Cancelled", Selected = (violation.PaymentStatus == "Cancelled") });
                paymentsStatus.Add(new SelectListItem() { Text = "Paid Late", Value = "Paid Late", Selected = (violation.PaymentStatus == "Paid Late") });
                paymentsStatus.Add(new SelectListItem() { Text = "Paid On Time", Value = "Paid On Time", Selected = (violation.PaymentStatus == "Paid On Time") });

                Vehicle car = null;
                FileStream fsVehicles = null;
                string strTagNumber = string.Empty;
                List<Vehicle> vehicles = new List<Vehicle>();
                BinaryFormatter bfVehicles = new BinaryFormatter();
                string strVehiclesFile = Server.MapPath("~/App_Data/Vehicles.tts");

                if (System.IO.File.Exists(strVehiclesFile))
                {
                    using (fsVehicles = new FileStream(strVehiclesFile, FileMode.Open, FileAccess.Read, FileShare.Read))
                    {
                        vehicles = (List<Vehicle>)bfVehicles.Deserialize(fsVehicles);
                    }

                    car = vehicles.Find(engine => engine.VehicleId == violation.VehicleId);
                }

                ViewBag.CameraNumber = itemsCameras;
                ViewBag.PaymentStatus = paymentsStatus;
                ViewBag.TagNumber = car.TagNumber;
                ViewBag.PaymentDate = violation.PaymentDate;
                ViewBag.ViolationName = itemsViolationsTypes;
                ViewBag.PhotoAvailable = itemsPhotoAvailable;
                ViewBag.VideoAvailable = itemsVideoAvailable;
                ViewBag.PaymentAmount = violation.PaymentAmount;
                ViewBag.ViolationDate = violation.ViolationDate;
                ViewBag.ViolationTime = violation.ViolationTime;
                ViewBag.PaymentDueDate = violation.PaymentDueDate;
                ViewBag.TrafficViolationNumber = violation.TrafficViolationNumber;
            }

            if (violation == null)
            {
                return HttpNotFound();
            }

            return View(violation);
        }

        // POST: TrafficViolations/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
                FileStream fsTrafficViolations = null;
                TrafficViolation violation = new TrafficViolation();
                BinaryFormatter bfTrafficViolations = new BinaryFormatter();
                List<TrafficViolation> violations = new List<TrafficViolation>();
                string strTrafficViolationsFile = Server.MapPath("~/App_Data/TrafficViolations.tts");

                if (!string.IsNullOrEmpty("TrafficViolationNumber"))
                {
                    if (System.IO.File.Exists(strTrafficViolationsFile))
                    {
                        using (fsTrafficViolations = new FileStream(strTrafficViolationsFile, FileMode.Open, FileAccess.Read, FileShare.Read))
                        {
                            violations = (List<TrafficViolation>)bfTrafficViolations.Deserialize(fsTrafficViolations);
                        }
                    }

                    violation = violations.FindLast(viol => viol.TrafficViolationId == id);

                    int cameraId = 0;
                    FileStream fsCameras = null;
                    List<Camera> cameras = new List<Camera>();
                    BinaryFormatter bfCameras = new BinaryFormatter();
                    string strCamerasFile = Server.MapPath("~/App_Data/Cameras.tts");

                    if (System.IO.File.Exists(strCamerasFile))
                    {
                        using (fsCameras = new FileStream(strCamerasFile, FileMode.Open, FileAccess.Read, FileShare.Read))
                        {
                            cameras = (List<Camera>)bfCameras.Deserialize(fsCameras);
                        }

                        foreach(Camera cmr in cameras)
                        {
                            if(cmr.CameraNumber == collection["CameraNumber"])
                            {
                                cameraId = cmr.CameraId;
                                break;
                            }
                        }
                    }

                    int vehicleId = 0;
                    FileStream fsVehicles = null;
                    List<Vehicle> vehicles = new List<Vehicle>();
                    BinaryFormatter bfVehicles = new BinaryFormatter();
                    string strVehiclesFile = Server.MapPath("~/App_Data/Vehicles.tts");

                    if (System.IO.File.Exists(strVehiclesFile))
                    {
                        using (fsVehicles = new FileStream(strVehiclesFile, FileMode.Open, FileAccess.Read, FileShare.Read))
                        {
                            vehicles = (List<Vehicle>)bfVehicles.Deserialize(fsVehicles);
                        }

                        foreach(Vehicle car in vehicles)
                        {
                            if(car.TagNumber == collection["TagNumber"])
                            {
                                vehicleId = car.VehicleId;
                                break;
                            }
                        }
                    }

                    int violationTypeId = 0;
                    FileStream fsViolationsTypes = null;
                    BinaryFormatter bfViolationsTypes = new BinaryFormatter();
                    List<ViolationType> violationsTypes = new List<ViolationType>();
                    string strViolationsTypesFile = Server.MapPath("~/App_Data/ViolationsTypes.tts");

                    if (System.IO.File.Exists(strViolationsTypesFile))
                    {
                        using (fsViolationsTypes = new FileStream(strViolationsTypesFile, FileMode.Open, FileAccess.Read, FileShare.Read))
                        {
                            violationsTypes = (List<ViolationType>)bfViolationsTypes.Deserialize(fsViolationsTypes);
                        }

                        foreach(ViolationType vt in violationsTypes)
                        {
                            if(vt.ViolationName == collection["ViolationName"])
                            {
                                violationTypeId = vt.ViolationTypeId;
                                break;
                            }
                        }
                    }

                    violation.TrafficViolationNumber = int.Parse(collection["TrafficViolationNumber"]);
                    violation.CameraId = cameraId;
                    violation.VehicleId = vehicleId;
                    violation.ViolationTypeId = violationTypeId;
                    violation.ViolationDate = collection["ViolationDate"];
                    violation.ViolationTime = collection["ViolationTime"];
                    violation.PhotoAvailable = collection["PhotoAvailable"];
                    violation.VideoAvailable = collection["VideoAvailable"];
                    violation.PaymentDueDate = collection["PaymentDueDate"];
                    violation.PaymentDate = collection["PaymentDate"];
                    violation.PaymentAmount = double.Parse(collection["PaymentAmount"]);
                    violation.PaymentStatus = collection["PaymentStatus"];

                    using (fsTrafficViolations = new FileStream(strTrafficViolationsFile, FileMode.Create, FileAccess.Write, FileShare.Write))
                    {
                        bfTrafficViolations.Serialize(fsTrafficViolations, violations);
                    }
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: TrafficViolations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            TrafficViolation violation = null;
            FileStream fsTrafficViolations = null;
            BinaryFormatter bfTrafficViolations = new BinaryFormatter();
            List<TrafficViolation> violations = new List<TrafficViolation>();
            string strTrafficViolationsFile = Server.MapPath("~/App_Data/TrafficViolations.tts");

            if (System.IO.File.Exists(strTrafficViolationsFile))
            {
                using (fsTrafficViolations = new FileStream(strTrafficViolationsFile, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    violations = (List<TrafficViolation>)bfTrafficViolations.Deserialize(fsTrafficViolations);
                }

                violation = violations.Find(tv => tv.TrafficViolationId == id);
            }

            if (violation == null)
            {
                return HttpNotFound();
            }

            return View(violation);
        }

        // POST: TrafficViolations/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                FileStream fsTrafficViolations = null;
                TrafficViolation violation = new TrafficViolation();
                BinaryFormatter bfTrafficViolations = new BinaryFormatter();
                List<TrafficViolation> violations = new List<TrafficViolation>();
                string strTrafficViolationsFile = Server.MapPath("~/App_Data/TrafficViolations.tts");

                if (!string.IsNullOrEmpty("TrafficViolationNumber"))
                {
                    if (System.IO.File.Exists(strTrafficViolationsFile))
                    {
                        using (fsTrafficViolations = new FileStream(strTrafficViolationsFile, FileMode.Open, FileAccess.Read, FileShare.Read))
                        {
                            violations = (List<TrafficViolation>)bfTrafficViolations.Deserialize(fsTrafficViolations);
                        }
                    }

                    TrafficViolation tv = violations.Find(viol => viol.TrafficViolationId == id);

                    violations.Remove(tv);

                    using (fsTrafficViolations = new FileStream(strTrafficViolationsFile, FileMode.Create, FileAccess.Write, FileShare.Write))
                    {
                        bfTrafficViolations.Serialize(fsTrafficViolations, violations);
                    }
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        /* This function takes the available status of a photo and video taken during the traffic violation.
           The function returns a sentence that will instruct the driver about how to view the photo or video or the violation. */
        private string GetPhotoVideoOptions(string photo, string video)
        {
            var sentence = string.Empty;
            var pvAvailable = "No";

            if (photo == "Yes")
            {
                if (video == "Yes")
                {
                    pvAvailable = "Yes";
                    sentence = "To view the photo and/or video";
                }
                else
                {
                    pvAvailable = "Yes";
                    sentence = "To see a photo";
                }
            }
            else
            { // if (photo == "No")
                if (video == "Yes")
                {
                    pvAvailable = "Yes";
                    sentence = "To review a video";
                }
                else
                {
                    pvAvailable = "No";
                }
            }

            if (pvAvailable == "No")
                return "There is no photo or video available of this infraction but the violation was committed.";
            else
                return sentence + " of this violation, please access http://www.trafficviolationsmedia.com. In the form, enter the citation number and click Submit.";
        }

        // GET: Drivers/Citation/
        public ActionResult Citation(int id)
        {
            FileStream fsTrafficViolations = null;
            BinaryFormatter bfTrafficViolations = new BinaryFormatter();
            List<TrafficViolation> violations = new List<TrafficViolation>();
            string strTrafficViolationsFile = Server.MapPath("~/App_Data/TrafficViolations.tts");

            if (System.IO.File.Exists(strTrafficViolationsFile))
            {
                using (fsTrafficViolations = new FileStream(strTrafficViolationsFile, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    violations = (List<TrafficViolation>)bfTrafficViolations.Deserialize(fsTrafficViolations);
                }

                TrafficViolation violation = violations.Find(viol => viol.TrafficViolationId == id);

                ViewBag.TrafficViolationNumber = violation.TrafficViolationNumber;

                ViewBag.ViolationDate = violation.ViolationDate;
                ViewBag.ViolationTime = violation.ViolationTime;
                ViewBag.PaymentAmount = violation.PaymentAmount.ToString("C");
                ViewBag.PaymentDueDate = violation.PaymentDueDate;

                ViewBag.PhotoVideo = GetPhotoVideoOptions(violation.PhotoAvailable, violation.VideoAvailable);

                FileStream fsViolationsTypes = null;
                ViolationType category = new ViolationType();
                BinaryFormatter bfViolationsTypes = new BinaryFormatter();
                List<ViolationType> violationsTypes = new List<ViolationType>();
                string strViolationsTypesFile = Server.MapPath("~/App_Data/ViolationsTypes.tts");

                if (System.IO.File.Exists(strViolationsTypesFile))
                {
                    using (fsViolationsTypes = new FileStream(strViolationsTypesFile, FileMode.Open, FileAccess.Read, FileShare.Read))
                    {
                        violationsTypes = (List<ViolationType>)bfViolationsTypes.Deserialize(fsViolationsTypes);
                    }
                    
                    ViolationType type = violationsTypes.Find(cat => cat.ViolationTypeId == violation.ViolationTypeId);
                    ViewBag.ViolationName = type.ViolationName;
                    ViewBag.ViolationDescription = type.Description;
                }

                FileStream fsVehicles = null;
                BinaryFormatter bfVehicles = new BinaryFormatter();
                List<Vehicle> vehicles = new List<Vehicle>();

                string strVehiclesFile = Server.MapPath("~/App_Data/Vehicles.tts");

                using (fsVehicles = new FileStream(strVehiclesFile, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    vehicles = (List<Vehicle>)bfVehicles.Deserialize(fsVehicles);
                }

                Vehicle vehicle = vehicles.Find(car => car.VehicleId == violation.VehicleId);

                ViewBag.Vehicle = vehicle.Make + " " + vehicle.Model + ", " +
                                  vehicle.Color + ", " + vehicle.VehicleYear + " (Tag # " +
                                  vehicle.TagNumber + ")";

                FileStream fsDrivers = null;
                BinaryFormatter bfDrivers = new BinaryFormatter();
                List<Driver> drivers = new List<Driver>();
                string strDriversFile = Server.MapPath("~/App_Data/Drivers.tts");

                using (fsDrivers = new FileStream(strDriversFile, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    drivers = (List<Driver>)bfDrivers.Deserialize(fsDrivers);
                }

                Driver person = drivers.Find(driver => driver.DriverId == violation.VehicleId);

                ViewBag.DriverName = person.FirstName + " " + person.LastName + " (Drv. Lic. # " + person.DrvLicNumber + ")";
                ViewBag.DriverAddress = person.Address + " " + person.City + ", " + person.State + " " + person.ZIPCode;

                FileStream fsCameras = null;
                BinaryFormatter bfCameras = new BinaryFormatter();
                List<Camera> cameras = new List<Camera>();
                string strCamerasFile = Server.MapPath("~/App_Data/Cameras.tts");

                using (fsCameras = new FileStream(strCamerasFile, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    cameras = (List<Camera>)bfCameras.Deserialize(fsCameras);
                }

                Camera viewer = cameras.Find(cmr => cmr.CameraId == violation.CameraId);

                ViewBag.Camera = viewer.Make + " " + viewer.Model + " (" + viewer.CameraNumber + ")";
                ViewBag.ViolationLocation = viewer.Location;
            }

            return View();
        }
    }
}
