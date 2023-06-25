using DataLayer.Context;
using DataLayer.Models;
using DataLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Services
{
    public class BlogGroupRipository : IBlogGroupRepository
    {
        MyProjectContext db;
        public BlogGroupRipository(MyProjectContext context)
        {
            this.db = context;
        }
        public IEnumerable<BlogGroup> GetAll()
        {
            var list = db.BlogGroups.ToList();
            return list;
        }

        public BlogGroup GetById(int GroupId)
        {
            var GetId = db.BlogGroups.Find(GroupId);
            return GetId;
        }

        public bool Created(BlogGroup blogGroup)
        {
            try
            {
                var AddtoBlogGroup = db.BlogGroups.Add(blogGroup);
                return true;
            }
            catch
            {
                throw new Exception("عملیات افزدون گروه با خطا مواجه شد");
            }
        }

        public bool Edit(BlogGroup blogGroup)
        {
            try
            {
                db.Entry(blogGroup).State = EntityState.Modified;
                return true;
            }
            catch
            {
                throw new Exception("عملیات ویرایش گروه با خطا مواجه شد");
            }
        }

        public bool DeleteById(int GroupId)
        {
            try
            {
                var GetId = GetById(GroupId);
                db.BlogGroups.Remove(GetId);
                return true;
            }
            catch
            {
                throw new Exception("عملیات حذف گروه با خطا مواجه شد");
            }
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
