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
    public class SlidersController : Controller
    {
        private SliderRepository sliderRepository;

        private MyProjectContext db = new MyProjectContext();
        public SlidersController()
        {
            sliderRepository = new SliderRepository(db);
        }
        // GET: Admin/Sliders
        public ActionResult Index()
        {
            return View(sliderRepository.GetAll());
        }

        // GET: Admin/Sliders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Slider slider = sliderRepository.GetById(id.Value);
            if (slider == null)
            {
                return HttpNotFound();
            }
            return View(slider);
        }

        // GET: Admin/Sliders/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Sliders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SliderViewModel sliderVM)
        {
            if (ModelState.IsValid)
            {

                Slider slider = new Slider()
                {
                    SlideID = sliderVM.SlideID,
                    Title = sliderVM.Title,
                    ImageName = sliderVM.ImageName,
                    StartDate = sliderVM.StartDate,
                    EndDate = sliderVM.EndDate,
                    IsActive = sliderVM.IsActive,
                    Url = sliderVM.Url
                };
                if (sliderVM.ImageUpload != null)
                {
                    slider.ImageName = Guid.NewGuid() + Path.GetExtension(sliderVM.ImageUpload.FileName);
                    sliderVM.ImageUpload.SaveAs(Server.MapPath("/Images/Sliders/" + slider.ImageName));
                }


                sliderRepository.Create(slider);

                sliderRepository.Save();


                return RedirectToAction("Index");
            }

            return View(sliderVM);
        }

        // GET: Admin/Sliders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SliderViewModel slider = sliderRepository.GetByIdForVM(id.Value);
            if (slider == null)
            {
                return HttpNotFound();
            }
            return View(slider);
        }

        // POST: Admin/Sliders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SliderViewModel sliderVM)
        {
            if (ModelState.IsValid)
            {
                Slider slider = new Slider()
                {
                    SlideID = sliderVM.SlideID,
                    Title = sliderVM.Title,
                    ImageName = sliderVM.ImageName,
                    StartDate = sliderVM.StartDate,
                    EndDate = sliderVM.EndDate,
                    IsActive = sliderVM.IsActive,
                    Url = sliderVM.Url
                };

                if (sliderVM.ImageUpload != null)
                {
                    if (slider.ImageName != null)
                    {
                        System.IO.File.Delete(Server.MapPath("/Images/Sliders/" + slider.ImageName)); ;
                    }
                    slider.ImageName = Guid.NewGuid() + Path.GetExtension(sliderVM.ImageUpload.FileName);
                    sliderVM.ImageUpload.SaveAs(Server.MapPath("/Images/Sliders/" + slider.ImageName));
                }


                sliderRepository.Edit(slider);
                sliderRepository.Save();
                return RedirectToAction("Index");
            }
            return View(sliderVM);
        }

        // GET: Admin/Sliders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Slider slider = sliderRepository.GetById(id.Value);
            if (slider == null)
            {
                return HttpNotFound();
            }
            return View(slider);
        }

        // POST: Admin/Sliders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Slider slider = sliderRepository.GetById(id);
            if (slider.ImageName != null)
            {
                System.IO.File.Delete(Server.MapPath("/Images/Sliders/" + slider.ImageName));
            }
            sliderRepository.DeleteById(id);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                sliderRepository.Dispose();
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
