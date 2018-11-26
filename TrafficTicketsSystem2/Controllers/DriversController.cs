using System.IO;
using System.Net;
using System.Web.Mvc;
using System.Collections.Generic;
using TrafficTicketsSystem2.Models;
using System.Runtime.Serialization.Formatters.Binary;

namespace TrafficTicketsSystem2.Controllers
{
    public class DriversController : Controller
    {
        // GET: Drivers
        public ActionResult Index()
        {
            FileStream fsDrivers = null;
            List<Driver> drivers = new List<Driver>();
            BinaryFormatter bfDrivers = new BinaryFormatter();
            string strDriversFile = Server.MapPath("~/App_Data/Drivers.tts");

            if (System.IO.File.Exists(strDriversFile))
            {
                using (fsDrivers = new FileStream(strDriversFile, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    drivers = (List<Driver>)bfDrivers.Deserialize(fsDrivers);
                }
            }

            return View(drivers);
        }

        // GET: Drivers/Details/5
        public ActionResult Details(int? id)
        {
            Driver driver = null;
            FileStream fsDrivers = null;
            List<Driver> drivers = new List<Driver>();
            BinaryFormatter bfDrivers = new BinaryFormatter();
            string strDriversFile = Server.MapPath("~/App_Data/Drivers.tts");

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (System.IO.File.Exists(strDriversFile))
            {
                using (fsDrivers = new FileStream(strDriversFile, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    drivers = (List<Driver>)bfDrivers.Deserialize(fsDrivers);
                }

                driver = drivers.Find(dvr => dvr.DriverId == id);
            }

            if (driver == null)
            {
                return HttpNotFound();
            }

            return View(driver);
        }

        // GET: Drivers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Drivers/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here
                int driverId = 0;
                FileStream fsDrivers = null;
                List<Driver> drivers = new List<Driver>();
                BinaryFormatter bfDrivers = new BinaryFormatter();
                string strDriversFile = Server.MapPath("~/App_Data/Drivers.tts");

                if ((!string.IsNullOrEmpty("DrvLicNumber")) && (!string.IsNullOrEmpty("State")))
                {
                    if (System.IO.File.Exists(strDriversFile))
                    {
                        using (fsDrivers = new FileStream(strDriversFile, FileMode.Open, FileAccess.Read, FileShare.Read))
                        {
                            drivers = (List<Driver>)bfDrivers.Deserialize(fsDrivers);
                        }

                        foreach(Driver drv in drivers)
                        {
                            driverId = drv.DriverId;
                        }
                    }

                    driverId++;

                    Driver person = new Driver()
                    {
                        DriverId = driverId,
                        DrvLicNumber = collection["DrvLicNumber"],
                        FirstName = collection["FirstName"],
                        LastName = collection["LastName"],
                        Address = collection["Address"],
                        City = collection["City"],
                        County = collection["County"],
                        State = collection["State"],
                        ZIPCode = collection["ZIPCode"],
                    };

                    drivers.Add(person);

                    using (fsDrivers = new FileStream(strDriversFile, FileMode.Create, FileAccess.Write, FileShare.Write))
                    {
                        bfDrivers.Serialize(fsDrivers, drivers);
                    }
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Drivers/Edit/5
        public ActionResult Edit(int? id)
        {
            Driver driver = null;
            FileStream fsDrivers = null;
            List<Driver> drivers = new List<Driver>();
            BinaryFormatter bfDrivers = new BinaryFormatter();
            string strDriversFile = Server.MapPath("~/App_Data/Drivers.tts");

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (System.IO.File.Exists(strDriversFile))
            {
                using (fsDrivers = new FileStream(strDriversFile, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    drivers = (List<Driver>)bfDrivers.Deserialize(fsDrivers);
                }

                driver = drivers.Find(dvr => dvr.DriverId == id);
            }

            if (driver == null)
            {
                return HttpNotFound();
            }

            return View(driver);
        }

        // POST: Drivers/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
                FileStream fsDrivers = null;
                List<Driver> drivers = new List<Driver>();
                BinaryFormatter bfDrivers = new BinaryFormatter();
                string strDriversFile = Server.MapPath("~/App_Data/Drivers.tts");

                if (!string.IsNullOrEmpty("DrvLicNumber"))
                {
                    if (System.IO.File.Exists(strDriversFile))
                    {
                        using (fsDrivers = new FileStream(strDriversFile, FileMode.Open, FileAccess.Read, FileShare.Read))
                        {
                            drivers = (List<Driver>)bfDrivers.Deserialize(fsDrivers);
                        }
                    }

                    Driver driver = drivers.Find(dvr => dvr.DriverId == id);

                    driver.FirstName = collection["FirstName"];
                    driver.LastName = collection["LastName"];
                    driver.Address = collection["Address"];
                    driver.City = collection["City"];
                    driver.County = collection["County"];
                    driver.State = collection["State"];
                    driver.ZIPCode = collection["ZIPCode"];

                    using (fsDrivers = new FileStream(strDriversFile, FileMode.Create, FileAccess.Write, FileShare.Write))
                    {
                        bfDrivers.Serialize(fsDrivers, drivers);
                    }
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Drivers/Delete/5
        public ActionResult Delete(int? id)
        {
            Driver driver = null;
            FileStream fsDrivers = null;
            List<Driver> drivers = new List<Driver>();
            BinaryFormatter bfDrivers = new BinaryFormatter();
            string strDriversFile = Server.MapPath("~/App_Data/Drivers.tts");

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (System.IO.File.Exists(strDriversFile))
            {
                using (fsDrivers = new FileStream(strDriversFile, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    drivers = (List<Driver>)bfDrivers.Deserialize(fsDrivers);
                }

                driver = drivers.Find(dvr => dvr.DriverId == id);
            }

            if (driver == null)
            {
                return HttpNotFound();
            }

            return View(driver);
        }

        // POST: Drivers/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                FileStream fsDrivers = null;
                List<Driver> drivers = new List<Driver>();
                BinaryFormatter bfDrivers = new BinaryFormatter();
                string strDriversFile = Server.MapPath("~/App_Data/Drivers.tts");

                if (!string.IsNullOrEmpty("DrvLicNumber"))
                {
                    if (System.IO.File.Exists(strDriversFile))
                    {
                        using (fsDrivers = new FileStream(strDriversFile, FileMode.Open, FileAccess.Read, FileShare.Read))
                        {
                            drivers = (List<Driver>)bfDrivers.Deserialize(fsDrivers);
                        }
                    }

                    Driver driver = drivers.Find(dvr => dvr.DriverId == id);

                    drivers.Remove(driver);

                    using (fsDrivers = new FileStream(strDriversFile, FileMode.Create, FileAccess.Write, FileShare.Write))
                    {
                        bfDrivers.Serialize(fsDrivers, drivers);
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
