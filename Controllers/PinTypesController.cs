using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.IO;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RCMMaps.Models;

namespace RCMMaps.Controllers
{
    [Authorize(Roles = "Editor")]
    public class PinTypesController : Controller
    {
        private RCMMapsEntities db = new RCMMapsEntities();

        // GET: /PinTypes/
        public ActionResult Index()
        {
            return View(db.PinTypes.ToList().OrderBy(s => s.Title));
        }

        // GET: /PinTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PinType pinType = db.PinTypes.Find(id);
            if (pinType == null)
            {
                return HttpNotFound();
            }
            return View(pinType);
        }

        // GET: /PinTypes/Create
        public ActionResult Create()
        {
            return View();
        }





        // POST: /PinTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Title,Image")] PinType pinType, HttpPostedFileBase file)
        {
            var redirectURL = "../PinTypes";

            // if file's content length is zero or no files submitted
            if (Request.Files.Count != 1 || Request.Files[0].ContentLength == 0)
            {
                ModelState.AddModelError("uploadError", "Please select an image to upload.");
                return View(pinType);
            }

            // check the file size (max 4 Mb)
            if (Request.Files[0].ContentLength > 1024 * 1024 * 4)
            {
                ModelState.AddModelError("uploadError", "File size can't exceed 4 MB");
                return View(pinType);
            }

            // check file extension
            string extension = Path.GetExtension(Request.Files[0].FileName).ToLower();

            if (extension != ".jpg" && extension != ".jpeg" && extension != ".gif" && extension != ".png")
            {
                ModelState.AddModelError("uploadError", "Supported file extensions: jpg, jpeg, gif, png");
                return View(pinType);
            }

            // extract only the filename
            var fileName = Path.GetFileName(file.FileName);

            // store the file inside ~/Images folder
            var path = Path.Combine(Server.MapPath("~/Images/Icons/"), fileName);

            try
            {
                if (!System.IO.File.Exists(path))
                {
                    Request.Files[0].SaveAs(path);
                    //System.IO.File.Delete(path);
                }
                else
                {
                    ModelState.AddModelError("uploadError", "An image with that name already exists.  Please rename your image.");
                    return View(pinType);
                }

            }
            catch (Exception)
            {
                ModelState.AddModelError("uploadError", "Can't save file to disk");
            }


            if (ModelState.IsValid)
            {
                // Add the PinType Image
                pinType.Image = file.FileName;

                db.PinTypes.Add(pinType);
                db.SaveChanges();

                return Redirect(redirectURL);
            }

            return Redirect(redirectURL);
        }






        // GET: /PinTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PinType pinType = db.PinTypes.Find(id);
            if (pinType == null)
            {
                return HttpNotFound();
            }
            return View(pinType);
        }




        // POST: /PinTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Title,Image")] PinType pinType, HttpPostedFileBase file)
        {
            var redirectURL = "../../PinTypes";

            // If a new image is being uploaded, run validation and upload it
            if (file != null)
            {
                // if file's content length is zero or no files submitted and there is no existing photo
                if (Request.Files.Count != 1 || Request.Files[0].ContentLength == 0)
                {
                    ModelState.AddModelError("uploadError", "Please select an image to upload.");
                    return View(pinType);
                }

                // check the file size (max 4 Mb)
                if (Request.Files[0].ContentLength > 1024 * 1024 * 4)
                {
                    ModelState.AddModelError("uploadError", "File size can't exceed 4 MB");
                    return View(pinType);
                }

                // check file extension
                string extension = Path.GetExtension(Request.Files[0].FileName).ToLower();

                if (extension != ".jpg" && extension != ".jpeg" && extension != ".gif" && extension != ".png")
                {
                    ModelState.AddModelError("uploadError", "Supported file extensions: jpg, jpeg, gif, png");
                    return View(pinType);
                }

                // extract only the filename
                var fileName = Path.GetFileName(file.FileName);

                // store the file inside ~/Images/Boards folder
                var path = Path.Combine(Server.MapPath("~/Images/Icons"), fileName);
                var oldImagePath = Path.Combine(Server.MapPath("~/Images/Icons"), pinType.Image);

                try
                {
                    if (!System.IO.File.Exists(path))
                    {
                        // Save the new image
                        Request.Files[0].SaveAs(path);

                        // Delete the old image
                        System.IO.File.Delete(oldImagePath);
                    }
                    else
                    {
                        ModelState.AddModelError("uploadError", "An image with that name already exists.  Please rename your image.");
                        return View(pinType);
                    }

                }
                catch (Exception)
                {
                    ModelState.AddModelError("uploadError", "Can't save file to disk");
                }
            }



            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    pinType.Image = file.FileName;
                }
                else
                {
                    pinType.Image = pinType.Image;
                }

                db.Entry(pinType).State = EntityState.Modified;
                db.SaveChanges();

                return Redirect(redirectURL);
            }

            return Redirect(redirectURL);
        }




        // GET: /PinTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PinType pinType = db.PinTypes.Find(id);
            if (pinType == null)
            {
                return HttpNotFound();
            }
            return View(pinType);
        }




        // POST: /PinTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PinType pinType = db.PinTypes.Find(id);
            var pinTypeId = pinType.ID;

            // First, find all the Map_Boards that have this pin
            // and reassign them to have the default pin type
            var mapBoards = from a in db.Map_Boards
                       where a.PinTypeID == id
                       select a;

            foreach (Map_Boards thisMapBoard in mapBoards)
            {
                thisMapBoard.PinTypeID = 1; // the default pin type
                db.Entry(thisMapBoard).State = EntityState.Modified;
            }
            

            // get the filename
            var fileName = Path.GetFileName(pinType.Image);

            // get the path to the file
            var path = Path.Combine(Server.MapPath("~/Images/Icons"), fileName);

            // delete the file
            try
            {
                if (System.IO.File.Exists(path))
                    System.IO.File.Delete(path);
            }
            catch (Exception)
            {
                ModelState.AddModelError("uploadError", "Can't delete the file.");
            }


            db.PinTypes.Remove(pinType);
            db.SaveChanges();

            var redirectURL = "../../PinTypes";
            return Redirect(redirectURL);
        }




        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
