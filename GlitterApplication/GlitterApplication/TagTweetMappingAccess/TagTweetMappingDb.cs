using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel;
using HashTagTweetMapsDTO;
namespace TagTweetMappingAccess
{
    public class TagTweetMappingDb
    {
        private GlitterDBEntities db;
        public TagTweetMappingDb()
        {
            db = new GlitterDBEntities();
        }

        public void DeleteTagTweetMapping(HashTagTweetMapDTO hashTagTweetMapDTO)
        {
            //get the hashTagMapping object from the database
            var hashTagTweetMapObj = db.HashTagTweetMaps.Where(x => x.TweetId == hashTagTweetMapDTO.TweetId && x.HashTagId == hashTagTweetMapDTO.HashTagId).FirstOrDefault();
            if (hashTagTweetMapObj != null)
            {
                db.HashTagTweetMaps.Remove(hashTagTweetMapObj);
                Save();
            }
        }



        public void Save()
        {
            db.SaveChanges();
        }

    }
}
