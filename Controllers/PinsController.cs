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
    public class PinsController : Controller
    {
        private RCMMapsEntities db = new RCMMapsEntities();

        // GET: /Pins/
        public ActionResult Index()
        {
            var pins = db.Pins.Include(p => p.Map).Include(p => p.PinType);
            return View(pins.ToList());
        }

        // GET: /Pins/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pin pin = db.Pins.Find(id);
            if (pin == null)
            {
                return HttpNotFound();
            }
            return View(pin);
        }

        // GET: /Pins/Create
        public ActionResult Create(int? mapId)
        {
            // Save the MapID to the ViewBag so you can redirect back
            Map map = db.Maps.Find(mapId);
            ViewBag.MapID = map.ID;

            ViewBag.PinTypeID = new SelectList(db.PinTypes, "ID", "Title");
            return View();
        }

        // POST: /Pins/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="ID,Title,Latitude,Longitude,PinTypeID,MapID")] Pin pin)
        {
            var redirectURL = "../Maps/Edit/" + pin.MapID + "#pins";

            // If the page is reloaded with an error message from below, the MapID is lost in the querystring
            // so save the MapID in the ViewBag to use in those cases
            ViewBag.MapID = pin.MapID;


            if (ModelState.IsValid)
            {
                db.Pins.Add(pin);
                db.SaveChanges();
                return Redirect(redirectURL);
            }

            ViewBag.MapID = new SelectList(db.Maps, "ID", "Title", pin.MapID);
            ViewBag.PinTypeID = new SelectList(db.PinTypes, "ID", "Title", pin.PinTypeID);
            return Redirect(redirectURL);
        }

        // GET: /Pins/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pin pin = db.Pins.Find(id);
            if (pin == null)
            {
                return HttpNotFound();
            }
            ViewBag.MapID = new SelectList(db.Maps, "ID", "Title", pin.MapID);
            ViewBag.PinTypeID = new SelectList(db.PinTypes, "ID", "Title", pin.PinTypeID);
            return View(pin);
        }




        // POST: /Pins/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ID,Title,Latitude,Longitude,PinTypeID,MapID")] Pin pin)
        {
            var redirectURL = "../../Maps/Edit/" + pin.MapID + "#pins";

            if (ModelState.IsValid)
            {
                db.Entry(pin).State = EntityState.Modified;
                db.SaveChanges();
                return Redirect(redirectURL);
            }
            ViewBag.MapID = new SelectList(db.Maps, "ID", "Title", pin.MapID);
            ViewBag.PinTypeID = new SelectList(db.PinTypes, "ID", "Title", pin.PinTypeID);
            return Redirect(redirectURL);
        }




        // GET: /Pins/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pin pin = db.Pins.Find(id);
            if (pin == null)
            {
                return HttpNotFound();
            }
            return View(pin);
        }




        // POST: /Pins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Pin pin = db.Pins.Find(id);
            var mapId = pin.MapID;

            db.Pins.Remove(pin);
            db.SaveChanges();

            var redirectURL = "../../Maps/Edit/" + mapId + "#pins";
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
