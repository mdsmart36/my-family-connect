using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyFamilyConnect.Models
{
    public class News
    {
        [Key]
        public int NewsId { get; set; }

        public string Title { get; set; }
        public string Text { get; set; }
        [Display(Name ="Date")]
        public DateTime TimeStamp { get; set; }

        // Foreign keys        
        
        
        // Navigation properties        
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual UserProfile UserProfile { get; set; }
    }
}
