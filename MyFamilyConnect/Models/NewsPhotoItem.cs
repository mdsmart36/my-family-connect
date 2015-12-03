using System;
using System.Collections.Generic;
//using System.Drawing;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

namespace MyFamilyConnect.Models
{
    public class NewsPhotoItem
    {
        // Primary key
        public int NewsPhotoItemId { get; set; }

        public string Title { get; set; }
        public string Text { get; set; }
        public bool HasPhoto { get; set; }
        public byte[] Photo { get; set; }        
        public DateTime TimeStamp { get; set; }

        // Foreign key        
        public int UserProfileId { get; set; }
        
        // Navigation properties
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual UserProfile UserProfile { get; set; }
    }
}
