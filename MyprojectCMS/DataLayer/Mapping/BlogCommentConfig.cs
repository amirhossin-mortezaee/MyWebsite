using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Mapping
{
    public class BlogCommentConfig : EntityTypeConfiguration<BlogComment>
    {
        public BlogCommentConfig()
        {
            ToTable("BlogComment", "Blogs");
            HasRequired(item => item.Blog).WithMany(item => item.BlogComments).HasForeignKey(item => item.BlogId).WillCascadeOnDelete(false);
        }
    }
}
