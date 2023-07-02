using DataLayer.Models;
using DataLayer.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repositories
{
    public interface IBlogRepository : IDisposable
    {
        IEnumerable<Blog> GetAll();
        IEnumerable<Blog> Search(string searchParamter);
        IEnumerable<Blog> LastBlog(int take = 3);
        IEnumerable<Blog> ArchiveBlog(int id = 1);
        int CountBlog();
        Blog GetById(int BlogId);
        BlogViewModel GetByIdForVM(int BlogId);
        bool Created(Blog blog);
        bool Edit(Blog blog);
        bool DeleteById(int BlogId);
        void save();
    }
}
