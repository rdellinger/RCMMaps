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
    public class MapStyleDefaultsController : Controller
    {
        private RCMMapsEntities db = new RCMMapsEntities();

        // GET: /MapStyleDefaults/
        public ActionResult Index()
        {
            var mapstyledefaults = db.MapStyleDefaults.Include(m => m.FeatureType);
            return View(mapstyledefaults.ToList().OrderBy(s => s.FeatureType.FeatureType1));
        }

        // GET: /MapStyleDefaults/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MapStyleDefault mapStyleDefault = db.MapStyleDefaults.Find(id);
            if (mapStyleDefault == null)
            {
                return HttpNotFound();
            }
            return View(mapStyleDefault);
        }

        // GET: /MapStyleDefaults/Create
        public ActionResult Create()
        {
            ViewBag.FeatureTypeID = new SelectList(db.FeatureTypes, "ID", "FeatureType1");
            return View();
        }

        // POST: /MapStyleDefaults/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="ID,FeatureTypeID,Hue,Saturation,Lightness,Gamma")] MapStyleDefault mapStyleDefault)
        {
            if (ModelState.IsValid)
            {
                db.MapStyleDefaults.Add(mapStyleDefault);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FeatureTypeID = new SelectList(db.FeatureTypes, "ID", "FeatureType1", mapStyleDefault.FeatureTypeID);
            return View(mapStyleDefault);
        }

        // GET: /MapStyleDefaults/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MapStyleDefault mapStyleDefault = db.MapStyleDefaults.Find(id);
            if (mapStyleDefault == null)
            {
                return HttpNotFound();
            }
            ViewBag.FeatureTypeID = new SelectList(db.FeatureTypes, "ID", "FeatureType1", mapStyleDefault.FeatureTypeID);
            return View(mapStyleDefault);
        }

        // POST: /MapStyleDefaults/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ID,FeatureTypeID,Hue,Saturation,Lightness,Gamma")] MapStyleDefault mapStyleDefault)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mapStyleDefault).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FeatureTypeID = new SelectList(db.FeatureTypes, "ID", "FeatureType1", mapStyleDefault.FeatureTypeID);
            return View(mapStyleDefault);
        }

        // GET: /MapStyleDefaults/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MapStyleDefault mapStyleDefault = db.MapStyleDefaults.Find(id);
            if (mapStyleDefault == null)
            {
                return HttpNotFound();
            }
            return View(mapStyleDefault);
        }

        // POST: /MapStyleDefaults/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MapStyleDefault mapStyleDefault = db.MapStyleDefaults.Find(id);
            db.MapStyleDefaults.Remove(mapStyleDefault);
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
