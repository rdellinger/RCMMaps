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
    public class MapDefaultsController : Controller
    {
        private RCMMapsEntities db = new RCMMapsEntities();

        // GET: /MapDefaults/
        public ActionResult Index()
        {
            return View(db.MapDefaults.ToList());
        }

        // GET: /MapDefaults/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MapDefault mapDefault = db.MapDefaults.Find(id);
            if (mapDefault == null)
            {
                return HttpNotFound();
            }
            return View(mapDefault);
        }

        // GET: /MapDefaults/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /MapDefaults/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="ID,Width,Height,CenterOnLat,CenterOnLong,ZoomLevel,DisplayZoomControl,DisplayMapTypeControl,DisplayScaleControl,DisplayStreetViewControl,DisplayPanControl")] MapDefault mapDefault)
        {
            if (ModelState.IsValid)
            {
                db.MapDefaults.Add(mapDefault);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(mapDefault);
        }

        // GET: /MapDefaults/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MapDefault mapDefault = db.MapDefaults.Find(id);
            if (mapDefault == null)
            {
                return HttpNotFound();
            }
            return View(mapDefault);
        }

        // POST: /MapDefaults/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ID,Width,Height,CenterOnLat,CenterOnLong,ZoomLevel,DisplayZoomControl,DisplayMapTypeControl,DisplayScaleControl,DisplayStreetViewControl,DisplayPanControl")] MapDefault mapDefault)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mapDefault).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index","Settings");
            }
            return View(mapDefault);
        }

        // GET: /MapDefaults/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MapDefault mapDefault = db.MapDefaults.Find(id);
            if (mapDefault == null)
            {
                return HttpNotFound();
            }
            return View(mapDefault);
        }

        // POST: /MapDefaults/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MapDefault mapDefault = db.MapDefaults.Find(id);
            db.MapDefaults.Remove(mapDefault);
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
