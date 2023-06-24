using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repositories
{
    public interface IBlogGroupRepository : IDisposable
    {
        IEnumerable<BlogGroup> GetAll();
        BlogGroup GetById(int GroupId);
        bool Created(BlogGroup blogGroup);
        bool Edit(BlogGroup blogGroup);
        bool DeleteById(int GroupId);
        void save();
    }
}
