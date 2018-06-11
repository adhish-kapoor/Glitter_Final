using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TweetsDTO
{
    public class TweetDTO
    {
            [key]
            public int TweetId { get; set; }
            public string TweetContent { get; set; }
            public int UserId { get; set; }
            public DateTime Time { get; set; }
            public string Name { get; set; }
            public string ImagePath { get; set; }
            public string Email { get; set; }
            public bool? LikeFlag { get; set; }

    }
}
