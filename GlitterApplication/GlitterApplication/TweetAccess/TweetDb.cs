using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel;
using TweetsDTO;
using HashTagTweetMapsDTO;

namespace TweetAccess
{
    public class TweetDb
    {
        private GlitterDBEntities db;
        public TweetDb()
        {
            db = new GlitterDBEntities();
        }

        // get a particular tweet with tweeet id.
        public TweetDTO GetByID(int Id)
        {
            var tweet = db.Tweets.Find(Id);
            TweetDTO tweetDTO = new TweetDTO();
            if (tweet != null)
            {
                tweetDTO.TweetId = tweet.TweetId;
                tweetDTO.UserId = tweet.UserId;
                tweetDTO.TweetContent = tweet.TweetContent;
                tweetDTO.Time = tweet.Time;
            }
            return tweetDTO;
        }

        public TweetDTO AddTweet(TweetDTO tweetDTO)
        {
            int userId = db.Users.Where(x => x.Email == tweetDTO.Email).Select(x => x.UserId).FirstOrDefault();
            tweetDTO.UserId = userId;
            Tweet tweet = new Tweet();

            tweet.TweetContent = tweetDTO.TweetContent;
            tweet.UserId = tweetDTO.UserId;
            tweet.Time = DateTime.Now;
            db.Tweets.Add(tweet);
            Save();
            var tweetObj = db.Tweets.OrderByDescending(x => x.Time).First();

            var tweetUser = db.Users.Where(x => x.UserId == tweetObj.UserId).FirstOrDefault();


            TweetDTO OutTweetDTO = new TweetDTO();
            OutTweetDTO.TweetContent = tweetObj.TweetContent;
            OutTweetDTO.TweetId = tweetObj.TweetId;
            OutTweetDTO.UserId = tweetObj.UserId;
            OutTweetDTO.Time = tweetObj.Time;

            OutTweetDTO.Name = tweetUser.Name;
            OutTweetDTO.ImagePath = tweetUser.ProfileImage;
            return OutTweetDTO;
        }

        

        public int AddHashTag(string hashTagContent)
        {
            int hashTagId = 0;
            var hashTag = db.HashTags.Where(x => x.HashTagContent == hashTagContent).FirstOrDefault();
            if (hashTag != null)
            {
                hashTag.Count = hashTag.Count + 1;
                hashTagId = hashTag.HashTagId;
                Save();
            }
            else
            {
                HashTag hashTagObj = new HashTag();
                hashTagObj.HashTagContent = hashTagContent;
                hashTagObj.Count = 1;
                db.HashTags.Add(hashTagObj);
                Save();

                hashTagId = db.HashTags.Where(x => x.HashTagContent == hashTagContent).FirstOrDefault().HashTagId;
            }
            return hashTagId;

        }


        public void AddTagTweetMapping(HashTagTweetMapDTO hashTagTweetMapDTO)
        {
            HashTagTweetMap hashTagTweetMap = new HashTagTweetMap();
            hashTagTweetMap.TweetId = hashTagTweetMapDTO.TweetId;
            hashTagTweetMap.HashTagId = hashTagTweetMapDTO.HashTagId;
            db.HashTagTweetMaps.Add(hashTagTweetMap);
            Save();
        }


        // Tweets are searched on the basis of the hashtags
        public IList<TweetDTO> GetSearchedTweets(string search)
        {
            IList<TweetDTO> tweets = new List<TweetDTO>();
            try
            {
                int hashTagId = 0;
                hashTagId = db.HashTags.Where(x => x.HashTagContent.Contains(search)).Select(x => x.HashTagId).FirstOrDefault();

                var tweetIds = db.HashTagTweetMaps.Where(x => x.HashTagId == hashTagId).Select(x => x.TweetId).ToList();

                //get the corrosponding tweets

                foreach (int tweetId in tweetIds)
                {
                    TweetDTO tweetObj = new TweetDTO();
                    var tweet = db.Tweets.Where(x => x.TweetId == tweetId).FirstOrDefault();
                    var tweetUser = db.Users.Where(x => x.UserId == tweet.UserId).FirstOrDefault();
                    tweetObj.TweetId = tweet.TweetId;
                    tweetObj.TweetContent = tweet.TweetContent;
                    tweetObj.UserId = tweet.UserId;
                    tweetObj.Time = tweet.Time;
                    tweetObj.Name = tweetUser.Name;
                    tweetObj.ImagePath = tweetUser.ProfileImage;
                    tweets.Add(tweetObj);
                }
                return tweets;

            }
            catch (Exception e)
            {
                tweets = null;
                return tweets;
            }

        }


        //used to delete a particular tweet from the database
        public void Delete(int Id)
        {
            var tweet = db.Tweets.Find(Id);
            db.Tweets.Remove(tweet);
            Save();
        }


        public void Save()
        {
            db.SaveChanges();
        }

    }
}
