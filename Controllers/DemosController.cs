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
    public class DemosController : Controller
    {
        private RCMMapsEntities db = new RCMMapsEntities();

        // GET: /Demos/
        public ActionResult Index()
        {
            var demos = db.Demos.Include(d => d.Board);
            return View(demos.ToList());
        }

        // GET: /Demos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Demo demo = db.Demos.Find(id);
            if (demo == null)
            {
                return HttpNotFound();
            }
            return View(demo);
        }

        // GET: /Demos/Create
        public ActionResult Create()
        {
            ViewBag.BoardID = new SelectList(db.Boards, "ID", "BoardNumber");
            return View();
        }

        // POST: /Demos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="ID,BoardID,Demo1,DemoWeeklyImpressions,DateUpdated,Source")] Demo demo)
        {
            if (ModelState.IsValid)
            {
                db.Demos.Add(demo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BoardID = new SelectList(db.Boards, "ID", "BoardNumber", demo.BoardID);
            return View(demo);
        }

        // GET: /Demos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Demo demo = db.Demos.Find(id);
            if (demo == null)
            {
                return HttpNotFound();
            }
            ViewBag.BoardID = new SelectList(db.Boards, "ID", "BoardNumber", demo.BoardID);
            return View(demo);
        }

        // POST: /Demos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ID,BoardID,Demo1,DemoWeeklyImpressions,DateUpdated,Source")] Demo demo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(demo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BoardID = new SelectList(db.Boards, "ID", "BoardNumber", demo.BoardID);
            return View(demo);
        }

        // GET: /Demos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Demo demo = db.Demos.Find(id);
            if (demo == null)
            {
                return HttpNotFound();
            }
            return View(demo);
        }

        // POST: /Demos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Demo demo = db.Demos.Find(id);
            db.Demos.Remove(demo);
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
