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
    public class MapStylesController : Controller
    {
        private RCMMapsEntities db = new RCMMapsEntities();

        // GET: /MapStyles/
        public ActionResult Index()
        {
            var mapstyles = db.MapStyles.Include(m => m.Map).Include(m => m.FeatureType);
            return View(mapstyles.ToList());
        }

        // GET: /MapStyles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MapStyle mapStyle = db.MapStyles.Find(id);
            if (mapStyle == null)
            {
                return HttpNotFound();
            }
            return View(mapStyle);
        }

        // GET: /MapStyles/Create
        public ActionResult Create(int? mapId)
        {
            // Save the MapID to the ViewBag so you can redirect back
            Map map = db.Maps.Find(mapId);
            ViewBag.MapID = map.ID;

            ViewBag.FeatureTypeID = new SelectList(db.FeatureTypes, "ID", "FeatureType1").OrderBy(s => s.Text);

            return View();
        }



        // POST: /MapStyles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="ID,FeatureTypeID,Hue,Saturation,Lightness,Gamma,MapID")] MapStyle mapStyle)
        {

            var redirectURL = "../Maps/Edit/" + mapStyle.MapID + "#appearance";

            // If the page is reloaded with an error message from below, the MapID is lost in the querystring
            // so save the MapID in the ViewBag to use in those cases
            ViewBag.MapID = mapStyle.MapID;


            if (ModelState.IsValid)
            {
                db.MapStyles.Add(mapStyle);
                db.SaveChanges();
                return Redirect(redirectURL);
            }

            ViewBag.MapID = new SelectList(db.Maps, "ID", "Title", mapStyle.MapID);
            ViewBag.FeatureTypeID = new SelectList(db.FeatureTypes, "ID", "FeatureType1", mapStyle.FeatureTypeID);
            return Redirect(redirectURL);
        }





        // GET: /MapStyles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MapStyle mapStyle = db.MapStyles.Find(id);
            if (mapStyle == null)
            {
                return HttpNotFound();
            }
            ViewBag.MapID = new SelectList(db.Maps, "ID", "Title", mapStyle.MapID);
            ViewBag.FeatureTypeID = new SelectList(db.FeatureTypes, "ID", "FeatureType1", mapStyle.FeatureTypeID);
            return View(mapStyle);
        }




        // POST: /MapStyles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ID,FeatureTypeID,Hue,Saturation,Lightness,Gamma,MapID")] MapStyle mapStyle)
        {

            var redirectURL = "../../Maps/Edit/" + mapStyle.MapID + "#appearance";

            // If the page is reloaded with an error message from below, the MapID is lost in the querystring
            // so save the MapID in the ViewBag to use in those cases
            ViewBag.MapID = mapStyle.MapID;

            if (ModelState.IsValid)
            {
                db.Entry(mapStyle).State = EntityState.Modified;
                db.SaveChanges();
                return Redirect(redirectURL);
            }
            ViewBag.MapID = new SelectList(db.Maps, "ID", "Title", mapStyle.MapID);
            ViewBag.FeatureTypeID = new SelectList(db.FeatureTypes, "ID", "FeatureType1", mapStyle.FeatureTypeID);
            return Redirect(redirectURL);
        }



        // GET: /MapStyles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MapStyle mapStyle = db.MapStyles.Find(id);
            if (mapStyle == null)
            {
                return HttpNotFound();
            }
            return View(mapStyle);
        }




        // POST: /MapStyles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MapStyle mapStyle = db.MapStyles.Find(id);
            var mapId = mapStyle.MapID;

            db.MapStyles.Remove(mapStyle);
            db.SaveChanges();

            var redirectURL = "../../Maps/Edit/" + mapId + "#appearance";
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
