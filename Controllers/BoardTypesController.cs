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
    public class BoardTypesController : Controller
    {
        private RCMMapsEntities db = new RCMMapsEntities();

        // GET: /BoardTypes/
        public ActionResult Index()
        {
            return View(db.BoardTypes.ToList().OrderBy(s => s.BoardType1));
        }

        // GET: /BoardTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BoardType boardType = db.BoardTypes.Find(id);
            if (boardType == null)
            {
                return HttpNotFound();
            }
            return View(boardType);
        }

        // GET: /BoardTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /BoardTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="ID,BoardType1")] BoardType boardType)
        {
            if (ModelState.IsValid)
            {
                db.BoardTypes.Add(boardType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(boardType);
        }

        // GET: /BoardTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BoardType boardType = db.BoardTypes.Find(id);
            if (boardType == null)
            {
                return HttpNotFound();
            }
            return View(boardType);
        }

        // POST: /BoardTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ID,BoardType1")] BoardType boardType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(boardType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(boardType);
        }

        // GET: /BoardTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BoardType boardType = db.BoardTypes.Find(id);
            if (boardType == null)
            {
                return HttpNotFound();
            }
            return View(boardType);
        }

        // POST: /BoardTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BoardType boardType = db.BoardTypes.Find(id);
            db.BoardTypes.Remove(boardType);
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
