using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFamilyConnect.Models
{
    public class Comment
    {
        [Key]
        public int CommentId { get; set; }        
        public string Text { get; set; }
        [Display(Name ="Date")]
        public DateTime TimeStamp { get; set; }

        // Foreign keys        

        // Navigation properties
        public virtual News NewsItem { get; set; }
        public virtual Photo PhotoItem { get; set; }
        public virtual UserProfile UserProfile { get; set; }        
    }
}
