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
        public int CommentId { get; set; }
        public int NewsPhotoItemId { get; set; }
        public string Text { get; set; }
        public int UserProfileId { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
