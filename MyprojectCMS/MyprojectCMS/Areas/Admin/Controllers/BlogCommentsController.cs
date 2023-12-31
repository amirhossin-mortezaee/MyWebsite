﻿using System;
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
    public class BlogCommentsController : Controller
    {
        private IBlogCommentRepository blogCommentRepository;
        private IBlogRepository blogRepository;
        private MyProjectContext db = new MyProjectContext();
        public BlogCommentsController()
        {
            blogCommentRepository = new BlogCommentRepository(db);
            blogRepository = new BlogRepository(db);
        }

        // GET: Admin/BlogComments
        public ActionResult Index()
        {
            var blogComments = blogCommentRepository.getAll();
            return View(blogComments);
        }

        // GET: Admin/BlogComments/Create
        public ActionResult Create()
        {
            ViewBag.BlogId = new SelectList(blogRepository.GetAll(), "BlogId", "Title");
            return View();
        }

        // POST: Admin/BlogComments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CommentId,BlogId,Name,Email,Website,Comment,CreateDate")] BlogComment blogComment)
        {
            if (ModelState.IsValid)
            {
                blogComment.CreateDate = DateTime.Now;
                blogCommentRepository.Create(blogComment);
                blogCommentRepository.Save();
                return RedirectToAction("Index");
            }
            ViewBag.BlogId = new SelectList(blogRepository.GetAll(), "BlogId", "Title", blogComment.BlogId);
            return View();
        }

        // GET: Admin/BlogComments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogComment blogComment = blogCommentRepository.GetById(id.Value);
            if (blogComment == null)
            {
                return HttpNotFound();
            }
            return View(blogComment);
        }

        // POST: Admin/BlogComments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BlogComment blogComment = blogCommentRepository.GetById(id);
            blogCommentRepository.DeleteByID(id);
            blogCommentRepository.Save();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                blogCommentRepository.Dispose();
                blogRepository.Dispose();
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
