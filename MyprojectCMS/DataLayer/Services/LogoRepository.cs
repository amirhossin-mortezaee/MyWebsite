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
    public class LogoRepository : ILogoRepository
    {
        private MyProjectContext db;
        public LogoRepository(MyProjectContext context)
        {
            this.db = context;
        }
        public IEnumerable<Logo> GetAll()
        {
            var getList = db.Logo.ToList();
            return getList;
        }

        public Logo GetById(int logoId)
        {
            var getID = db.Logo.Find(logoId);
            return getID;
        }
        public bool Create(Logo logo)
        {
            try
            {
                var AddToSlider = db.Logo.Add(logo);
                return true;
            }
            catch
            {
                throw new Exception("عملیات افزودن لوگو با خطا مواجه شد");
            }
        }

        public bool DeleteById(int logoId)
        {
            try
            {
                var GetId = GetById(logoId);
                db.Logo.Remove(GetId);
                return true;
            }
            catch
            {
                throw new Exception("عملیات حذف لوگو با خطا مواجه شد");
            }
        }


        public bool Edit(Logo logo)
        {
            try
            {
                db.Entry(logo).State = EntityState.Modified;
                return true;
            }
            catch
            {
                throw new Exception("عملیات ویرایش لوگو با خطا مواجه شد");
            }
        }



        public LogoViewModel GetByIdForVM(int logoId)
        {
            var getId = db.Logo.Where(x => x.LogoID == logoId).Select(x => new LogoViewModel()
            {
                LogoID = x.LogoID,
                Title = x.Title,
                ImageName = x.ImageName,
            }).FirstOrDefault();
            return getId;
        }
        public int CountLogo()
        {
            var getCount = db.Logo.Count();
            return getCount;
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
