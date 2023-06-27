using DataLayer.Models;
using DataLayer.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repositories
{
    public interface ISliderRepository : IDisposable
    {
        IEnumerable<Slider> GetAll();
        Slider GetById(int sliderId);
        SliderViewModel GetByIdForVM(int sliderId);
        bool Create(Slider Slider);
        bool Edit(Slider Slider);
        bool DeleteById(int sliderId);
        void Save();
    }
}
