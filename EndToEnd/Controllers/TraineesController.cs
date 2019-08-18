using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EndToEnd;
using PagedList;

namespace EndToEnd.Controllers
{
    public class TraineesController : Controller
    {
        private Entities db = new Entities();

        // GET: Trainees
        public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;
            var trainees = from s in db.Trainees
                           select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                trainees = trainees.Where(s => s.TraineeName.Contains(searchString) || s.TraineeTOEIC.ToString().Contains(searchString)
                                       || s.TraineeEducation.Contains(searchString) || s.TraineeAge.ToString().Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    trainees = trainees.OrderByDescending(s => s.TraineeName);
                    break;
                case "Date":
                    trainees = trainees.OrderBy(s => s.TraineeDOB);
                    break;
                case "date_desc":
                    trainees = trainees.OrderByDescending(s => s.TraineeDOB);
                    break;
                default:
                    trainees = trainees.OrderBy(s => s.TraineeName);
                    break;
            }
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(trainees.ToPagedList(pageNumber, pageSize));
        }

        // GET: Trainees/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trainee trainee = db.Trainees.Find(id);
            if (trainee == null)
            {
                return HttpNotFound();
            }
            return View(trainee);
        }

        // GET: Trainees/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Trainees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TraineeID,TraineeName,TraineeDOB,TraineeAge,TraineeEducation,TraineeTOEIC")] Trainee trainee)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Trainees.Add(trainee);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return View(trainee);
        }

        // GET: Trainees/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trainee trainee = db.Trainees.Find(id);
            if (trainee == null)
            {
                return HttpNotFound();
            }
            return View(trainee);
        }

        // POST: Trainees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var TraineeToUpdate = db.Trainees.Find(id);
            if (TryUpdateModel(TraineeToUpdate, "",
               new string[] { "TraineeID,TraineeName,TraineeDOB,TraineeAge,TraineeEducation,TraineeTOEIC" }))
            {
                try
                {
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch (DataException /* dex */)
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }
            return View(TraineeToUpdate);
        }

        // GET: Trainees/Delete/5
        public ActionResult Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Delete failed. Try again, and if the problem persists see your system administrator.";
            }
            Trainee trainee = db.Trainees.Find(id);
            if (trainee == null)
            {
                return HttpNotFound();
            }
            return View(trainee);
        }

        // POST: Trainees/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                Trainee trainee = db.Trainees.Find(id);
                db.Trainees.Remove(trainee);
                db.SaveChanges();
            }
            catch (DataException/* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                return RedirectToAction("Delete", new { id = id, saveChangesError = true });
            }
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
