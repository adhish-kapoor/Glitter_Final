using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsersDTO
{
    public class UserDTO
    {      
        [Key]
        public int UserId { get; set; }
        [Required]        
        public string ContactNumber { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string ProfileImage { get; set; }
        [Required]
        public string Name { get; set; }
        public Nullable<int> FollowingCount { get; set; }
        public Nullable<int> FollowerCount { get; set; }
    }
}
