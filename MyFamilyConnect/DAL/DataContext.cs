using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFamilyConnect.Models
{
    public class DataContext : ApplicationDbContext
    {
        public virtual DbSet<News> News { get; set; }
        public virtual DbSet<Photo> Photos { get; set; }
        public virtual DbSet<UserProfile> UserProfiles { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();            
            //modelBuilder.Entity<Photo>().HasOptional(a => a.Comments).WithOptionalDependent().WillCascadeOnDelete(true);
            //modelBuilder.Entity<News>().HasOptional(a => a.Comments).WithMany().WillCascadeOnDelete(true);           
            
        }
    }
}
