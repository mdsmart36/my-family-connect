using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFamilyConnect.Models
{
    public class Comment
    {
        public int CommentId { get; set; }
        public string Text { get; set; }
        public ApplicationUser Owner { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
