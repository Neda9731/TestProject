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
    public class MembersController : Controller
    {
        private HotelDBEntities db = new HotelDBEntities();

        // GET: Members
        public ActionResult Index()
        {
            return View(db.Member.ToList());
        }
        public ActionResult login(string Username,string password)
        {
           var table1= db.Member.Where(t => t.Username_m.Equals(Username) && t.Password_m.Equals(password)).ToList();
          
            if(Username=="admin" && password=="123")
           {
               Session["admin"] = Username;
               Session["user"] = null;
               return RedirectToAction("Index", "Rooms");
           }else if (table1.Count > 0)
           {
               Session["user"] = Username;
               Session["admin"] = null;
               return RedirectToAction("newindex", "Rooms");
           }else  
               return View("Create");
        }
        public ActionResult exit()
        {
            Session["admin"] = null;
            Session["user"] = null;
            return RedirectToAction("Index", "Home");
            //return RedirectToAction("newindex", "Rooms");
        }
        // GET: Members/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Member member = db.Member.Find(id);
            if (member == null)
            {
                return HttpNotFound();
            }
            return View(member);
        }

        // GET: Members/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Members/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Code_m,name_m,Username_m,Password_m,Email_m")] Member member)
        {
            if (ModelState.IsValid)
            {
                db.Member.Add(member);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(member);
        }

        // GET: Members/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Member member = db.Member.Find(id);
            if (member == null)
            {
                return HttpNotFound();
            }
            return View(member);
        }

        // POST: Members/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Code_m,name_m,Username_m,Password_m,Email_m")] Member member)
        {
            if (ModelState.IsValid)
            {
                db.Entry(member).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(member);
        }

        // GET: Members/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Member member = db.Member.Find(id);
            if (member == null)
            {
                return HttpNotFound();
            }
            return View(member);
        }

        // POST: Members/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Member member = db.Member.Find(id);
            db.Member.Remove(member);
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
