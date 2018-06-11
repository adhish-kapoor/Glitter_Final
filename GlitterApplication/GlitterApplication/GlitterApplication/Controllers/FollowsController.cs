using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FollowBusinessLogic;
using FollowsDTO;
namespace GlitterApplication.Controllers
{
    [AllowCrossSiteJson]
    public class FollowsController : ApiController
    {
        private FollowBs followBs;
        public FollowsController()
        {
            followBs = new FollowBs();
        }

        [HttpPost]
        [System.Web.Http.Route("api/follows/follow")]
        public IHttpActionResult Follow(FollowDTO followDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            followBs.AddFollowing(followDTO);
            return Ok(followDTO);
        }

        [HttpPost]
        [System.Web.Http.Route("api/follows/unfollow")]
        public IHttpActionResult Unfollow(FollowDTO followDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            followBs.DeleteFollowing(followDTO);
            return Ok(followDTO);
        }
    }
}
