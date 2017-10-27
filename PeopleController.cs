using System.Linq;
using System.Net;
using System.Web.Mvc;
using Iam.Payroll.Services;
using Iam.Payroll.Common;
using Iam.Payroll.Site.Models;
using Microsoft.AspNet.Identity;
namespace Iam.Payroll.Site.Controllers
{
    public class PeopleController : Controller
    {
        readonly PersonService svcP;
        readonly DepartmentService svcD;

        public PeopleController(PersonService svcP, DepartmentService svcD)
        {
            this.svcP = svcP;
            this.svcD = svcD;
        }

        // GET: People
        public ActionResult Index()
        {
            var person = svcP.GetAll();
            return View(person.ToList());
        }

        // GET: People/Details/5
        public ActionResult Details(int? id)
        {
            var person = svcP.Get(id.Value);
            return View(person);
        }

        // GET: People/Create
        public ActionResult Create()
        {
            var aDepartments = svcD.GetAll();
            ViewBag.DepartmentId = new SelectList(aDepartments, "Id", "Name");
            return View();
        }

        // POST: People/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FirstName,LastName,Gender,DepartmentId")] PersonEx person)
        {
            var aDepartments = svcD.GetAll();
            svcP.Save(person);
            ViewBag.DepartmentId = new SelectList(aDepartments, "Id", "Name", person.DepartmentId);
            return RedirectToAction("Index");
        }

        // GET: People/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PersonEx person = svcP.Get(id.GetValueOrDefault(0));
            if (person == null)
            {
                return HttpNotFound();
            }
            var aDepartment = svcD.GetAll();
            ViewBag.DepartmentId = new SelectList(aDepartment, "Id", "Name", person.DepartmentId);
            return View(person);
        }

        // POST: People/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FirstName,LastName,Gender,DepartmentId")] PersonEx person)
        {
            var aDepartment = svcD.GetAll();
            ViewBag.DepartmentId = new SelectList(aDepartment, "Id", "Name", person.DepartmentId);
            svcP.Save(person);
            return RedirectToAction("Index");
        }

        // GET: People/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PersonEx person = svcP.Get(id.GetValueOrDefault(0));
            if (person == null)
            {
                return HttpNotFound();
            }
            return View(person);
        }

        // POST: People/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PersonEx person = svcP.Get(id);
            svcP.Delete(person);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {

            }
            base.Dispose(disposing);
        }
    }
}
