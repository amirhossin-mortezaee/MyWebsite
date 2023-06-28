using DataLayer.Mapping;
using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Context
{
    public class MyProjectContext : DbContext
    {
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<BlogComment> BlogComments { get; set; }
        public DbSet<BlogGroup> BlogGroups { get; set; }
        public DbSet<Logo> Logo { get; set; }
        public DbSet<Slider> sliders { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Configurations.Add(new BlogConfig());
            modelBuilder.Configurations.Add(new BlogCommentConfig());
        }
    }
}
