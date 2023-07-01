using DataLayer.Context;
using DataLayer.Services;
using DataLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyprojectCMS.Controllers
{
    public class HomeController : Controller
    {
        private ISliderRepository sliderRepository;
        private ILogoRepository LogoRepository;
        private MyProjectContext db = new MyProjectContext();
        public HomeController()
        {
            sliderRepository = new SliderRepository(db);
            LogoRepository = new LogoRepository(db);
        }
        public ActionResult Index()
        {
            int id = 1;
            ViewBag.CourentBlog = id;
            return View();
        }

        public ActionResult Logo()
        {
            return PartialView(LogoRepository.GetAll());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Slider()
        {
            return PartialView(sliderRepository.GetAll());
        }
    }
}