using DataLayer.Context;
using DataLayer.Models;
using DataLayer.Models.ViewModels;
using DataLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Services
{
    public class SliderRepository : ISliderRepository
    {
        private MyProjectContext db;
        public SliderRepository(MyProjectContext context)
        {
            this.db = context;
        }
        public IEnumerable<Slider> GetAll()
        {
            var getList = db.sliders.ToList();
            return getList;
        }

        public Slider GetById(int sliderId)
        {
            var getID = db.sliders.Find(sliderId);
            return getID;
        }
        public bool Create(Slider Slider)
        {
            try
            {
                var AddToSlider = db.sliders.Add(Slider);
                return true;
            }
            catch
            {
                throw new Exception("عملیات افزودن اسلایدر با خطا مواجه شد");
            }
        }

        public bool DeleteById(int sliderId)
        {
            try
            {
                var GetId = GetById(sliderId);
                db.sliders.Remove(GetId);
                return true;
            }
            catch
            {
                throw new Exception("عملیات حذف اسلایدر با خطا مواجه شد");
            }
        }


        public bool Edit(Slider Slider)
        {
            try
            {
                db.Entry(Slider).State = EntityState.Modified;
                return true;
            }
            catch
            {
                throw new Exception("عملیات ویرایش اسلایدر با خطا مواجه شد");
            }
        }



        public SliderViewModel GetByIdForVM(int sliderId)
        {
            var getId = db.sliders.Where(x => x.SlideID == sliderId).Select(x => new SliderViewModel()
            {
                SlideID = x.SlideID,
                Title = x.Title,
                ImageName = x.ImageName,
                StartDate = x.StartDate,
                EndDate = x.EndDate,
                IsActive = x.IsActive,
                Url = x.Url
            }).FirstOrDefault();
            return getId;
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
