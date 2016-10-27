using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BSKLedenManagement.Helpers;
using BSKLedenManagement.Models;

namespace BSKLedenManagement.Controllers
{
    [Authorize]

    public class KringenController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Kringen
        public ActionResult Index()
        {
            Logger.Log(LogType.Info, User.Identity.Name, "Kringenlijst bekeken.");
            return View(db.Krings.ToList());
        }

        // GET: Kringen/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kring kring = db.Krings.Find(id);
            if (kring == null)
            {
                return HttpNotFound();
            }
            return View(kring);
        }

        // GET: Kringen/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Kringen/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] Kring kring)
        {
            if (ModelState.IsValid)
            {
                db.Krings.Add(kring);
                db.SaveChanges();
                Logger.Log(LogType.Success, User.Identity.Name, "Kring toegevoegd.");
                return RedirectToAction("Index");
            }

            return View(kring);
        }

        // GET: Kringen/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kring kring = db.Krings.Find(id);
            if (kring == null)
            {
                return HttpNotFound();
            }
            return View(kring);
        }

        // POST: Kringen/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] Kring kring)
        {
            if (ModelState.IsValid)
            {
                db.Entry(kring).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(kring);
        }

        // GET: Kringen/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kring kring = db.Krings.Find(id);
            if (kring == null)
            {
                return HttpNotFound();
            }
            return View(kring);
        }

        // POST: Kringen/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Kring kring = db.Krings.Find(id);
            db.Krings.Remove(kring);
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
