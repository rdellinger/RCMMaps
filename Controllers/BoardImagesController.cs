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
    public class BoardImagesController : Controller
    {
        private RCMMapsEntities db = new RCMMapsEntities();

        // GET: /BoardImages/
        public ActionResult Index()
        {
            var boardimages = db.BoardImages.Include(b => b.Board);
            return View(boardimages.ToList());
        }

        // GET: /BoardImages/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BoardImage boardImage = db.BoardImages.Find(id);
            if (boardImage == null)
            {
                return HttpNotFound();
            }
            return View(boardImage);
        }



        // GET: /BoardImages/Create
        public ActionResult Create(int? boardId)
        {
            // Save the BoardID to the ViewBag so you can redirect back
            Board board = db.Boards.Find(boardId);
            ViewBag.BoardID = board.ID;

            return View();
        }



        // POST: /BoardImages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,BoardID,Title,Image,ImageDate,DisplayOrder")] BoardImage boardImage, HttpPostedFileBase file)
        {
            var redirectURL = "../Boards/Edit/" + boardImage.BoardID + "#images";

            // If the page is reloaded with an error message from below, the BoardID is lost in the querystring
            // so save the BoardID in the ViewBag to use in those cases
            ViewBag.BoardID = boardImage.BoardID;

            // if file's content length is zero or no files submitted
            if (Request.Files.Count != 1 || Request.Files[0].ContentLength == 0)
            {
                ModelState.AddModelError("uploadError", "Please select an image to upload.");
                return View(boardImage);
            }

            // check the file size (max 4 Mb)
            if (Request.Files[0].ContentLength > 1024 * 1024 * 4)
            {
                ModelState.AddModelError("uploadError", "File size can't exceed 4 MB");
                return View(boardImage);
            }

            // check file extension
            string extension = Path.GetExtension(Request.Files[0].FileName).ToLower();

            if (extension != ".jpg" && extension != ".jpeg" && extension != ".gif" && extension != ".png")
            {
                ModelState.AddModelError("uploadError", "Supported file extensions: jpg, jpeg, gif, png");
                return View(boardImage);
            }

            // extract only the filename
            var fileName = Path.GetFileName(file.FileName);

            // store the file inside ~/Images folder
            var path = Path.Combine(Server.MapPath("~/Images/Boards/"), fileName);

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
                    return View(boardImage);
                }

            }
            catch (Exception)
            {
                ModelState.AddModelError("uploadError", "Can't save file to disk");
            }


            if (ModelState.IsValid)
            {
                // Add the Board Image
                boardImage.Image = file.FileName;

                db.BoardImages.Add(boardImage);

                // Also update the Board modified info
                Board board = db.Boards.Find(boardImage.BoardID);
                board.ModifiedBy = User.Identity.Name;
                board.DateModified = DateTime.Now;

                db.Entry(board).State = EntityState.Modified;

                db.SaveChanges();

                return Redirect(redirectURL);
            }

            return Redirect(redirectURL);
        }



        // GET: /BoardImages/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BoardImage boardImage = db.BoardImages.Find(id);
            if (boardImage == null)
            {
                return HttpNotFound();
            }
            
            ViewBag.BoardID = boardImage.BoardID;

            return View(boardImage);
        }



        // POST: /BoardImages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,BoardID,Title,Image,ImageDate,DisplayOrder")] BoardImage boardImage, HttpPostedFileBase file)
        {
            var redirectURL = "../../Boards/Edit/" + boardImage.BoardID + "#images";

            // If the page is reloaded with an error message from below, the BoardID is lost in the querystring
            // so save the BoardID in the ViewBag to use in those cases
            ViewBag.BoardID = boardImage.BoardID;

            // If a new image is being uploaded, run validation and upload it
            if (file != null)
            {
                // if file's content length is zero or no files submitted and there is no existing photo
                if (Request.Files.Count != 1 || Request.Files[0].ContentLength == 0)
                {
                    ModelState.AddModelError("uploadError", "Please select an image to upload.");
                    return View(boardImage);
                }

                // check the file size (max 4 Mb)
                if (Request.Files[0].ContentLength > 1024 * 1024 * 4)
                {
                    ModelState.AddModelError("uploadError", "File size can't exceed 4 MB");
                    return View(boardImage);
                }

                // check file extension
                string extension = Path.GetExtension(Request.Files[0].FileName).ToLower();

                if (extension != ".jpg" && extension != ".jpeg" && extension != ".gif" && extension != ".png")
                {
                    ModelState.AddModelError("uploadError", "Supported file extensions: jpg, jpeg, gif, png");
                    return View(boardImage);
                }

                // extract only the filename
                var fileName = Path.GetFileName(file.FileName);

                // store the file inside ~/Images/Boards folder
                var path = Path.Combine(Server.MapPath("~/Images/Boards"), fileName);
                var oldImagePath = Path.Combine(Server.MapPath("~/Images/Boards"), boardImage.Image);

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
                        return View(boardImage);
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
                    boardImage.Image = file.FileName;
                }
                else
                {
                    boardImage.Image = boardImage.Image;
                }

                db.Entry(boardImage).State = EntityState.Modified;

                // Also update the Board modified info
                Board board = db.Boards.Find(boardImage.BoardID);
                board.ModifiedBy = User.Identity.Name;
                board.DateModified = DateTime.Now;

                db.Entry(board).State = EntityState.Modified;

                db.SaveChanges();

                return Redirect(redirectURL);
            }

            return Redirect(redirectURL);
        }




        // GET: /BoardImages/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BoardImage boardImage = db.BoardImages.Find(id);
            if (boardImage == null)
            {
                return HttpNotFound();
            }
            return View(boardImage);
        }




        // POST: /BoardImages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BoardImage boardImage = db.BoardImages.Find(id);
            var boardId = boardImage.BoardID;

            // get the filename
            var fileName = Path.GetFileName(boardImage.Image);

            // get the path to the file
            var path = Path.Combine(Server.MapPath("~/Images/Boards"), fileName);

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


            db.BoardImages.Remove(boardImage);

            // Also update the Board modified info
            Board board = db.Boards.Find(boardId);
            board.ModifiedBy = User.Identity.Name;
            board.DateModified = DateTime.Now;

            db.Entry(board).State = EntityState.Modified;

            db.SaveChanges();

            var redirectURL = "../../Boards/Edit/" + boardId + "#images";
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
