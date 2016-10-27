using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using BSKLedenManagement.Helpers;
using BSKLedenManagement.Models;

namespace BSKLedenManagement.Controllers
{
    [Authorize]

    public class MembersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Members
        public ActionResult Index(string sortOrder, string search)
        {
            if (String.IsNullOrEmpty(sortOrder))
            {
                sortOrder = "firstname";
            }
            ViewBag.FirstnameSortParm = String.IsNullOrEmpty(sortOrder) ? "firstname_desc" : "firstname";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "date";
            ViewBag.KringSortParm = sortOrder == "Kring" ? "kring_desc" : "kring";


            var ledenlijst = new List<Member>();
            var leden = from s in db.Members
                        select s;

            if (!String.IsNullOrEmpty(search))
            {
                leden = leden.Where(s => s.LastName.Contains(search)
                                         || s.FirstName.Contains(search)
                                         || s.Kring.Contains(search));
            }
           
            switch (sortOrder)
            {
                case "lastname_desc":
                    ledenlijst = leden.OrderByDescending(x => x.LastName).ToList();
                    break;
                case "lastname":
                    ledenlijst = leden.OrderBy(x => x.LastName).ToList();
                    break;
                case "firstname_desc":
                    ledenlijst = leden.OrderByDescending(x => x.FirstName).ToList();
                    break;
                case "firstname":
                    ledenlijst = leden.OrderBy(x => x.FirstName).ToList();
                    break;
                case "kring_desc":
                    ledenlijst = leden.OrderByDescending(x => x.Kring).ToList();
                    break;
                case "kring":
                    ledenlijst = leden.OrderBy(x => x.Kring).ToList();
                    break;
                case "date_desc":
                    ledenlijst = leden.OrderByDescending(x => x.RegisterDate).ToList();
                    break;
                case "date":
                    ledenlijst = leden.OrderBy(x => x.RegisterDate).ToList();
                    break;
            }
            Logger.Log(LogType.Info, User.Identity.Name, "Ledenlijst bekeken.");
            return View(ledenlijst);
        }

        public FileContentResult Backup()
        {
            //before your loop
            var csv = new StringBuilder();

            var leden = db.Members.OrderBy(x => x.Id).ToList();
            foreach (var lid in leden)
            {
                //Suggestion made by KyleMit
                var newLine = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9}", lid.Id, lid.FirstName, lid.LastName, lid.Kring, lid.RegisterDate, lid.Adres, lid.Email,  lid.Phone, lid.IsActive,lid.Verzekering);
                csv.AppendLine(newLine);
            }
            Logger.Log(LogType.Success, User.Identity.Name, "Backup gedownload.");
            return File(new UTF8Encoding().GetBytes(csv.ToString()), "text/csv", "BSK_Ledenlijst_Backup_ "+ DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day +".csv");
        }

        // GET: Members/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Member member = db.Members.Find(id);
            if (member == null)
            {
                return HttpNotFound();
            }
            return View(member);
        }

        // GET: Members/Create
        public ActionResult Create()
        {
            var model = new MemberViewModel();

            //get kringen
            var kringen = db.Krings.OrderBy(x => x.Name).ToList();

            var kringList = new List<SelectListItem>();

            foreach (var kring in kringen)
            {
                var kringListItem = new SelectListItem
                {
                    Text = kring.Name,
                    Value = kring.Name
                };
                //jaja binden op nen text ipv een id zuigt maar boeie
                kringList.Add(kringListItem);
            }

            var vmKringList = new SelectList(kringList, "Value", "Text");
            model.Kringen = vmKringList;
            model.RegisterDate = DateTime.Now.Date;

            //standaard aanvinken
            model.IsActive = true;

            return View(model);
        }

        // POST: Members/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FirstName,LastName,Kring,RegisterDate,Adres,Email,Phone,IsActive,Verzekering")] Member member)
        {
            if (ModelState.IsValid)
            {
                db.Members.Add(member);
                db.SaveChanges();
                var msg = "Lid werd toegevoegd: \n" + member.FirstName + " " + member.LastName + " met als kring " +
                          member.Kring + " en email: " + member.Email +  " verzekering: " + member.Verzekering;
                Logger.Log(LogType.Success, User.Identity.Name, "Lid toegevoegd: " + msg);
                MailSender.SendMultiMail("[BSK vzw] Nieuwe registratie lid", msg);
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
            Member member = db.Members.Find(id);
            if (member == null)
            {
                return HttpNotFound();
            }
            Logger.Log(LogType.Warning, User.Identity.Name, "Bewerken van lid: " + 
                member.FirstName + " " + 
                member.LastName + " " +
                member.Email + " " +
                member.Kring + " " +
                member.IsActive + " " +
                member.Verzekering + " " +
                member.Adres + " " + 
                member.Phone
                );
            return View(member);
        }

        // POST: Members/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FirstName,LastName,Kring,RegisterDate,Adres,Email,Phone,IsActive,Verzekering")] Member member)
        {
            if (ModelState.IsValid)
            {
                db.Entry(member).State = EntityState.Modified;
                db.SaveChanges();
                Logger.Log(LogType.Warning, User.Identity.Name, "Lidgegevens aangepast: " +
                                                                member.FirstName + " " +
                                                                member.LastName + " " +
                                                                member.Email + " " +
                                                                member.Kring + " " +
                                                                member.IsActive + " " +
                                                                member.Verzekering + " " +
                                                                member.Adres + " " +
                                                                member.Phone);
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
            Member member = db.Members.Find(id);
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
            Member member = db.Members.Find(id);
            var deletedMember = member.FirstName + " " + member.LastName;
            db.Members.Remove(member);
            db.SaveChanges();
            Logger.Log(LogType.Deleted, User.Identity.Name, "Lid verwijderd: " + deletedMember);
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
