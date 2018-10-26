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
    public class DMAsController : Controller
    {
        private RCMMapsEntities db = new RCMMapsEntities();

        // GET: /DMAs/
        public ActionResult Index()
        {
            return View(db.DMAs.ToList().OrderBy(s => s.DMA1));
        }

        // GET: /DMAs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DMA dMA = db.DMAs.Find(id);
            if (dMA == null)
            {
                return HttpNotFound();
            }
            return View(dMA);
        }

        // GET: /DMAs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /DMAs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="ID,DMA1")] DMA dMA)
        {
            if (ModelState.IsValid)
            {
                db.DMAs.Add(dMA);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(dMA);
        }

        // GET: /DMAs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DMA dMA = db.DMAs.Find(id);
            if (dMA == null)
            {
                return HttpNotFound();
            }
            return View(dMA);
        }

        // POST: /DMAs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ID,DMA1")] DMA dMA)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dMA).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(dMA);
        }

        // GET: /DMAs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DMA dMA = db.DMAs.Find(id);
            if (dMA == null)
            {
                return HttpNotFound();
            }
            return View(dMA);
        }

        // POST: /DMAs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DMA dMA = db.DMAs.Find(id);
            db.DMAs.Remove(dMA);
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
