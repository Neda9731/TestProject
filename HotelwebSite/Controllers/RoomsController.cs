using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HotelwebSite.Models;

namespace HotelwebSite.Controllers
{
    public class RoomsController : Controller
    {
        private HotelDBEntities db = new HotelDBEntities();

        // GET: Rooms
        public ActionResult Index()
        {
            if (Session["admin"] != null)
            {
                 return View(db.Rooms.ToList());
            }
            return RedirectToAction("index", "Home");
           
        }
        public ActionResult newindex()
        { // TempData["message"] = "رزروی وجود ندارد";
            if(Session["user"]!=null)
            return View(db.Rooms.ToList());
          
            return RedirectToAction("Index", "Home");
        }
        // GET: Rooms/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rooms rooms = db.Rooms.Find(id);
            if (rooms == null)
            {
                return HttpNotFound();
            }
            return View(rooms);
        }

        // GET: Rooms/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Rooms/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AcceptVerbs(HttpVerbs.Post), ValidateInput(false)]
        public ActionResult Create([Bind(Include = "Number_r,price_r,Desc_r")] Rooms rooms)
        {
           
            if (ModelState.IsValid)
            {
                HttpPostedFileBase file0 = Request.Files["uploadedfiles"];
                if(file0!=null&& file0.ContentLength>0)
                {
                    System.IO.Directory.CreateDirectory(System.IO.Path.Combine(Server.MapPath("~/uploadedfiles/Rooms/")));
                    var filename0 = System.IO.Path.GetFileName(file0.FileName);
                    var path0 = System.IO.Path.Combine(Server.MapPath("~/uploadedfiles/Rooms/"), (filename0));
                    file0.SaveAs(path0);
                    rooms.Image_r = "~/uploadedfiles/Rooms/" + (filename0);
                }
                db.Rooms.Add(rooms);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(rooms);
        }

        // GET: Rooms/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rooms rooms = db.Rooms.Find(id);
            if (rooms == null)
            {
                return HttpNotFound();
            }
            return View(rooms);
        }

        // POST: Rooms/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Code_r,Number_r,price_r,Desc_r")] Rooms rooms)
        {
            if (ModelState.IsValid)
            {
                Rooms room = db.Rooms.Find(rooms.Code_r);
                room.price_r = rooms.price_r;
                room.Desc_r = rooms.Desc_r;
               // db.Entry(rooms).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(rooms);
        }

        // GET: Rooms/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rooms rooms = db.Rooms.Find(id);
            if (rooms == null)
            {
                return HttpNotFound();
            }
            return View(rooms);
        }

        // POST: Rooms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Rooms rooms = db.Rooms.Find(id);
            db.Rooms.Remove(rooms);
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
