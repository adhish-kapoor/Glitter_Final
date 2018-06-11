using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FollowsDTO
{
    public class FollowDTO
    {
        public int FollowerId { get; set; }
        public int FollowingId { get; set; }
        public string Email  { get; set; }
    }
}
