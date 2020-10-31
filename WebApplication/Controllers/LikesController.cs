using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class LikesController : ApiController
    {
        [HttpPost]
        [Route("api/addLikes")]
        public object addLikes([FromBody]LikesViewModel model)
        {


            using (var context = new DB_SocialPlatformEntities())
            {
                try
                {
                    var Like = new Like
                    {
                        Like1 = true,

                        PostId = model.PostId


                    };
                    context.Likes.Add(Like);
                    context.SaveChanges();

                    return Request.CreateResponse(HttpStatusCode.OK);
                }
                catch (Exception ex)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest);
                }

            }
        }

        [HttpGet]
        [Route("api/getPostLikes")]
        public object getPostLikes(int PostId)
        {


            using (var context = new DB_SocialPlatformEntities())
            {
                var likesCount = (from l in context.Likes

                                  where PostId == l.PostId
                                  select l).Count();

                return Json(likesCount);
            }
        }

    }
}
