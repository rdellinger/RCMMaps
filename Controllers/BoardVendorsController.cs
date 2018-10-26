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
    public class BoardVendorsController : Controller
    {
        private RCMMapsEntities db = new RCMMapsEntities();

        // GET: /BoardVendors/
        public ActionResult Index()
        {
            return View(db.BoardVendors.ToList().OrderBy(s => s.Vendor));
        }

        // GET: /BoardVendors/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BoardVendor boardVendor = db.BoardVendors.Find(id);
            if (boardVendor == null)
            {
                return HttpNotFound();
            }
            return View(boardVendor);
        }

        // GET: /BoardVendors/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /BoardVendors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="ID,Vendor")] BoardVendor boardVendor)
        {
            if (ModelState.IsValid)
            {
                db.BoardVendors.Add(boardVendor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(boardVendor);
        }

        // GET: /BoardVendors/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BoardVendor boardVendor = db.BoardVendors.Find(id);
            if (boardVendor == null)
            {
                return HttpNotFound();
            }
            return View(boardVendor);
        }

        // POST: /BoardVendors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ID,Vendor")] BoardVendor boardVendor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(boardVendor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(boardVendor);
        }

        // GET: /BoardVendors/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BoardVendor boardVendor = db.BoardVendors.Find(id);
            if (boardVendor == null)
            {
                return HttpNotFound();
            }
            return View(boardVendor);
        }

        // POST: /BoardVendors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BoardVendor boardVendor = db.BoardVendors.Find(id);
            db.BoardVendors.Remove(boardVendor);
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
