using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RCMMaps.Models;

namespace RCMMaps.Controllers
{
    [Authorize(Roles = "Editor")]
    public class BoardRatingsController : Controller
    {
        private RCMMapsEntities db = new RCMMapsEntities();

        // GET: /BoardRatings/
        public ActionResult Index()
        {
            return View(db.BoardRatings.ToList().OrderBy(s => s.Rating));
        }

        // GET: /BoardRatings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BoardRating boardRating = db.BoardRatings.Find(id);
            if (boardRating == null)
            {
                return HttpNotFound();
            }
            return View(boardRating);
        }

        // GET: /BoardRatings/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /BoardRatings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="ID,Rating,Description")] BoardRating boardRating)
        {
            if (ModelState.IsValid)
            {
                db.BoardRatings.Add(boardRating);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(boardRating);
        }

        // GET: /BoardRatings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BoardRating boardRating = db.BoardRatings.Find(id);
            if (boardRating == null)
            {
                return HttpNotFound();
            }
            return View(boardRating);
        }

        // POST: /BoardRatings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ID,Rating,Description")] BoardRating boardRating)
        {
            if (ModelState.IsValid)
            {
                db.Entry(boardRating).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(boardRating);
        }

        // GET: /BoardRatings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BoardRating boardRating = db.BoardRatings.Find(id);
            if (boardRating == null)
            {
                return HttpNotFound();
            }
            return View(boardRating);
        }

        // POST: /BoardRatings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BoardRating boardRating = db.BoardRatings.Find(id);
            db.BoardRatings.Remove(boardRating);
            db.SaveChanges();
            return RedirectToAction("Index");
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
