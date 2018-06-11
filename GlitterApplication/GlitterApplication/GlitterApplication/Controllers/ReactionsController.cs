using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ReactionBusinessLogic;
using ReactionsDTO;

namespace GlitterApplication.Controllers
{
    public class ReactionsController : ApiController
    {
        private ReactionBs reactionBs;
        public ReactionsController()
        {
            reactionBs = new ReactionBs();
        }



        [HttpPost]
        [System.Web.Http.Description.ResponseType(typeof(ReactionDTO))]
        [Route("api/reactions/")]
        public IHttpActionResult PostTweetReaction(ReactionDTO reactionDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            reactionBs.AddTweetReaction(reactionDTO);
            return Ok(reactionDTO);
        }

    }
}
