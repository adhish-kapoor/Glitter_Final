using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using UserBusinessLogic;
using UsersDTO;

namespace GlitterApplication.Controllers
{
    //[Authorize]
    [AllowCrossSiteJson]
    public class UsersController : ApiController
    {
        UserBL userBL = new UserBL();
                
        [HttpPost]
        // custom register
        public Boolean Register(UserDTO user)
        {
            return userBL.RegisterUser(user);
        }


        //Tweets of the logged in user 
        [System.Web.Http.Route("api/users/{email}/tweets")]
        public IHttpActionResult GetUserTweets(string email)
        {
            if (email != "")
            {
                return Ok(userBL.GetAllTweets(email));
            }
            else
            {
                return NotFound();
            }

        }


        //Followers count of the logged in user
        [System.Web.Http.Route("api/users/{email}/getfollowerscount")]
        public IHttpActionResult GetUserFollowersCount(string email)
        {
            int? result = userBL.GetFollowersCount(email);
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }


        //Following count of the the logged in user
        [System.Web.Http.Route("api/users/{email}/getfollowingscount")]
        public IHttpActionResult GetUserFollowingsCount(string email)
        {

            int? result = userBL.GetFollowingsCount(email);
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }

        //Search people
        [System.Web.Mvc.HttpGet]
        [System.Web.Http.Route("api/users")]
        public IHttpActionResult GetUsers([FromUri]string search)
        {
            var result = userBL.GetSearchedUsers(search);
            if (result != null)
            {
                return Ok(result);
            }
            else {
                return NotFound();
            }
        }


        //Logged in user followings
        [System.Web.Mvc.HttpGet]
        [System.Web.Http.Route("api/users/{email}/followings")]
        public IHttpActionResult GetUserFollowings([FromUri]string email)
        {
            var result = userBL.GetUserFollowings(email);
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }


        //Logged in user followers
        [System.Web.Mvc.HttpGet]
        [System.Web.Http.Route("api/users/{email}/followers")]
        public IHttpActionResult GetUserFollowers([FromUri]string email)
        {
            var result = userBL.GetUserFollowers(email);
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }




    }
}