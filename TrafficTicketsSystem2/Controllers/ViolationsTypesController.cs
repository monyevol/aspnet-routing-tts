using System.IO;
using System.Net;
using System.Web.Mvc;
using System.Collections.Generic;
using TrafficTicketsSystem2.Models;
using System.Runtime.Serialization.Formatters.Binary;

namespace TrafficTicketsSystem2.Controllers
{
    public class ViolationsTypesController : Controller
    {
        // GET: ViolationsTypes
        public ActionResult Index()
        {
            FileStream fsViolationsTypes = null;
            BinaryFormatter bfViolationsTypes = new BinaryFormatter();
            List<ViolationType> categories = new List<ViolationType>();
            string strViolationsTypesFile = Server.MapPath("~/App_Data/ViolationsTypes.tts");

            if (System.IO.File.Exists(strViolationsTypesFile))
            {
                using (fsViolationsTypes = new FileStream(strViolationsTypesFile, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    categories = (List<ViolationType>)bfViolationsTypes.Deserialize(fsViolationsTypes);
                }
            }

            return View(categories);
        }

        // GET: ViolationsTypes/Details/5
        public ActionResult Details(int? id)
        {
            ViolationType category = null;
            FileStream fsViolationsTypes = null;
            BinaryFormatter bfViolationsTypes = new BinaryFormatter();
            List<ViolationType> categories = new List<ViolationType>();
            string strViolationsTypesFile = Server.MapPath("~/App_Data/ViolationsTypes.tts");

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (System.IO.File.Exists(strViolationsTypesFile))
            {
                using (fsViolationsTypes = new FileStream(strViolationsTypesFile, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    categories = (List<ViolationType>)bfViolationsTypes.Deserialize(fsViolationsTypes);
                }

                category = categories.Find(cat => cat.ViolationTypeId == id);
            }

            if (category == null)
            {
                return HttpNotFound();
            }

            return View(category);
        }

        // GET: ViolationsTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ViolationsTypes/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here
                int vTypeId = 0;
                FileStream fsViolationsTypes = null;
                BinaryFormatter bfViolationsTypes = new BinaryFormatter();
                List<ViolationType> categories = new List<ViolationType>();
                string strViolationsTypesFile = Server.MapPath("~/App_Data/ViolationsTypes.tts");

                if (!string.IsNullOrEmpty("DrvLicNumber"))
                {
                    if (System.IO.File.Exists(strViolationsTypesFile))
                    {
                        using (fsViolationsTypes = new FileStream(strViolationsTypesFile, FileMode.Open, FileAccess.Read, FileShare.Read))
                        {
                            categories = (List<ViolationType>)bfViolationsTypes.Deserialize(fsViolationsTypes);
                        }

                        foreach (ViolationType vt in categories)
                        {
                            vTypeId = vt.ViolationTypeId;
                        }
                    }

                    vTypeId++;

                    ViolationType category = new ViolationType()
                    {
                        ViolationTypeId = vTypeId,
                        ViolationName = collection["ViolationName"],
                        Description = collection["Description"],
                    };

                    categories.Add(category);

                    using (fsViolationsTypes = new FileStream(strViolationsTypesFile, FileMode.Create, FileAccess.Write, FileShare.Write))
                    {
                        bfViolationsTypes.Serialize(fsViolationsTypes, categories);
                    }
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: ViolationsTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            ViolationType category = null;
            FileStream fsViolationsTypes = null;
            BinaryFormatter bfViolationsTypes = new BinaryFormatter();
            List<ViolationType> categories = new List<ViolationType>();
            string strViolationsTypesFile = Server.MapPath("~/App_Data/ViolationsTypes.tts");

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (System.IO.File.Exists(strViolationsTypesFile))
            {
                using (fsViolationsTypes = new FileStream(strViolationsTypesFile, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    categories = (List<ViolationType>)bfViolationsTypes.Deserialize(fsViolationsTypes);
                }

                category = categories.Find(cat => cat.ViolationTypeId == id);
            }

            if (category == null)
            {
                return HttpNotFound();
            }

            return View(category);
        }

        // POST: ViolationsTypes/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
                FileStream fsViolationsTypes = null;
                BinaryFormatter bfDrivers = new BinaryFormatter();
                List<ViolationType> violationsTypes = new List<ViolationType>();
                string strViolationsTypesFile = Server.MapPath("~/App_Data/ViolationsTypes.tts");

                if (!string.IsNullOrEmpty("ViolationName"))
                {
                    if (System.IO.File.Exists(strViolationsTypesFile))
                    {
                        using (fsViolationsTypes = new FileStream(strViolationsTypesFile, FileMode.Open, FileAccess.Read, FileShare.Read))
                        {
                            violationsTypes = (List<ViolationType>)bfDrivers.Deserialize(fsViolationsTypes);
                        }
                    }

                    ViolationType vType = violationsTypes.Find(type => type.ViolationTypeId == id);

                    vType.ViolationName = collection["ViolationName"];
                    vType.Description = collection["Description"];

                    using (fsViolationsTypes = new FileStream(strViolationsTypesFile, FileMode.Create, FileAccess.Write, FileShare.Write))
                    {
                        bfDrivers.Serialize(fsViolationsTypes, violationsTypes);
                    }
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: ViolationsTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            ViolationType category = null;
            FileStream fsViolationsTypes = null;
            BinaryFormatter bfViolationsTypes = new BinaryFormatter();
            List<ViolationType> categories = new List<ViolationType>();
            string strViolationsTypesFile = Server.MapPath("~/App_Data/ViolationsTypes.tts");

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (System.IO.File.Exists(strViolationsTypesFile))
            {
                using (fsViolationsTypes = new FileStream(strViolationsTypesFile, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    categories = (List<ViolationType>)bfViolationsTypes.Deserialize(fsViolationsTypes);
                }

                category = categories.Find(cat => cat.ViolationTypeId == id);
            }

            if (category == null)
            {
                return HttpNotFound();
            }

            return View(category);
        }

        // POST: ViolationsTypes/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                FileStream fsViolationsTypes = null;
                BinaryFormatter bfViolationsTypes = new BinaryFormatter();
                List<ViolationType> violationsTypes = new List<ViolationType>();
                string strViolationsTypesFile = Server.MapPath("~/App_Data/ViolationsTypes.tts");

                if (!string.IsNullOrEmpty("ViolationName"))
                {
                    if (System.IO.File.Exists(strViolationsTypesFile))
                    {
                        using (fsViolationsTypes = new FileStream(strViolationsTypesFile, FileMode.Open, FileAccess.Read, FileShare.Read))
                        {
                            violationsTypes = (List<ViolationType>)bfViolationsTypes.Deserialize(fsViolationsTypes);
                        }
                    }

                    ViolationType violationType = violationsTypes.Find(type => type.ViolationTypeId == id);

                    bool success = violationsTypes.Remove(violationType);

                    if (success)
                    {
                        using (fsViolationsTypes = new FileStream(strViolationsTypesFile, FileMode.Create, FileAccess.Write, FileShare.Write))
                        {
                            bfViolationsTypes.Serialize(fsViolationsTypes, violationsTypes);
                        }
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
