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
    public class MapsController : Controller
    {
        private RCMMapsEntities db = new RCMMapsEntities();

        // GET: /Maps/
        public ActionResult Index(string searchString)
        {

            // Send the default map values
            MapDefault mapDefaults = db.MapDefaults.Find(1);
            ViewBag.DefaultWidth = mapDefaults.Width;
            ViewBag.DefaultHeight = mapDefaults.Height;
            ViewBag.DefaultCenterOnLat = mapDefaults.CenterOnLat;
            ViewBag.DefaultCenterOnLong = mapDefaults.CenterOnLong;
            ViewBag.DefaultZoomLevel = mapDefaults.ZoomLevel;
            ViewBag.DefaultDisplayZoomControl = mapDefaults.DisplayZoomControl.ToString();
            ViewBag.DefaultDisplayMapTypeControl = mapDefaults.DisplayMapTypeControl.ToString();
            ViewBag.DefaultDisplayScaleControl = mapDefaults.DisplayScaleControl.ToString();
            ViewBag.DefaultDisplayStreetViewControl = mapDefaults.DisplayStreetViewControl.ToString();
            ViewBag.DefaultDisplayPanControl = mapDefaults.DisplayPanControl.ToString();

            // Create a list of Maps to filter against
            var maps = from a in db.Maps
                         select a;

            // Check for a previous search for this user
            UserSearch userSearch = db.UserSearches.SingleOrDefault(s => s.Username == User.Identity.Name);

            // Save the current search in the database
            // If an existing search record exists, then update it with the new search text
            if (!String.IsNullOrEmpty(searchString) && userSearch != null)
            {
                userSearch.MapsSearchString = searchString;
                db.Entry(userSearch).State = EntityState.Modified;
                db.SaveChanges();
            }

            // If the user has no existing searches, then add a new record with the search text
            if (!String.IsNullOrEmpty(searchString) && userSearch == null)
            {
                var newUserSearch = new Models.UserSearch();
                newUserSearch.Username = User.Identity.Name;
                newUserSearch.MapsSearchString = searchString;
                db.UserSearches.Add(newUserSearch);
                db.SaveChanges();
            }


            // If this is a fresh page load, get the saved search if there is one
            if (userSearch != null && !String.IsNullOrEmpty(userSearch.MapsSearchString))
            {
                searchString = userSearch.MapsSearchString;
                ViewBag.MapsSearchString = searchString;
            }
            

            // Set the search results to nothing by default
            if (String.IsNullOrEmpty(searchString))
            {
                maps = maps.Where(s => s.Title.Contains("fkwlenbisengoapengies"));
            }


            // Filter by text, if there is any
            if (!String.IsNullOrEmpty(searchString))
            {
                maps = maps.Where(s => s.Title.Contains(searchString) || s.Description.Contains(searchString) || s.Tags.Contains(searchString));
            }


            // If the user searches for *, show all records
            if (searchString == "*")
            {
                maps = from a in db.Maps
                       select a;
            }

            
            // Return the filtered list in alphabetical order
            return View(maps.OrderBy(s => s.Title));

        }




        // GET: /Maps/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Map map = db.Maps.Find(id);
            if (map == null)
            {
                return HttpNotFound();
            }


            // Get the styles for the map
            var allStyles = (from a in db.MapStyles
                             where a.MapID == id
                             select a);

            ViewBag.allStyles = allStyles.OrderBy(s => s.FeatureType.FeatureType1);
            ViewBag.allStylesCount = allStyles.Count();


            // Get a list of all Boards for the map
            var allBoards = (from a in db.Map_Boards
                                    where a.MapID == id
                                    select a);

            ViewBag.allBoards = allBoards;
            ViewBag.allBoardsCount = allBoards.Count();


            // Get a list of all Pins for the map
            var allPins = (from a in db.Pins
                             where a.MapID == id
                             select a);

            ViewBag.allPins = allPins;
            ViewBag.allPinsCount = allPins.Count();


            return View(map);
        }




        // GET: /Maps/Create
        public ActionResult Create()
        {
            return View();
        }




        // POST: /Maps/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="ID,Title,Description,Width,Height,CenterOnLat,CenterOnLong,ZoomLevel,DisplayZoomControl,DisplayMapTypeControl,DisplayScaleControl,DisplayStreetViewControl,DisplayPanControl,Tags,DateCreated,CreatedBy,DateModified,ModifiedBy")] Map map)
        {
            if (ModelState.IsValid)
            {
                // Stamp the Map with CreatedBy and DateCreated
                map.CreatedBy = User.Identity.Name;
                map.DateCreated = DateTime.Now;

                // Add the map
                var thisID = db.Maps.Add(map);
 
                // Add the default styles for this map
                var thisMapStyle = new MapStyle();
                var mapStyleDefaults = db.MapStyleDefaults.ToList();

                foreach (var defaultMapStyle in mapStyleDefaults)
                {
                    thisMapStyle.MapID = thisID.ID;
                    thisMapStyle.FeatureTypeID = defaultMapStyle.FeatureTypeID;
                    thisMapStyle.Hue = defaultMapStyle.Hue;
                    thisMapStyle.Saturation = defaultMapStyle.Saturation;
                    thisMapStyle.Lightness = defaultMapStyle.Lightness;
                    thisMapStyle.Gamma = defaultMapStyle.Gamma;

                    db.MapStyles.Add(thisMapStyle);
                    db.SaveChanges();
                }
                
                
                db.SaveChanges();
                return RedirectToAction("Edit", thisID);
            }

            return View(map);
        }






        // GET: /Maps/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Map map = db.Maps.Find(id);
            if (map == null)
            {
                return HttpNotFound();
            }


            // Get the styles for the map
            var allStyles = (from a in db.MapStyles
                             where a.MapID == id
                             select a);

            ViewBag.allStyles = allStyles.OrderBy(s => s.FeatureType.FeatureType1);
            ViewBag.allStylesCount = allStyles.Count();


            // Get a list of all Boards for the map
            var allBoards = (from a in db.Map_Boards
                             where a.MapID == id
                             select a);

            ViewBag.allBoards = allBoards;
            ViewBag.allBoardsCount = allBoards.Count();


            // Get a list of all Pins for the map
            var allPins = (from a in db.Pins
                           where a.MapID == id
                           select a);

            ViewBag.allPins = allPins;
            ViewBag.allPinsCount = allPins.Count();


            return View(map);
        }




        // POST: /Maps/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ID,Title,Description,Width,Height,CenterOnLat,CenterOnLong,ZoomLevel,DisplayZoomControl,DisplayMapTypeControl,DisplayScaleControl,DisplayStreetViewControl,DisplayPanControl,Tags,DateCreated,CreatedBy,DateModified,ModifiedBy")] Map map)
        {
            if (ModelState.IsValid)
            {
                map.ModifiedBy = User.Identity.Name;
                map.DateModified = DateTime.Now;

                db.Entry(map).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Edit");
            }
            return View(map);
        }




        // GET: /Maps/Copy/5
        public ActionResult Copy(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            Map map = db.Maps.Find(id);

            if (map == null)
            {
                return HttpNotFound();
            }

            return View(map);
        }




        // POST: /Maps/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Copy([Bind(Include = "ID,Title")] Map map)
        {
            var newMap = db.Maps.Find(map.ID);

            // Stamp the Map with CreatedBy and DateCreated
            newMap.Title = map.Title;
            newMap.CreatedBy = User.Identity.Name;
            newMap.DateCreated = DateTime.Now;

            // Add the map
            var thisID = db.Maps.Add(newMap);


            // Copy the styles from the source map to this new map
            var sourceMapStyles = from a in db.MapStyles
                                  where a.MapID == map.ID
                                  select a;

            foreach (var mapStyle in sourceMapStyles)
            {
                var thisMapStyle = new MapStyle();
                thisMapStyle.MapID = thisID.ID;
                thisMapStyle.FeatureTypeID = mapStyle.FeatureTypeID;
                thisMapStyle.Hue = mapStyle.Hue;
                thisMapStyle.Saturation = mapStyle.Saturation;
                thisMapStyle.Lightness = mapStyle.Lightness;
                thisMapStyle.Gamma = mapStyle.Gamma;

                db.Entry(thisMapStyle).State = EntityState.Added;
            }



            // Copy the boards from the source map to this new map
            var sourceMapBoards = (from a in db.Map_Boards
                                   where a.MapID == map.ID
                                   select a);

            foreach (var mapBoard in sourceMapBoards)
            {
                var thisBoard = new Map_Boards();
                thisBoard.MapID = thisID.ID;
                thisBoard.BoardID = mapBoard.BoardID;
                thisBoard.PinTypeID = mapBoard.PinTypeID;

                db.Entry(thisBoard).State = EntityState.Added;
            }



            // Copy the pins from the source map to this new map
            var sourceMapPins = (from a in db.Pins
                                 where a.MapID == map.ID
                                 select a);

            foreach (var mapPin in sourceMapPins)
            {
                var thisPin = new Pin();
                thisPin.MapID = thisID.ID;
                thisPin.Title = mapPin.Title;
                thisPin.Latitude = mapPin.Latitude;
                thisPin.Longitude = mapPin.Longitude;
                thisPin.PinTypeID = mapPin.PinTypeID;

                db.Entry(thisPin).State = EntityState.Added;
            }

           
            db.SaveChanges();
            return RedirectToAction("Edit", thisID);
        }




        // GET: /Maps/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Map map = db.Maps.Find(id);
            if (map == null)
            {
                return HttpNotFound();
            }
            return View(map);
        }

        // POST: /Maps/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Map map = db.Maps.Find(id);
            db.Maps.Remove(map);
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
