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

namespace MyprojectCMS.Areas.Admin.Controllers
{
    public class BlogsController : Controller
    {
        private IBlogRepository blogRepository;
        private IBlogGroupRepository blogGroupRepository;
        private MyProjectContext db = new MyProjectContext();
        public BlogsController()
        {
            blogRepository = new BlogRepository(db);
            blogGroupRepository = new BlogGroupRipository(db);
        }
        // GET: Admin/Blogs
        public ActionResult Index()
        {
            var GetList = blogRepository.GetAll();
            return View(GetList.ToList());
        }

        // GET: Admin/Blogs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Blog blog = blogRepository.GetById(id.Value);
            if (blog == null)
            {
                return HttpNotFound();
            }
            return View(blog);
        }

        // GET: Admin/Blogs/Create
        public ActionResult Create()
        {
            ViewBag.GroupId = new SelectList(blogGroupRepository.GetAll(), "GroupId", "GroupTitle");
            return View();
        }

        // POST: Admin/Blogs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BlogViewModel blogVM)
        {
            if (ModelState.IsValid)
            {
                Blog blog = new Blog()
                {
                    BlogId = blogVM.BlogId,
                    GroupId = blogVM.GroupId,
                    Title = blogVM.Title,
                    ShortDescription = blogVM.ShortDescription,
                    BlogText = blogVM.BlogText,
                    ImageName = blogVM.ImageName,
                    CreateDate = blogVM.CreateDate,
                    Visite = blogVM.Visite
                };
                blog.Visite = 0;
                blog.CreateDate = DateTime.Now;
                if (blogVM.ImageUpload != null)
                {
                    blog.ImageName = Guid.NewGuid()+Path.GetExtension(blogVM.ImageUpload.FileName);
                    blogVM.ImageUpload.SaveAs(Server.MapPath("/Images/" + blog.ImageName));
                }


                blogRepository.Created(blog);
                blogRepository.save();
                return RedirectToAction("Index");
            }

            ViewBag.GroupId = new SelectList(db.BlogGroups, "GroupId", "GroupTitle", blogVM.GroupId);
            return View(blogVM);
        }

        // GET: Admin/Blogs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Blog blog = db.Blogs.Find(id);
            if (blog == null)
            {
                return HttpNotFound();
            }
            ViewBag.GroupId = new SelectList(db.BlogGroups, "GroupId", "GroupTitle", blog.GroupId);
            return View(blog);
        }

        // POST: Admin/Blogs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BlogId,GroupId,Title,ShortDescription,BlogText,Visite,ImageName,CreateDate")] Blog blog)
        {
            if (ModelState.IsValid)
            {
                db.Entry(blog).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GroupId = new SelectList(db.BlogGroups, "GroupId", "GroupTitle", blog.GroupId);
            return View(blog);
        }

        // GET: Admin/Blogs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Blog blog = db.Blogs.Find(id);
            if (blog == null)
            {
                return HttpNotFound();
            }
            return View(blog);
        }

        // POST: Admin/Blogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Blog blog = db.Blogs.Find(id);
            db.Blogs.Remove(blog);
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
