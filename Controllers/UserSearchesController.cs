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
    public class UserSearchesController : Controller
    {
        private RCMMapsEntities db = new RCMMapsEntities();

        // GET: /UserSearches/
        public ActionResult Index()
        {
            return View(db.UserSearches.ToList());
        }

        // GET: /UserSearches/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserSearch userSearch = db.UserSearches.Find(id);
            if (userSearch == null)
            {
                return HttpNotFound();
            }
            return View(userSearch);
        }

        // GET: /UserSearches/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /UserSearches/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="ID,Username,MapsSearchString,BoardsSearchString,BoardType,DMA,Vendor")] UserSearch userSearch)
        {
            if (ModelState.IsValid)
            {
                db.UserSearches.Add(userSearch);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(userSearch);
        }

        // GET: /UserSearches/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserSearch userSearch = db.UserSearches.Find(id);
            if (userSearch == null)
            {
                return HttpNotFound();
            }
            return View(userSearch);
        }



        // POST: /UserSearches/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ID,Username,MapsSearchString,BoardsSearchString,BoardType,DMA,Vendor")] UserSearch userSearch)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userSearch).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(userSearch);
        }



        // POST: /UserSearches/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ClearMapsSearchString()
        {
            if (ModelState.IsValid)
            {
                UserSearch userSearch = db.UserSearches.SingleOrDefault(s => s.Username == User.Identity.Name);

                userSearch.MapsSearchString = null;

                db.Entry(userSearch).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index", "Maps");
            }

            return RedirectToAction("Index", "Maps");
        }



        // POST: /UserSearches/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ClearBoardsSearchString(string redirectTo, string mapId)
        {

            ViewBag.MapID = mapId;

            if (ModelState.IsValid)
            {
                UserSearch userSearch = db.UserSearches.SingleOrDefault(s => s.Username == User.Identity.Name);

                userSearch.BoardsSearchString = null;
                userSearch.BoardType = null;
                userSearch.DMA = null;
                userSearch.Vendor = null;

                db.Entry(userSearch).State = EntityState.Modified;
                db.SaveChanges();

                if(redirectTo == "Boards"){
                    return RedirectToAction("Index", "Boards");
                }

                if (redirectTo == "Map_Boards")
                {
                    return RedirectToAction("Index", "Map_Boards", new { MapID = mapId});
                }
                
            }

            return RedirectToAction("Index", "Boards");
        }




        // GET: /UserSearches/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserSearch userSearch = db.UserSearches.Find(id);
            if (userSearch == null)
            {
                return HttpNotFound();
            }
            return View(userSearch);
        }

        // POST: /UserSearches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserSearch userSearch = db.UserSearches.Find(id);
            db.UserSearches.Remove(userSearch);
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
