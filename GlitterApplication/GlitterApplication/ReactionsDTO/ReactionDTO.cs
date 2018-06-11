using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReactionsDTO
{
    public class ReactionDTO
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public int TweetId { get; set; }
        public Nullable<bool> LikeFlag { get; set; }
    }
}
