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
    public class ReservationsController : Controller
    {
        private HotelDBEntities db = new HotelDBEntities();

        // GET: Reservations
        public ActionResult Index()
        {
            var reservation = db.Reservation.Include(r => r.Member).Include(r => r.Rooms);
            return View(reservation.ToList());
        }
        public ActionResult reserv(int roomCode)
        {
            Reservation reserv1 = new Reservation();
            reserv1.Code_r = roomCode;
            string s = "null";
            if(Session["user"]!=null)
          s = Session["user"].ToString();
            int membercode = db.Member.Where(t => t.Username_m.Equals(s)).First().Code_m;
            reserv1.Code_m = membercode;
                reserv1.date = DateTime.Now;
                db.Reservation.Add(reserv1);
                db.SaveChanges();

                TempData["message"] = "ثبت رزرو شما انجام شد";
                return RedirectToAction("newindex", "Rooms");
        }

        // GET: Reservations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservation reservation = db.Reservation.Find(id);
            if (reservation == null)
            {
                return HttpNotFound();
            }
            return View(reservation);
        }

        // GET: Reservations/Create
        public ActionResult Create()
        {
            ViewBag.Code_m = new SelectList(db.Member, "Code_m", "name_m");
            ViewBag.Code_r = new SelectList(db.Rooms, "Code_r", "Desc_r");
            return View();
        }

        // POST: Reservations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Code_reserv,Code_r,Code_m,date")] Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                db.Reservation.Add(reservation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Code_m = new SelectList(db.Member, "Code_m", "name_m", reservation.Code_m);
            ViewBag.Code_r = new SelectList(db.Rooms, "Code_r", "Desc_r", reservation.Code_r);
            return View(reservation);
        }

        // GET: Reservations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservation reservation = db.Reservation.Find(id);
            if (reservation == null)
            {
                return HttpNotFound();
            }
            ViewBag.Code_m = new SelectList(db.Member, "Code_m", "name_m", reservation.Code_m);
            ViewBag.Code_r = new SelectList(db.Rooms, "Code_r", "Desc_r", reservation.Code_r);
            return View(reservation);
        }

        // POST: Reservations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Code_reserv,Code_r,Code_m,date")] Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(reservation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Code_m = new SelectList(db.Member, "Code_m", "name_m", reservation.Code_m);
            ViewBag.Code_r = new SelectList(db.Rooms, "Code_r", "Desc_r", reservation.Code_r);
            return View(reservation);
        }

        // GET: Reservations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservation reservation = db.Reservation.Find(id);
            if (reservation == null)
            {
                return HttpNotFound();
            }
            return View(reservation);
        }

        // POST: Reservations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Reservation reservation = db.Reservation.Find(id);
            db.Reservation.Remove(reservation);
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
