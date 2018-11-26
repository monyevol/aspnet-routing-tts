using System.IO;
using System.Net;
using System.Web.Mvc;
using System.Collections.Generic;
using TrafficTicketsSystem2.Models;
using System.Runtime.Serialization.Formatters.Binary;

namespace TrafficTicketsSystem2.Controllers
{
    public class VehiclesController : Controller
    {
        // GET: Vehicles
        public ActionResult Index()
        {
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
            }

            return View(vehicles);
        }

        // GET: Vehicles/Details/5
        public ActionResult Details(int? id)
        {
            Vehicle car = null;
            FileStream fsVehicles = null;
            List<Vehicle> vehicles = new List<Vehicle>();
            BinaryFormatter bfVehicles = new BinaryFormatter();
            string strVehiclesFile = Server.MapPath("~/App_Data/Vehicles.tts");

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (System.IO.File.Exists(strVehiclesFile))
            {
                using (fsVehicles = new FileStream(strVehiclesFile, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    vehicles = (List<Vehicle>)bfVehicles.Deserialize(fsVehicles);
                }

                car = vehicles.Find(engine => engine.VehicleId == id);
            }

            if (car == null)
            {
                return HttpNotFound();
            }

            return View(car);
        }

        // GET: Vehicles/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Vehicles/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here
                bool driverFound = false;
                int vehicleId = 0, driverId = 0;
                List<Driver> drivers = new List<Driver>();
                List<Vehicle> vehicles = new List<Vehicle>();
                FileStream fsVehicles = null, fsDrivers = null;
                BinaryFormatter bfDrivers = new BinaryFormatter();
                BinaryFormatter bfVehicles = new BinaryFormatter();
                string strDriversFile = Server.MapPath("~/App_Data/Drivers.tts");
                string strVehiclesFile = Server.MapPath("~/App_Data/Vehicles.tts");

                // Open the file of the drivers records
                if (System.IO.File.Exists(strDriversFile))
                {
                    using (fsDrivers = new FileStream(strDriversFile, FileMode.Open, FileAccess.Read, FileShare.Read))
                    {
                        /* After opening the file, get the list of records from it
                         * and store in the empty LinkedList<> collection we started. */
                        drivers = (List<Driver>)bfDrivers.Deserialize(fsDrivers);

                        // Navigate to each record
                        foreach (Driver person in drivers)
                        {
                            /* When you get to a record, find out if its DrvLicNumber value 
                             * is the DrvLicNumber the user typed on the form. */
                            if (person.DrvLicNumber == collection["DrvLicNumber"])
                            {
                                /* If you find the record with that DrvLicNumber value, 
                                 * make a note to indicate it. */
                                driverId = person.DriverId;
                                driverFound = true;
                                break;
                            }
                        }
                    }
                }

                /* If the driverFound is true, it means the driver's license 
                 * number is valid, which means we can save the record. */
                if (driverFound == true)
                {
                    // Check whether a file for vehicles exists already.
                    if (System.IO.File.Exists(strVehiclesFile))
                    {
                        // If a file for vehicles exists already, open it ...
                        using (fsVehicles = new FileStream(strVehiclesFile, FileMode.Open, FileAccess.Read, FileShare.Read))
                        {
                            // ... and store the records in our vehicles variable
                            vehicles = (List<Vehicle>)bfVehicles.Deserialize(fsVehicles);
                        }

                        foreach (Vehicle engine in vehicles)
                            vehicleId = engine.VehicleId;
                    }

                    vehicleId++;

                    Vehicle car = new Vehicle()
                    {
                        VehicleId = vehicleId,
                        TagNumber = collection["TagNumber"],
                        DriverId = driverId,
                        Make = collection["Make"],
                        Model = collection["Model"],
                        VehicleYear = collection["VehicleYear"],
                        Color = collection["Color"]
                    };

                    vehicles.Add(car);

                    using (fsVehicles = new FileStream(strVehiclesFile, FileMode.Create, FileAccess.Write, FileShare.Write))
                    {
                        bfVehicles.Serialize(fsVehicles, vehicles);
                    }
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Vehicles/Edit/5
        public ActionResult Edit(int? id)
        {
            Vehicle car = null;
            string strDrvLicNumber = string.Empty;
            List<Driver> drivers = new List<Driver>();
            List<Vehicle> vehicles = new List<Vehicle>();
            FileStream fsVehicles = null, fsDrivers = null;
            BinaryFormatter bfDrivers = new BinaryFormatter();
            BinaryFormatter bfVehicles = new BinaryFormatter();
            string strDriversFile = Server.MapPath("~/App_Data/Drivers.tts");
            string strVehiclesFile = Server.MapPath("~/App_Data/Vehicles.tts");

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (System.IO.File.Exists(strVehiclesFile))
            {
                using (fsVehicles = new FileStream(strVehiclesFile, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    vehicles = (List<Vehicle>)bfVehicles.Deserialize(fsVehicles);
                }

                car = vehicles.Find(engine => engine.VehicleId == id);
            }

            if (System.IO.File.Exists(strDriversFile))
            {
                using (fsDrivers = new FileStream(strDriversFile, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    drivers = (List<Driver>)bfDrivers.Deserialize(fsDrivers);
                    
                    foreach (Driver person in drivers)
                    {
                        if (person.DriverId == car.DriverId)
                        {
                            ViewBag.DrvLicNumber = person.DrvLicNumber;
                            break;
                        }
                    }
                }
            }

            if (car == null)
            {
                return HttpNotFound();
            }

            return View(car);
        }

        // POST: Vehicles/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
                int driverId = 0;
                List<Driver> drivers = new List<Driver>();
                List<Vehicle> vehicles = new List<Vehicle>();
                FileStream fsVehicles = null, fsDrivers = null;
                BinaryFormatter bfDrivers = new BinaryFormatter();
                BinaryFormatter bfVehicles = new BinaryFormatter();
                string strDriversFile = Server.MapPath("~/App_Data/Drivers.tts");
                string strVehiclesFile = Server.MapPath("~/App_Data/Vehicles.tts");

                if (!string.IsNullOrEmpty("CameraNumber"))
                {
                    if (System.IO.File.Exists(strVehiclesFile))
                    {
                        using (fsVehicles = new FileStream(strVehiclesFile, FileMode.Open, FileAccess.Read, FileShare.Read))
                        {
                            vehicles = (List<Vehicle>)bfVehicles.Deserialize(fsVehicles);
                        }
                    }

                    Vehicle vehicle = vehicles.Find(car => car.VehicleId == id);

                    if (System.IO.File.Exists(strDriversFile))
                    {
                        using (fsDrivers = new FileStream(strDriversFile, FileMode.Open, FileAccess.Read, FileShare.Read))
                        {
                            drivers = (List<Driver>)bfDrivers.Deserialize(fsDrivers);

                            foreach (Driver person in drivers)
                            {
                                if (person.DrvLicNumber == collection["DrvLicNumber"])
                                {
                                    driverId = person.DriverId;
                                    break;
                                }
                            }
                        }
                    }

                    vehicle.TagNumber = collection["TagNumber"];
                    vehicle.DriverId = driverId;
                    vehicle.Make = collection["Make"];
                    vehicle.Model = collection["Model"];
                    vehicle.VehicleYear = collection["VehicleYear"];
                    vehicle.Color = collection["Color"];

                    using (fsVehicles = new FileStream(strVehiclesFile, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite))
                    {
                        bfVehicles.Serialize(fsVehicles, vehicles);
                    }
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Vehicles/Delete/5
        public ActionResult Delete(int? id)
        {
            Vehicle car = null;
            FileStream fsVehicles = null;
            List<Vehicle> vehicles = new List<Vehicle>();
            BinaryFormatter bfVehicles = new BinaryFormatter();
            string strVehiclesFile = Server.MapPath("~/App_Data/Vehicles.tts");

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (System.IO.File.Exists(strVehiclesFile))
            {
                using (fsVehicles = new FileStream(strVehiclesFile, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    vehicles = (List<Vehicle>)bfVehicles.Deserialize(fsVehicles);
                }

                car = vehicles.Find(engine => engine.VehicleId == id);
            }

            if (car == null)
            {
                return HttpNotFound();
            }

            return View(car);
        }

        // POST: Vehicles/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                FileStream fsVehicles = null;
                List<Vehicle> vehicles = new List<Vehicle>();
                BinaryFormatter bfVehicles = new BinaryFormatter();
                string strVehiclesFile = Server.MapPath("~/App_Data/Vehicles.tts");

                if (!string.IsNullOrEmpty("TagNumber"))
                {
                    if (System.IO.File.Exists(strVehiclesFile))
                    {
                        using (fsVehicles = new FileStream(strVehiclesFile, FileMode.Open, FileAccess.Read, FileShare.Read))
                        {
                            vehicles = (List<Vehicle>)bfVehicles.Deserialize(fsVehicles);
                        }
                    }

                    Vehicle vehicle = vehicles.Find(car => car.VehicleId == id);

                    vehicles.Remove(vehicle);

                    using (fsVehicles = new FileStream(strVehiclesFile, FileMode.Create, FileAccess.Write, FileShare.Write))
                    {
                        bfVehicles.Serialize(fsVehicles, vehicles);
                    }
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
