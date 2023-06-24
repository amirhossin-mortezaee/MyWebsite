using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Mapping
{
    public class BlogConfig : EntityTypeConfiguration<Blog>
    {
        public BlogConfig()
        {
            ToTable("Blog", "Blogs");
            HasRequired(item => item.BlogGroup).WithMany(item => item.Blogs).HasForeignKey(item => item.GroupId).WillCascadeOnDelete(false);
        }
    }
}
