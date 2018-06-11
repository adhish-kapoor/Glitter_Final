using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel;

namespace HashTagAccess
{
   public class HashTagDb
    {
        private GlitterDBEntities db;
        public HashTagDb()
        {
            db = new GlitterDBEntities();
        }


        public int GetHashTagId(string hashTag)
        {
            int hashTagId = 0;
            var hashTagObj = db.HashTags.Where(x => x.HashTagContent == hashTag).FirstOrDefault();
            if (hashTagObj != null)
            {
                hashTagId = hashTagObj.HashTagId;
            }
            return hashTagId;
        }



        public int UpdateHashTag(string hashTag)
        {
            int hashTagId = 0;
            var hashTagObj = db.HashTags.Where(x => x.HashTagContent == hashTag).FirstOrDefault();
            if (hashTagObj != null)
            {

                if (hashTagObj.Count > 1)
                {
                    hashTagObj.Count = hashTagObj.Count - 1;
                    hashTagId = hashTagObj.HashTagId;
                    Save();
                }
                else
                {
                    db.HashTags.Remove(hashTagObj);
                    Save();
                    hashTagId = hashTagObj.HashTagId;
                }
            }
            return hashTagId;

        }


        public void Save()
        {
            db.SaveChanges();
        }
    }
}
