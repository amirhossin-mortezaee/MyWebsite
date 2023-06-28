using DataLayer.Context;
using DataLayer.Models;
using System.Data.Entity;
using DataLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Services
{
    public class BlogCommentRepository : IBlogCommentRepository
    {
        private MyProjectContext db;
        public BlogCommentRepository(MyProjectContext context)
        {
            this.db = context;
        }

        public IEnumerable<BlogComment> getAll()
        {
            var blogComments = db.BlogComments.Include(item => item.Blog).ToList();
            return blogComments;
        }

        public BlogComment GetById(int commentID)
        {
            var GetID = db.BlogComments.Find(commentID);
            return GetID;
        }

        public bool Create(BlogComment comment)
        {
            try
            {
                var AddToComment = db.BlogComments.Add(comment);
                return true;
            }
            catch
            {
                throw new Exception("عملیات افزودن کامنت با خطا مواجه شد");
            }
        }

        public bool DeleteByID(int commentID)
        {
            try
            {
                BlogComment GetId = GetById(commentID);
                db.BlogComments.Remove(GetId);
                return true;
            }
            catch
            {
                throw new Exception("عملیات حذف کامنت با خطا مواجه شد");
            }
            
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public void Dispose()
        {
            db.Dispose();
        }

       

        

        
    }
}
