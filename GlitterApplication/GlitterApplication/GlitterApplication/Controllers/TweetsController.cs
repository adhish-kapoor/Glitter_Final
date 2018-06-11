using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using TweetsDTO;
using TweetBusinessLogic;
namespace GlitterApplication.Controllers
{
    [AllowCrossSiteJson]
    public class TweetsController : ApiController
    {

        private TweetBs tweetBs;

        public TweetsController()
        {
            tweetBs = new TweetBs();
        }


        [HttpPost]
        [ResponseType(typeof(TweetDTO))]
        [System.Web.Http.Route("api/tweets/addtweet")]
        public IHttpActionResult AddTweet(TweetDTO tweetDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //db.UserDTOes.Add(userDTO);
            tweetDTO = tweetBs.AddTweet(tweetDTO);
            
            if (tweetDTO != null)
            {
                return Ok(tweetDTO);
            }
            else
            {
                return NotFound();
            }

        }



        //Delete a tweet
        [HttpDelete]
        [ResponseType(typeof(TweetDTO))]
        [System.Web.Http.Route("api/tweets/deletetweet")]
        public IHttpActionResult DeleteTweet(TweetDTO tweetDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            tweetDTO = tweetBs.DeleteTweet(tweetDTO);

            if (tweetDTO != null)
            {
                return Ok(tweetDTO);
            }
            else
            {
                return NotFound();
            }

        }



        //Search tweets on the basis of the hashtags
        [HttpGet]
        [System.Web.Http.Route("api/tweets/gettweets")]
        public IHttpActionResult GetTwets(string search)
        {
            var result = tweetBs.GetSearchedTweets(search);
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
