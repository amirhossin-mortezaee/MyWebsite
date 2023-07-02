using DataLayer.Context;
using DataLayer.Repositories;
using DataLayer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyprojectCMS.Controllers
{
    public class SearchController : Controller
    {
        private IBlogRepository blogRepository;
        MyProjectContext db = new MyProjectContext();
        public SearchController()
        {
            blogRepository = new BlogRepository(db);
        }
        // GET: Search
        public ActionResult Index(string q , int id = 1)
        {
            int take = 3;
            ViewBag.CurentBlog = id;
            ViewBag.BlogCount = blogRepository.CountBlog() / take;
            var SearchQ = blogRepository.Search(q);
            ViewBag.Name = q;
            return View(SearchQ);
        }
    }
}