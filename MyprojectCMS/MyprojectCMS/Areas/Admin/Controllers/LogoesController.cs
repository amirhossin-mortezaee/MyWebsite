using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DataLayer.Context;
using DataLayer.Models;
using DataLayer.Models.ViewModels;
using DataLayer.Repositories;
using DataLayer.Services;

namespace MyProjectCMS.Areas.Admin.Controllers
{
    public class LogoesController : Controller
    {
        private ILogoRepository logoRepository;

        private MyProjectContext db = new MyProjectContext();
        public LogoesController()
        {
            logoRepository = new LogoRepository(db);
        }
        // GET: Admin/Logos
        public ActionResult Index()
        {
            return View(logoRepository.GetAll());
        }

        // GET: Admin/Logos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Logo logo = logoRepository.GetById(id.Value);
            if (logo == null)
            {
                return HttpNotFound();
            }
            return View(logo);
        }

        // GET: Admin/logos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/logos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(LogoViewModel logoVM)
        {
            if (ModelState.IsValid)
            {
                var getcount = logoRepository.CountLogo();
                if (getcount >= 1)
                {
                    ModelState.AddModelError("", "در این بخش از مدیریت اسلایدر تنها می توانید یک تصویر را درج نمایید");
                    return RedirectToAction("Index");
                }

                Logo logo = new Logo()
                {
                    LogoID = logoVM.LogoID,
                    Title = logoVM.Title,
                    ImageName = logoVM.ImageName,
                };
                if (logoVM.ImageUpload != null)
                {
                    logo.ImageName = Guid.NewGuid() + Path.GetExtension(logoVM.ImageUpload.FileName);
                    logoVM.ImageUpload.SaveAs(Server.MapPath("/Images/" + logo.ImageName));
                }


                logoRepository.Create(logo);

                logoRepository.Save();


                return RedirectToAction("Index");
            }

            return View(logoVM);
        }

        // GET: Admin/Logos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LogoViewModel logo = logoRepository.GetByIdForVM(id.Value);
            if (logo == null)
            {
                return HttpNotFound();
            }
            return View(logo);
        }

        // POST: Admin/Logos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(LogoViewModel logoVM)
        {
            if (ModelState.IsValid)
            {
                Logo logo = new Logo()
                {
                    LogoID = logoVM.LogoID,
                    Title = logoVM.Title,
                    ImageName = logoVM.ImageName,

                };

                if (logoVM.ImageUpload != null)
                {
                    if (logo.ImageName != null)
                    {
                        System.IO.File.Delete(Server.MapPath("/Images/" + logo.ImageName)); ;
                    }
                    logo.ImageName = Guid.NewGuid() + Path.GetExtension(logoVM.ImageUpload.FileName);
                    logoVM.ImageUpload.SaveAs(Server.MapPath("/Images/" + logo.ImageName));
                }


                logoRepository.Edit(logo);
                logoRepository.Save();
                return RedirectToAction("Index");
            }
            return View(logoVM);
        }

        // GET: Admin/Logos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Logo logo = logoRepository.GetById(id.Value);
            if (logo == null)
            {
                return HttpNotFound();
            }
            return View(logo);
        }

        // POST: Admin/Logos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Logo logo = logoRepository.GetById(id);
            if (logo.ImageName != null)
            {
                System.IO.File.Delete(Server.MapPath("/Images/" + logo.ImageName));
            }
            logoRepository.DeleteById(id);
            logoRepository.Save();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                logoRepository.Dispose();
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
