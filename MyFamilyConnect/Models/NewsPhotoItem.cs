using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFamilyConnect.Models
{
    public class NewsPhotoItem
    {
        public int NewsPhotoId { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public bool HasPhoto { get; set; }
        public Image Photo { get; set; }
        public int UserProfileId { get; set; }
        public DateTime TimeStamp { get; set; }
        public virtual List<Comment> Comments { get; set; }
    }
}
