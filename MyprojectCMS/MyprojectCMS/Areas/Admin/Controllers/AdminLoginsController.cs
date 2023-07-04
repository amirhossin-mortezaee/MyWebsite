using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DataLayer.Context;
using DataLayer.Models;
using DataLayer.Repositories;
using DataLayer.Services;

namespace MyprojectCMS.Areas.Admin.Controllers
{
    public class AdminLoginsController : Controller
    {
        private MyProjectContext db = new MyProjectContext();
        private ILoginRepository LoginRepository;
        public AdminLoginsController()
        {
            LoginRepository = new LoginRepository(db);
        }

        // GET: Admin/AdminLogins
        public ActionResult Index()
        {
            return View(LoginRepository.GetAll());
        }

        // GET: Admin/AdminLogins/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AdminLogins adminLogins = LoginRepository.GetById(id.Value);
            if (adminLogins == null)
            {
                return HttpNotFound();
            }
            return View(adminLogins);
        }

        // GET: Admin/AdminLogins/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/AdminLogins/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LoginId,UserName,Email,Password")] AdminLogins adminLogins)
        {
            if (ModelState.IsValid)
            {
                LoginRepository.Create(adminLogins);
                LoginRepository.save();
                return RedirectToAction("Index");
            }

            return View(adminLogins);
        }

        // GET: Admin/AdminLogins/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AdminLogins adminLogins = LoginRepository.GetById(id.Value);
            if (adminLogins == null)
            {
                return HttpNotFound();
            }
            return View(adminLogins);
        }

        // POST: Admin/AdminLogins/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LoginId,UserName,Email,Password")] AdminLogins adminLogins)
        {
            if (ModelState.IsValid)
            {
                LoginRepository.Edit(adminLogins);
                LoginRepository.save();
                return RedirectToAction("Index");
            }
            return View(adminLogins);
        }

        // GET: Admin/AdminLogins/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AdminLogins adminLogins = LoginRepository.GetById(id.Value);
            if (adminLogins == null)
            {
                return HttpNotFound();
            }
            return View(adminLogins);
        }

        // POST: Admin/AdminLogins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AdminLogins adminLogins = LoginRepository.GetById(id);
            LoginRepository.DeleteById(id);
            LoginRepository.save();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                LoginRepository.Dispose();
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
