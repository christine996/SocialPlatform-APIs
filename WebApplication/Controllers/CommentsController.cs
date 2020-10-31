using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplication.Models;
namespace WebApplication.Controllers
{
    public class CommentsController : ApiController
    {
        [HttpGet]
        [Route("api/getPostComments")]
        public object getPostComments(int PostId)
        {


            using (var context = new DB_SocialPlatformEntities())
            {
                var comments = (from c in context.Comments
                                where PostId == c.PostId

                                select new
                                {
                                    Id = c.Id,
                                    comment = c.Comment1,
                                    PostId = c.PostId


                                }).ToArray();
                return Json(comments);
            }
        }
        [HttpPost]
        [Route("api/addComment")]
        public object addComment([FromBody] CommentsViewModel model)
        {


            using (var context = new DB_SocialPlatformEntities())
            {
                try
                {
                    var Comment = new Comment
                    {
                        Comment1 = model.Comment,

                        PostId = model.PostId


                    };
                    context.Comments.Add(Comment);
                    context.SaveChanges();

                    return Request.CreateResponse(HttpStatusCode.OK);
                }
                catch (Exception ex)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest);
                }

            }
        }

        [HttpPost]
        [Route("api/deleteComment")]
        public object deleteComment([FromBody] CommentsViewModel model)
        {


            using (var context = new DB_SocialPlatformEntities())
            {
                try
                {
                    context.Comments.Remove(context.Comments.First(c => c.Id == model.Id));
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
        [Route("api/getAllComments")]
        public object getAllComments()
        {
            using (var context = new DB_SocialPlatformEntities())
            {
                var comments = (from c in context.Comments

                                select new
                                {
                                    Id = c.Id,
                                    comment = c.Comment1,
                                    PostId = c.PostId


                                }).ToArray();
                return Json(comments);
            }
        }

    }
}
