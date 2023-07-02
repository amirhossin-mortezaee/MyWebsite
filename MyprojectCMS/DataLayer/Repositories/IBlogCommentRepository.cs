using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repositories
{
    public interface IBlogCommentRepository : IDisposable
    {
        IEnumerable<BlogComment> getAll();
        IEnumerable<BlogComment> getCommmentByBlogId(int blogId);
        BlogComment GetById(int commentID);
        bool Create(BlogComment comment);
        bool DeleteByID(int commentID);
        void Save();
    }
}
