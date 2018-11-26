using System.IO;
using System.Net;
using System.Web.Mvc;
using System.Collections.Generic;
using TrafficTicketsSystem2.Models;
using System.Runtime.Serialization.Formatters.Binary;

namespace TrafficTicketsSystem2.Controllers
{
    public class CamerasController : Controller
    {
        // GET: Cameras
        public ActionResult Index()
        {
            FileStream fsCameras = null;
            List<Camera> cameras = new List<Camera>();
            BinaryFormatter bfCameras = new BinaryFormatter();
            string strCamerasFile = Server.MapPath("~/App_Data/Cameras.tts");

            // If a file for Cameras records was proviously created, ...
            if (System.IO.File.Exists(strCamerasFile))
            {
                // ... open it
                using (fsCameras = new FileStream(strCamerasFile, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    /* After opening the file, get the list of cameras from it
                     * and store in the declared List<> variable. */
                    cameras = (List<Camera>)bfCameras.Deserialize(fsCameras);
                }
            }

            // Return the list of cameras so they can be accessed in the view
            return View(cameras);
        }

        // GET: Cameras/Details/5
        public ActionResult Details(int? id)
        {
            Camera viewer = null;
            FileStream fsCameras = null;
            List<Camera> cameras = new List<Camera>();
            BinaryFormatter bfCameras = new BinaryFormatter();
            string strCamerasFile = Server.MapPath("~/App_Data/Cameras.tts");

            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (System.IO.File.Exists(strCamerasFile))
            {
                using (fsCameras = new FileStream(strCamerasFile, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    cameras = (List<Camera>)bfCameras.Deserialize(fsCameras);
                }

                viewer = cameras.Find(cmr => cmr.CameraId == id);
            }

            if (viewer == null)
            {
                return HttpNotFound();
            }

            return View(viewer);
        }

        // GET: Cameras/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Cameras/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here
                int cmrId = 0;
                FileStream fsCameras = null;
                List<Camera> cameras = new List<Camera>();
                BinaryFormatter bfCameras = new BinaryFormatter();
                string strCamerasFile = Server.MapPath("~/App_Data/Cameras.tts");

                if (!string.IsNullOrEmpty("CameraNumber"))
                {
                    if (System.IO.File.Exists(strCamerasFile))
                    {
                        using (fsCameras = new FileStream(strCamerasFile, FileMode.Open, FileAccess.Read, FileShare.Read))
                        {
                            cameras = (List<Camera>)bfCameras.Deserialize(fsCameras);

                            foreach (Camera cmr in cameras)
                            {
                                cmrId = cmr.CameraId;
                            }
                        }
                    }

                    cmrId++;

                    Camera viewer = new Camera()
                    {
                        CameraId = cmrId,
                        CameraNumber = collection["CameraNumber"],
                        Make = collection["Make"],
                        Model = collection["Model"],
                        Location = collection["Location"]
                    };

                    cameras.Add(viewer);

                    using (fsCameras = new FileStream(strCamerasFile, FileMode.Create, FileAccess.Write, FileShare.Write))
                    {
                        bfCameras.Serialize(fsCameras, cameras);
                    }
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Cameras/Edit/5
        public ActionResult Edit(int? id)
        {
            Camera viewer = null;
            FileStream fsCameras = null;
            List<Camera> cameras = new List<Camera>();
            BinaryFormatter bfCameras = new BinaryFormatter();
            string strCamerasFile = Server.MapPath("~/App_Data/Cameras.tts");

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (System.IO.File.Exists(strCamerasFile))
            {
                using (fsCameras = new FileStream(strCamerasFile, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    cameras = (List<Camera>)bfCameras.Deserialize(fsCameras);
                }

                viewer = cameras.Find(cmr => cmr.CameraId == id);
            }

            if (viewer == null)
            {
                return HttpNotFound();
            }

            return View(viewer);
        }

        // POST: Cameras/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
                FileStream fsCameras = null;
                List<Camera> cameras = new List<Camera>();
                BinaryFormatter bfCameras = new BinaryFormatter();
                string strCamerasFile = Server.MapPath("~/App_Data/Cameras.tts");

                if (!string.IsNullOrEmpty("CameraNumber"))
                {
                    if (System.IO.File.Exists(strCamerasFile))
                    {
                        using (fsCameras = new FileStream(strCamerasFile, FileMode.Open, FileAccess.Read, FileShare.Read))
                        {
                            cameras = (List<Camera>)bfCameras.Deserialize(fsCameras);
                        }

                        Camera camera = cameras.Find(cmr => cmr.CameraId == id);

                        camera.Make = collection["Make"];
                        camera.Model = collection["Model"];
                        camera.Location = collection["Location"];

                        using (fsCameras = new FileStream(strCamerasFile, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite))
                        {
                            bfCameras.Serialize(fsCameras, cameras);
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

        // GET: Cameras/Delete/5
        public ActionResult Delete(int? id)
        {
            Camera viewer = null;
            FileStream fsCameras = null;
            List<Camera> cameras = new List<Camera>();
            BinaryFormatter bfCameras = new BinaryFormatter();
            string strCamerasFile = Server.MapPath("~/App_Data/Cameras.tts");

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (System.IO.File.Exists(strCamerasFile))
            {
                using (fsCameras = new FileStream(strCamerasFile, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    cameras = (List<Camera>)bfCameras.Deserialize(fsCameras);
                }

                viewer = cameras.Find(cmr => cmr.CameraId == id);
            }

            if (viewer == null)
            {
                return HttpNotFound();
            }

            return View(viewer);
        }

        // POST: Cameras/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                FileStream fsCameras = null;
                List<Camera> cameras = new List<Camera>();
                BinaryFormatter bfCameras = new BinaryFormatter();
                string strCamerasFile = Server.MapPath("~/App_Data/Cameras.tts");

                if (!string.IsNullOrEmpty("CameraNumber"))
                {
                    if (System.IO.File.Exists(strCamerasFile))
                    {
                        using (fsCameras = new FileStream(strCamerasFile, FileMode.Open, FileAccess.Read, FileShare.Read))
                        {
                            cameras = (List<Camera>)bfCameras.Deserialize(fsCameras);
                        }

                        Camera camera = cameras.Find(cmr => cmr.CameraId == id);

                        cameras.Remove(camera);

                        using (fsCameras = new FileStream(strCamerasFile, FileMode.Create, FileAccess.Write, FileShare.Write))
                        {
                            bfCameras.Serialize(fsCameras, cameras);
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
