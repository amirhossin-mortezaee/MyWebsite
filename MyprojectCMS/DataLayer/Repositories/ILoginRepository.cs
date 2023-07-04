using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repositories
{
    public interface ILoginRepository : IDisposable
    {
        IEnumerable<AdminLogins> GetAll();
        bool Create(AdminLogins login);
        AdminLogins GetById(int LoginId);
        bool Edit(AdminLogins login);
        bool DeleteById(int LoginId);
        void save();
        bool IsExistUser(string username,string Password);
    }
}
