using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel;
using ReactionsDTO;


namespace ReactionAccess
{
    public class ReactionDb
    {
        private GlitterDBEntities db;
        public ReactionDb()
        {
            db = new GlitterDBEntities();
        }

        public void AddTweetReaction(ReactionDTO reactionDTO)
        {
            // first retrieve the user id from the email
            int userId = db.Users.Where(x => x.Email == reactionDTO.Email).Select(x => x.UserId).FirstOrDefault();
            reactionDTO.UserId = userId;
            //check weather the reaction is already present for the given tweet
            var reaction = db.Reactions.Where(x => x.UserId == userId && x.TweetId == reactionDTO.TweetId).FirstOrDefault();
            //if reaction is not present then add a new reaction to that tweet.
            if (reaction == null)
            {
                Reaction reactionObj = new Reaction();
                reactionObj.UserId = userId;
                reactionObj.TweetId = reactionDTO.TweetId;
                reactionObj.LikeFlag = reactionDTO.LikeFlag;
                db.Reactions.Add(reactionObj);
                Save();
            }
            //if reaction is present then change its likeflag status 
            else
            {
                reaction.LikeFlag = reactionDTO.LikeFlag;
                Save();
            }
        }



        public void Save()
        {
            db.SaveChanges();
        }

    }
}
