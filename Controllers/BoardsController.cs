using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RCMMaps.Models;
using System.IO;

namespace RCMMaps.Controllers
{
    [Authorize(Roles = "Editor")]
    public class BoardsController : Controller
    {
        private RCMMapsEntities db = new RCMMapsEntities();

        // GET: /Boards/
        public ActionResult Index(string searchString, string boardType, string dmas, string vendors)
        {

            // Send the required fields
            ViewBag.DefaultIlluminated = false.ToString();


            // Create a list of Boards to filter against
            var boards = from a in db.Boards
                          select a;

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




        // GET: /Boards/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Board board = db.Boards.Find(id);
            if (board == null)
            {
                return HttpNotFound();
            }
            return View(board);
        }



        // GET: /Boards/Create
        public ActionResult Create()
        {
            ViewBag.BoardTypeID = new SelectList(db.BoardTypes, "ID", "BoardType1").OrderBy( s=> s.Text);
            ViewBag.DMAID = new SelectList(db.DMAs, "ID", "DMA1").OrderBy(s => s.Text);
            ViewBag.BoardVendorID = new SelectList(db.BoardVendors, "ID", "Vendor").OrderBy(s => s.Text);
            ViewBag.ProductionVendorID = new SelectList(db.ProductionVendors, "ID", "Vendor").OrderBy(s => s.Text);
            ViewBag.BoardRatingID = new SelectList(db.BoardRatings, "ID", "Description").OrderBy(s => s.Value);
            return View();
        }



        // POST: /Boards/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Board board)
        {
            board.BoardNumber = "0";
            board.CreatedBy = User.Identity.Name;
            board.DateCreated = DateTime.Now;

            // Add the board
            var thisID = db.Boards.Add(board);
            db.SaveChanges();
            return RedirectToAction("Edit", thisID);

        }





        // GET: /Boards/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Board board = db.Boards.Find(id);
            if (board == null)
            {
                return HttpNotFound();
            }
            ViewBag.BoardTypeID = new SelectList(db.BoardTypes.OrderBy(s => s.BoardType1), "ID", "BoardType1", board.BoardTypeID);
            ViewBag.DMAID = new SelectList(db.DMAs.OrderBy(s => s.DMA1), "ID", "DMA1", board.DMAID);
            ViewBag.BoardVendorID = new SelectList(db.BoardVendors.OrderBy(s => s.Vendor), "ID", "Vendor", board.BoardVendorID);
            ViewBag.ProductionVendorID = new SelectList(db.ProductionVendors.OrderBy(s => s.Vendor), "ID", "Vendor", board.ProductionVendorID);
            ViewBag.BoardRatingID = new SelectList(db.BoardRatings.OrderBy(s => s.Rating), "ID", "Description", board.BoardRatingID);
            return View(board);
        }

        // POST: /Boards/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ID,BoardTypeID,DMAID,ProductionVendorID,BoardNumber,Description,Address,City,Zip,Latitude,Longitude,Facing,SideOfStreet,ReadDirection,Illuminated,BoardHeight,BoardWidth,Weekly18PlusImpressions,TabPanelID,LinkToProductionSpecs,BoardRatingID,Notes,Tags,DateCreated,CreatedBy,DateModified,ModifiedBy,BoardVendorID")] Board board)
        {
            if (ModelState.IsValid)
            {
                board.ModifiedBy = User.Identity.Name;
                board.DateModified = DateTime.Now;

                db.Entry(board).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BoardTypeID = new SelectList(db.BoardTypes, "ID", "BoardType1", board.BoardTypeID);
            ViewBag.DMAID = new SelectList(db.DMAs, "ID", "DMA1", board.DMAID);
            ViewBag.BoardVendorID = new SelectList(db.BoardVendors, "ID", "Vendor", board.BoardVendorID);
            ViewBag.ProductionVendorID = new SelectList(db.ProductionVendors, "ID", "Vendor", board.ProductionVendorID);
            ViewBag.BoardRatingID = new SelectList(db.BoardRatings, "ID", "Description", board.BoardRatingID);
            return View(board);
        }



        // GET: /Boards/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Board board = db.Boards.Find(id);
            if (board == null)
            {
                return HttpNotFound();
            }
            return View(board);
        }




        // POST: /Boards/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {

            // First, delete the board
            Board board = db.Boards.Find(id);
            db.Boards.Remove(board);


            // Delete the board images
            var boardImages = from a in db.BoardImages
                              where a.BoardID == id
                              select a;


            foreach (BoardImage boardImage in boardImages){

                // get the filename
                var fileName = Path.GetFileName(boardImage.Image);

                // get the path to the file
                var path = Path.Combine(Server.MapPath("~/Images/Boards"), fileName);

                // delete the file
                try
                {
                    if (System.IO.File.Exists(path))
                        System.IO.File.Delete(path);
                }
                catch (Exception)
                {
                    ModelState.AddModelError("uploadError", "Can't delete the file.");
                }


                db.BoardImages.Remove(boardImage);

            }


            // Delete the board from any existing maps
            var mapBoards = from a in db.Map_Boards
                              where a.BoardID == id
                              select a;


            foreach (Map_Boards mapBoard in mapBoards)
            {
                db.Map_Boards.Remove(mapBoard);
            }


            // Save the changes
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
