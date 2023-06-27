using DataLayer.Context;
using DataLayer.Models;
using DataLayer.Models.ViewModels;
using DataLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Services
{
    public class BlogRepository : IBlogRepository
    {
        MyProjectContext db;
        public BlogRepository(MyProjectContext context)
        {
            this.db = context;
        }
        public IEnumerable<Blog> GetAll()
        {
            var GetList = db.Blogs.ToList();
            return GetList;
        }

        public IEnumerable<Blog> LastBlog(int take = 3)
        {
            var GetLastBlog = db.Blogs.OrderByDescending(item => item.CreateDate).Take(take);
            return GetLastBlog;
        }

        public Blog GetById(int BlogId)
        {
            var GetId = db.Blogs.Find(BlogId);
            return GetId;
        }
        public bool Created(Blog blog)
        {
            try
            {
                var AddtoBlog = db.Blogs.Add(blog);
                return true;
            }
            catch
            {
                throw new Exception("عملیات افزدون وبلاگ  با خطا مواجه شد");
            }
        }

        public bool Edit(Blog blog)
        {
            try
            {
                db.Entry(blog).State = EntityState.Modified;
                return true;
            }
            catch
            {
                throw new Exception("عملیات ویرایش وبلاگ  با خطا مواجه شد");
            }
        }

        public bool DeleteById(int BlogId)
        {
            try
            {
                var GetId = GetById(BlogId);
                db.Blogs.Remove(GetId);
                return true;
            }
            catch
            {
                throw new Exception("عملیات حذف وبلاگ  با خطا مواجه شد");
            }
        }
        public BlogViewModel GetByIdForVM(int blogId)
        {
            var getId = db.Blogs.Where(model => model.BlogId == blogId).Select(model => new BlogViewModel()
            {
                BlogId = model.BlogId,
                GroupId = model.GroupId,
                Title = model.Title,
                ShortDescription = model.ShortDescription,
                BlogText = model.BlogText,
                ImageName = model.ImageName,
                CreateDate = model.CreateDate,
                Visite = model.Visite
            }).FirstOrDefault();
            return getId;
        }
        public void save()
        {
            db.SaveChanges();
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}
