﻿namespace FollowAccess
{
    using System;
    using System.Linq;
    using DataModel;
    using FollowsDTO;

    public class FollowDb
    {
        private GlitterDBEntities db;
        public FollowDb()
        {
            db = new GlitterDBEntities();
        }

        public Boolean AddFollowing(FollowDTO follower)
        {
            //idetify the userId from the user email
            int userId = db.Users.Where(x => x.Email == follower.Email).Select(x => x.UserId).FirstOrDefault();
            follower.FollowerId = userId;
            Follow follow = new Follow();
            follow.FollowerId = userId;
            follow.FollowingId = follower.FollowingId;
            db.Follows.Add(follow);
            Save();
            return true;
        }


        public Boolean DeleteFollowing(FollowDTO follower)
        {
            Boolean result = true;
            try
            {
                int userId = db.Users.Where(x => x.Email == follower.Email).Select(x => x.UserId).FirstOrDefault();
                follower.FollowerId = userId;
                var followObj = db.Follows.Where(x => x.FollowerId == follower.FollowerId && x.FollowingId == follower.FollowingId).FirstOrDefault();
                if (followObj == null)
                {

                }
                db.Follows.Remove(followObj);
                Save();
            }
            catch (Exception) {
                result = false;       
            }
            return result;
        }


        public void IncreaseFollowing(int userId, int followingId)
        {
            var user = db.Users.Find(userId);
            var following = db.Users.Find(followingId);
            if (user != null && following != null)
            {
                user.FollowingCount = user.FollowingCount + 1;
                following.FollowerCount = following.FollowerCount + 1;
                Save();
            }

        }


        public void DecreaseFollowing(int userId, int followingId)
        {

            var user = db.Users.Find(userId);
            var following = db.Users.Find(followingId);

            if (user != null && following != null)
            {
                user.FollowingCount = user.FollowingCount - 1;
                following.FollowerCount = following.FollowerCount - 1;

                Save();
            }
        }

        public void Save()
        {
            db.SaveChanges();
        }
    }
}
