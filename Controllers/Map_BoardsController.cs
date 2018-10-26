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
    public class Map_BoardsController : Controller
    {
        private RCMMapsEntities db = new RCMMapsEntities();

        // GET: /Map_Boards/
        public ActionResult Index(string searchString, string boardType, string dmas, string vendors, int mapId)
        {

            // Save the MapID in the Viewbag for redirecting back
            ViewBag.MapID = mapId;


            // Create a list of Boards to filter against
            var boards = from a in db.Boards
                         select a;

            // Create a list of Map_Boards to see if the Board is already associated with this Map
            var map_boards = from a in db.Map_Boards
                             where a.MapID == mapId
                             select a;

            ViewBag.MapBoards = map_boards;


            // Check for a previous search for this user
            UserSearch userSearch = db.UserSearches.SingleOrDefault(s => s.Username == User.Identity.Name);


            // Create search dropdowns
            // BoardTypes
            if (userSearch != null && !String.IsNullOrEmpty(userSearch.BoardType))
            {
                ViewBag.BoardType = new SelectList(db.BoardTypes.OrderBy(s => s.BoardType1), "BoardType1", "BoardType1", userSearch.BoardType);
            }
            else
            {
                ViewBag.BoardType = new SelectList(db.BoardTypes.OrderBy(s => s.BoardType1), "BoardType1", "BoardType1");
            }


            // DMAs
            if (userSearch != null && !String.IsNullOrEmpty(userSearch.DMA))
            {
                ViewBag.DMAs = new SelectList(db.DMAs.OrderBy(s => s.DMA1), "DMA1", "DMA1", userSearch.DMA);
            }
            else
            {
                ViewBag.DMAs = new SelectList(db.DMAs.OrderBy(s => s.DMA1), "DMA1", "DMA1");
            }


            // Board Vendors
            if (userSearch != null && !String.IsNullOrEmpty(userSearch.Vendor))
            {
                ViewBag.Vendors = new SelectList(db.BoardVendors.OrderBy(s => s.Vendor), "Vendor", "Vendor", userSearch.Vendor);
            }
            else
            {
                ViewBag.Vendors = new SelectList(db.BoardVendors.OrderBy(s => s.Vendor), "Vendor", "Vendor");
            }




            // Save the current search in the database
            // If an existing search record exists, then update it with the new search text
            if ((!String.IsNullOrEmpty(searchString) || !String.IsNullOrEmpty(boardType) || !String.IsNullOrEmpty(dmas) || !String.IsNullOrEmpty(vendors)) && userSearch != null)
            {
                userSearch.BoardsSearchString = searchString;
                userSearch.BoardType = boardType;
                userSearch.DMA = dmas;
                userSearch.Vendor = vendors;

                db.Entry(userSearch).State = EntityState.Modified;
                db.SaveChanges();
            }

            // If the user has no existing searches, then add a new record with the search text
            if ((!String.IsNullOrEmpty(searchString) || !String.IsNullOrEmpty(boardType) || !String.IsNullOrEmpty(dmas) || !String.IsNullOrEmpty(vendors)) && userSearch == null)
            {
                var newUserSearch = new Models.UserSearch();
                newUserSearch.Username = User.Identity.Name;
                newUserSearch.BoardsSearchString = searchString;
                newUserSearch.BoardType = boardType;
                newUserSearch.DMA = dmas;
                newUserSearch.Vendor = vendors;

                db.UserSearches.Add(newUserSearch);
                db.SaveChanges();
            }


            // If this is a fresh page load, get the saved search if there is one
            if (userSearch != null && (!String.IsNullOrEmpty(userSearch.BoardsSearchString) || !String.IsNullOrEmpty(userSearch.BoardType) || !String.IsNullOrEmpty(userSearch.DMA) || !String.IsNullOrEmpty(userSearch.Vendor)))
            {
                searchString = userSearch.BoardsSearchString;
                boardType = userSearch.BoardType;
                dmas = userSearch.DMA;
                vendors = userSearch.Vendor;

                ViewBag.BoardsSearchString = searchString;
            }


            // Set the search results to nothing by default
            if (String.IsNullOrEmpty(searchString) && String.IsNullOrEmpty(boardType) && String.IsNullOrEmpty(dmas) && String.IsNullOrEmpty(vendors))
            {
                boards = boards.Where(s => s.BoardNumber.Contains("fkwlenbisengoapengies"));
            }


            // Filter by text, if there is any
            if (!String.IsNullOrEmpty(searchString))
            {
                boards = boards.Where(s => s.BoardNumber.Contains(searchString) || s.Description.Contains(searchString) || s.Tags.Contains(searchString));
            }

            // Filter by BoardType, if one has been selected in the dropdown
            if (!String.IsNullOrEmpty(boardType))
            {
                boards = boards.Where(x => x.BoardType.BoardType1 == boardType);
            }

            // Filter by DMA, if one has been selected in the dropdown
            if (!String.IsNullOrEmpty(dmas))
            {
                boards = boards.Where(x => x.DMA.DMA1 == dmas);
            }

            // Filter by Board Vendor, if one has been selected in the dropdown
            if (!String.IsNullOrEmpty(vendors))
            {
                boards = boards.Where(x => x.BoardVendor.Vendor == vendors);
            }


            // If the user searches for *, show all records
            if (searchString == "*" && String.IsNullOrEmpty(boardType) && String.IsNullOrEmpty(dmas) && String.IsNullOrEmpty(vendors))
            {
                boards = from a in db.Boards
                         select a;
            }


            // Return the filtered list in alphabetical order
            return View(boards.OrderBy(s => s.BoardNumber));
        }



        // GET: /Map_Boards/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Map_Boards map_Boards = db.Map_Boards.Find(id);
            if (map_Boards == null)
            {
                return HttpNotFound();
            }
            return View(map_Boards);
        }



        // GET: /Map_Boards/Create
        public ActionResult Create()
        {
            ViewBag.BoardID = new SelectList(db.Boards, "ID", "BoardNumber");
            ViewBag.MapID = new SelectList(db.Maps, "ID", "Title");
            return View();
        }




        // POST: /Map_Boards/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="ID,MapID,BoardID")] Map_Boards map_Boards)
        {
            if (ModelState.IsValid)
            {
                db.Map_Boards.Add(map_Boards);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BoardID = new SelectList(db.Boards, "ID", "BoardNumber", map_Boards.BoardID);
            ViewBag.MapID = new SelectList(db.Maps, "ID", "Title", map_Boards.MapID);
            return View(map_Boards);
        }




        // POST: /Map_Boards/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        public ActionResult AddToMap([Bind(Include = "ID,MapID,BoardID")] Map_Boards map_Boards)
        {
            var redirectURL = "../Map_Boards?mapId=" + map_Boards.MapID;

            // Set a default pin
            map_Boards.PinTypeID = 1;

            db.Map_Boards.Add(map_Boards);
            db.SaveChanges();

            return Redirect(redirectURL);

        }



        // POST: /Map_Boards/Delete/5
        public ActionResult RemoveFromMap(int id, string redirectTo)
        {
            Map_Boards map_Boards = db.Map_Boards.Find(id);
            var mapId = map_Boards.MapID;

            db.Map_Boards.Remove(map_Boards);
            db.SaveChanges();

            if (redirectTo == "Map")
            {
                var redirectURL = "../../Maps/Edit/" + mapId + "#boards";
                return Redirect(redirectURL);
            }

            else
            {
                var redirectURL = "../../Map_Boards?mapId=" + mapId;
                return Redirect(redirectURL);
            }
            
        }



        // GET: /Map_Boards/Edit/5
        public ActionResult SelectPinType(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Map_Boards map_Boards = db.Map_Boards.Find(id);
            if (map_Boards == null)
            {
                return HttpNotFound();
            }
            ViewBag.PinTypeID = new SelectList(db.PinTypes, "ID", "Title", map_Boards.PinTypeID);

            return View(map_Boards);
        }




        // POST: /Map_Boards/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SelectPinType([Bind(Include = "ID,MapID,BoardID,PinTypeID")] Map_Boards map_Boards)
        {
            var redirectURL = "../../Maps/Edit/" + map_Boards.MapID + "#boards";

            if (ModelState.IsValid)
            {
                db.Entry(map_Boards).State = EntityState.Modified;
                db.SaveChanges();
                return Redirect(redirectURL);
            }
            
            ViewBag.PinTypeID = new SelectList(db.PinTypes, "ID", "Title", map_Boards.PinTypeID);

            return Redirect(redirectURL);
        }



        // GET: /Map_Boards/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Map_Boards map_Boards = db.Map_Boards.Find(id);
            if (map_Boards == null)
            {
                return HttpNotFound();
            }
            ViewBag.BoardID = new SelectList(db.Boards, "ID", "BoardNumber", map_Boards.BoardID);
            ViewBag.MapID = new SelectList(db.Maps, "ID", "Title", map_Boards.MapID);
            return View(map_Boards);
        }




        // POST: /Map_Boards/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ID,MapID,BoardID")] Map_Boards map_Boards)
        {
            if (ModelState.IsValid)
            {
                db.Entry(map_Boards).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BoardID = new SelectList(db.Boards, "ID", "BoardNumber", map_Boards.BoardID);
            ViewBag.MapID = new SelectList(db.Maps, "ID", "Title", map_Boards.MapID);
            return View(map_Boards);
        }





        // GET: /Map_Boards/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Map_Boards map_Boards = db.Map_Boards.Find(id);
            if (map_Boards == null)
            {
                return HttpNotFound();
            }
            return View(map_Boards);
        }




        // POST: /Map_Boards/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Map_Boards map_Boards = db.Map_Boards.Find(id);
            db.Map_Boards.Remove(map_Boards);
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
