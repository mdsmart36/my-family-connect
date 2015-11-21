using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFamilyConnect.Models
{
    public class DataContext : DbContext
    {
        public virtual DbSet<NewsPhotoItem> NewsAndPhotos { get; set; }
        public virtual IDbSet<Comment> Comments { get; set; }
    }
}
