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

        public IEnumerable<Blog> Search(string searchParamter)
        {
            return db.Blogs.Where(model => model.BlogText.Contains(searchParamter) || model.Tag.Contains(searchParamter) || model.ShortDescription.Contains(searchParamter) || model.Title.Contains(searchParamter)).Distinct();
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

        public IEnumerable<Blog> ArchiveBlog(int id = 1)
        {
            int take = 3;
            int skip = (id - 1) * take;
            return (db.Blogs.OrderBy(model => model.CreateDate).Skip(skip).Take(take).ToList());
        }

        public int CountBlog()
        {
            return (db.Blogs.Count());
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
                Visite = model.Visite,
                Tag = model.Tag
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
