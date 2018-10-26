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
    public class FeatureTypesController : Controller
    {
        private RCMMapsEntities db = new RCMMapsEntities();

        // GET: /FeatureTypes/
        public ActionResult Index()
        {
            return View(db.FeatureTypes.ToList().OrderBy(s => s.FeatureType1));
        }

        // GET: /FeatureTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FeatureType featureType = db.FeatureTypes.Find(id);
            if (featureType == null)
            {
                return HttpNotFound();
            }
            return View(featureType);
        }

        // GET: /FeatureTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /FeatureTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="ID,FeatureType1,Code")] FeatureType featureType)
        {
            if (ModelState.IsValid)
            {
                db.FeatureTypes.Add(featureType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(featureType);
        }

        // GET: /FeatureTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FeatureType featureType = db.FeatureTypes.Find(id);
            if (featureType == null)
            {
                return HttpNotFound();
            }
            return View(featureType);
        }

        // POST: /FeatureTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ID,FeatureType1,Code")] FeatureType featureType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(featureType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(featureType);
        }

        // GET: /FeatureTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FeatureType featureType = db.FeatureTypes.Find(id);
            if (featureType == null)
            {
                return HttpNotFound();
            }
            return View(featureType);
        }

        // POST: /FeatureTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FeatureType featureType = db.FeatureTypes.Find(id);
            db.FeatureTypes.Remove(featureType);
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
