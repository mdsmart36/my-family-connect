using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MyFamilyConnect.Models
{
    public class Photo
    {        
        [Key]
        public int PhotoId { get; set; }

        public string Title { get; set; }
        public string Text { get; set; }       
        public byte[] Content { get; set; }
        [Display(Name ="Date")]
        public DateTime TimeStamp { get; set; }

        // Foreign keys        


        // Navigation properties
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual UserProfile UserProfile { get; set; }
    }
}
