using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFamilyConnect.Models
{
    public class UserProfile
    {
        [Key]
        public int UserProfileId { get; set; }
        public ApplicationUser Owner { get; set; }
        [Display(Name ="First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        public DateTime? Birthdate { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public string Email { get; set; }
        public string AboutMe { get; set; }

        // Navigation properties
        public virtual ICollection<News> NewsItems { get; set; }
        public virtual ICollection<Photo> PhotoItems { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
                
    }
}
