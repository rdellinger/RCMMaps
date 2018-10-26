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
    public class ProductionVendorsController : Controller
    {
        private RCMMapsEntities db = new RCMMapsEntities();

        // GET: /ProductionVendors/
        public ActionResult Index()
        {
            return View(db.ProductionVendors.ToList());
        }

        // GET: /ProductionVendors/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductionVendor productionVendor = db.ProductionVendors.Find(id);
            if (productionVendor == null)
            {
                return HttpNotFound();
            }
            return View(productionVendor);
        }

        // GET: /ProductionVendors/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /ProductionVendors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="ID,Vendor")] ProductionVendor productionVendor)
        {
            if (ModelState.IsValid)
            {
                db.ProductionVendors.Add(productionVendor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(productionVendor);
        }

        // GET: /ProductionVendors/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductionVendor productionVendor = db.ProductionVendors.Find(id);
            if (productionVendor == null)
            {
                return HttpNotFound();
            }
            return View(productionVendor);
        }

        // POST: /ProductionVendors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ID,Vendor")] ProductionVendor productionVendor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(productionVendor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(productionVendor);
        }

        // GET: /ProductionVendors/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductionVendor productionVendor = db.ProductionVendors.Find(id);
            if (productionVendor == null)
            {
                return HttpNotFound();
            }
            return View(productionVendor);
        }

        // POST: /ProductionVendors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProductionVendor productionVendor = db.ProductionVendors.Find(id);
            db.ProductionVendors.Remove(productionVendor);
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
