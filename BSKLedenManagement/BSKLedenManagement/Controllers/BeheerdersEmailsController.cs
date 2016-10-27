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
    public class BeheerdersEmailsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: BeheerdersEmails
        public ActionResult Index()
        {
            Logger.Log(LogType.Info, User.Identity.Name, "BeheerdersEmails bekeken.");
            return View(db.BeheerdersEmails.ToList());
        }

        // GET: BeheerdersEmails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BeheerdersEmail beheerdersEmail = db.BeheerdersEmails.Find(id);
            if (beheerdersEmail == null)
            {
                return HttpNotFound();
            }
            return View(beheerdersEmail);
        }

        // GET: BeheerdersEmails/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BeheerdersEmails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Email")] BeheerdersEmail beheerdersEmail)
        {
            if (ModelState.IsValid)
            {
                db.BeheerdersEmails.Add(beheerdersEmail);
                db.SaveChanges();
                Logger.Log(LogType.Success, User.Identity.Name, "Nieuwe beheerdersemail toegevoegd: " + beheerdersEmail.Email);

                return RedirectToAction("Index");
            }

            return View(beheerdersEmail);
        }

        // GET: BeheerdersEmails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BeheerdersEmail beheerdersEmail = db.BeheerdersEmails.Find(id);
            if (beheerdersEmail == null)
            {
                return HttpNotFound();
            }
            return View(beheerdersEmail);
        }

        // POST: BeheerdersEmails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Email")] BeheerdersEmail beheerdersEmail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(beheerdersEmail).State = EntityState.Modified;
                db.SaveChanges();
                Logger.Log(LogType.Warning, User.Identity.Name, "BeheerdersEmail aangepast:" + beheerdersEmail.Email);

                return RedirectToAction("Index");
            }
            return View(beheerdersEmail);
        }

        // GET: BeheerdersEmails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BeheerdersEmail beheerdersEmail = db.BeheerdersEmails.Find(id);
            if (beheerdersEmail == null)
            {
                return HttpNotFound();
            }
            return View(beheerdersEmail);
        }

        // POST: BeheerdersEmails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var mail = "";
            BeheerdersEmail beheerdersEmail = db.BeheerdersEmails.Find(id);
            mail = beheerdersEmail.Email;
            db.BeheerdersEmails.Remove(beheerdersEmail);
            db.SaveChanges();
            Logger.Log(LogType.Deleted, User.Identity.Name, "BeheerdersEmail verwijderd: " + mail);

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
