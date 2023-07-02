using DataLayer.Context;
using DataLayer.Models;
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
        private IBlogRepository blogRepository;
        private IBlogCommentRepository blogCommentRepository;
       private MyProjectContext db = new MyProjectContext();
        public BloggController()
        {
            blogRepository = new BlogRepository(db);
            blogCommentRepository = new BlogCommentRepository(db);
        }
        // GET: Blogg
        public ActionResult lastBlogs()
        {
            return PartialView(blogRepository.LastBlog());
        }

        public ActionResult leftCornerlastBlogs()
        {
            return PartialView(blogRepository.LastBlog());
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

        public ActionResult AddComment(int id,string name , string email , string comment)
        {
            BlogComment addComment = new BlogComment()
            {
                CreateDate = DateTime.Now,
                BlogId = id,
                Name = name,
                Email = email,
                Comment = comment
            };
            blogCommentRepository.Create(addComment);
            blogCommentRepository.Save();
            return PartialView("showComment",blogCommentRepository.getCommmentByBlogId(id));
        }

        public ActionResult showComment(int id)
        {
            return PartialView(blogCommentRepository.getCommmentByBlogId(id));
        }
    }
}