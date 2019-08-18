using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EndToEnd;

namespace EndToEnd.Controllers
{
    public class CCsController : Controller
    {
        private Entities db = new Entities();

        // GET: CCs
        public ActionResult Index()
        {
            return View(db.CCs.ToList());
        }

        // GET: CCs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CC cC = db.CCs.Find(id);
            if (cC == null)
            {
                return HttpNotFound();
            }
            return View(cC);
        }

        // GET: CCs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CCs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CCID,CCName,CCDescription")] CC cC)
        {
            if (ModelState.IsValid)
            {
                db.CCs.Add(cC);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cC);
        }

        // GET: CCs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CC cC = db.CCs.Find(id);
            if (cC == null)
            {
                return HttpNotFound();
            }
            return View(cC);
        }

        // POST: CCs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CCID,CCName,CCDescription")] CC cC)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cC).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cC);
        }

        // GET: CCs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CC cC = db.CCs.Find(id);
            if (cC == null)
            {
                return HttpNotFound();
            }
            return View(cC);
        }

        // POST: CCs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CC cC = db.CCs.Find(id);
            db.CCs.Remove(cC);
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
