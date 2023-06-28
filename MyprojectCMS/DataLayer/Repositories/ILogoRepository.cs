using DataLayer.Models;
using DataLayer.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repositories
{
    public interface ILogoRepository:IDisposable
    {
        IEnumerable<Logo> GetAll();
        Logo GetById(int logoId);
        LogoViewModel GetByIdForVM(int logoId);
        bool Create(Logo logo);
        bool Edit(Logo logo);
        bool DeleteById(int logoId);
        void Save();
        int CountLogo();
    }
}
