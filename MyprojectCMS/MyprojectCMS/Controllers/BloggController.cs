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
    public class BloggController : Controller
    {
        IBlogRepository blogRepository;
        MyProjectContext db = new MyProjectContext();
        public BloggController()
        {
            blogRepository = new BlogRepository(db);
        }
        // GET: Blogg
        public ActionResult lastBlogs()
        {
            return PartialView(blogRepository.GetAll());
        }

        [Route("Archive/{id}")]

        public ActionResult Archive(int id = 1)
        {
            int take = 3;
            ViewBag.CourentBlog = id;
            ViewBag.BlogCount = blogRepository.CountBlog() / take;
            var GetArchive = blogRepository.ArchiveBlog(id);
            return View(GetArchive);
        }

        [Route("Blogs/{id}")]
        public ActionResult ShowBlogs(int id)
        {
            var blogs = blogRepository.GetById(id);
            if (blogs == null)
            {
                return HttpNotFound();
            }
            blogs.Visite += 1;
            blogRepository.Edit(blogs);
            blogRepository.save();
            return View(blogs);

        }
    }
}