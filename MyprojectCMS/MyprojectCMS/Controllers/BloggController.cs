﻿using DataLayer.Context;
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
    }
}