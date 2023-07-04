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
    public class LoginRepository : ILoginRepository
    {
        private MyProjectContext db;
        public LoginRepository(MyProjectContext context)
        {
            this.db = context;
        }

        public IEnumerable<AdminLogins> GetAll()
        {
            var GetList = db.AdminLogin.ToList();
            return GetList;
        }
        public AdminLogins GetById(int LoginId)
        {
            var GetId = db.AdminLogin.Find(LoginId);
            return GetId;
        }
        public bool Create(AdminLogins login)
        {
            try
            {
                var Add = db.AdminLogin.Add(login);
                return true;
            }
            catch
            {
                throw new Exception("عملیات افزودن کاربر  با خطا مواجه شد");
            }
        }

        public bool Edit(AdminLogins login)
        {
            try
            {
                db.Entry(login).State = EntityState.Modified;
                return true;
            }
            catch
            {
                throw new Exception("عملیات ویرایش کاربر  با خطا مواجه شد");
            }
        }

        public bool DeleteById(int LoginId)
        {
            try
            {
                var GetId = GetById(LoginId);
                db.AdminLogin.Remove(GetId);
                return true;
            }
            catch
            {
                throw new Exception("عملیات حذف کاربر  با خطا مواجه شد");
            }
        }

        public bool IsExistUser(string username, string Password)
        {
           return db.AdminLogin.Any(model => model.UserName == username && model.Password == Password);
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
