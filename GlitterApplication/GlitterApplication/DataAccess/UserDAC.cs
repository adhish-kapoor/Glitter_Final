using DataModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TweetsDTO;
using UsersDTO;

namespace DataAccess
{
    public class UserDAC
    {
        public UserDTO GetUser()
        {
            using (GlitterDBEntities context = new GlitterDBEntities())
            {
                var user = (from u in context.Users
                            select u).First();

                UserDTO userDTO = new UserDTO
                {
                    Name = user.Name,
                    ContactNumber = user.ContactNumber,
                    ProfileImage = user.ProfileImage,
                    Email = user.Email,
                    Country = user.Country,
                    FollowerCount = user.FollowerCount,
                    FollowingCount = user.FollowingCount
                };
                return userDTO;
            }
        }

        public Boolean RegisterUserToDatabase(UserDTO user)
        {
            Boolean result;
            try
            {
                using (GlitterDBEntities context = new GlitterDBEntities())
                {
                    User userDTO = new User
                    {
                        Name = user.Name,
                        ContactNumber = user.ContactNumber,
                        ProfileImage = user.ProfileImage,
                        Email = user.Email,
                        Country = user.Country,
                        FollowerCount = 0,
                        FollowingCount = 0
                    };
                    context.Users.Add(userDTO);
                    if (context.SaveChanges() > 0)
                    {
                        result = false;
                    }
                    result = true;
                }
            }
            catch (Exception e)
            {
                result = false;
            }
            return result;
        }

        // get all tweets        
        public IList<TweetDTO> GetUserTweets(string email)
        {
            using (GlitterDBEntities context = new GlitterDBEntities())
            {
                int id = context.Users.Where(x => x.Email == email).FirstOrDefault().UserId;

                IList<TweetDTO> tweets = new List<TweetDTO>();

                var followersIds = context.Follows.Where(x => x.FollowerId == id)
                        .Select(x => x.FollowingId).ToList();
                followersIds.Add(id);


                foreach (var i in followersIds)
                {
                    var result = context.Tweets.Where(x => x.UserId == i).ToList();


                    foreach (var item in result)
                    {
                        var likeFlag = context.Reactions.Where(x => x.TweetId == item.TweetId && x.UserId == id).Select(x => x.LikeFlag).FirstOrDefault();
                        TweetDTO tweet = new TweetDTO();
                        tweet.TweetId = item.TweetId;
                        tweet.TweetContent = item.TweetContent;
                        tweet.UserId = item.UserId;
                        tweet.Time = item.Time;
                        if (i == id)
                        {
                            tweet.Email = email;
                        }
                        else
                        {
                            tweet.Email = "dummy@gmail.com";
                        }
                        tweet.LikeFlag = likeFlag;
                        tweets.Add(tweet);
                    }
                }
                //IList<TweetDTO> sortedTweets = tweets.OrderByDescending(o => o.Time).ToList();
                IList<TweetDTO> sortedTweets = tweets.OrderByDescending(o => o.Time).ToList();
                return sortedTweets;
            }
        }  // method closing

        // get user's follwers count
        public int? GetUserFollowersCount(string email)
        {
            using (GlitterDBEntities context = new GlitterDBEntities())
            {
                int? result = context.Users.Where(x => x.Email == email).FirstOrDefault().FollowerCount;
                return result;
            }
        }


        // get user's follwings count
        public int? GetUserFollowingsCount(string email)
        {
            using (GlitterDBEntities context = new GlitterDBEntities())
            {
                int? result = context.Users.Where(x => x.Email == email).FirstOrDefault().FollowingCount;
                return result;
            }
        }



        // used to search people based on the search string passed
        public IList<UserDTO> GetUsers(string search)
        {

            using (GlitterDBEntities context = new GlitterDBEntities())
            {
                var users = context.Users.Where(x => x.Name.Contains(search) ||
                            x.Email.Contains(search)).ToList();

                IList<UserDTO> usersDTO = new List<UserDTO>();

                foreach (var user in users)
                {
                    UserDTO userDTO = new UserDTO();
                    userDTO.Name = user.Name;
                    userDTO.ContactNumber = user.ContactNumber;
                    userDTO.Country = user.Country;
                    userDTO.Email = user.Email;
                    userDTO.ProfileImage = user.ProfileImage;
                    userDTO.FollowerCount = user.FollowerCount;
                    userDTO.FollowingCount = user.FollowingCount;
                    userDTO.UserId = user.UserId;
                    usersDTO.Add(userDTO);
                }
                return usersDTO;
            }
        }


        //used to retrieve user followings
        public IList<UserDTO> GetUserFollowings(string email)
        {
            using (GlitterDBEntities context = new GlitterDBEntities())
            {
                int userId = context.Users.Where(x => x.Email == email).Select(x => x.UserId).FirstOrDefault();


                var users = context.Follows.Where(x => x.FollowerId == userId)
                              .Include(x => x.User1).Select(y => y.User1).ToList();

                IList<UserDTO> usersDTO = new List<UserDTO>();

                foreach (var user in users)
                {
                    UserDTO userDTO = new UserDTO();
                    userDTO.UserId = user.UserId;
                    userDTO.Name = user.Name;
                    userDTO.ContactNumber = user.ContactNumber;
                    userDTO.Country = user.Country;
                    userDTO.Email = user.Email;
                    userDTO.ProfileImage = user.ProfileImage;
                    userDTO.FollowerCount = user.FollowerCount;
                    userDTO.FollowingCount = user.FollowingCount;
                    usersDTO.Add(userDTO);
                }
                return usersDTO;
            }
        }



        // used to retrieve the follwers of the logged in user
        public IList<UserDTO> GetUserFollowers(string email)
        {

            using (GlitterDBEntities context = new GlitterDBEntities())
            {
                int userId = context.Users.Where(x => x.Email == email).Select(x => x.UserId).FirstOrDefault();

                var users = context.Follows.Where(x => x.FollowingId == userId)
                                   .Include(x => x.User).Select(y => y.User).ToList();

                IList<UserDTO> usersDTO = new List<UserDTO>();

                foreach (var user in users)
                {
                    UserDTO userDTO = new UserDTO();
                    userDTO.UserId = user.UserId;
                    userDTO.Name = user.Name;
                    userDTO.ContactNumber = user.ContactNumber;
                    userDTO.Country = user.Country;
                    userDTO.Email = user.Email;
                    userDTO.ProfileImage = user.ProfileImage;
                    userDTO.FollowerCount = user.FollowerCount;
                    userDTO.FollowingCount = user.FollowingCount;
                    usersDTO.Add(userDTO);
                }
                return usersDTO;
            }
        }
    }
}