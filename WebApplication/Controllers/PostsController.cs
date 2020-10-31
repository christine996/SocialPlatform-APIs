using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class PostsController : ApiController
    {

        [HttpGet]
        [Route("api/getAllPosts")]
        public object getAllPosts()
        {


            using (var context = new DB_SocialPlatformEntities())
            {
                var posts = (from p in context.Posts

                                select new
                                {
                                    Id = p.Id,
                                    Post=p.Post1
                                  

                                }).ToArray();
                return Json(posts);
            }
        }

        [HttpPost]
        [Route("api/addPost")]
        public object addPost([FromBody] PostsViewModel model)
        {


            using (var context = new DB_SocialPlatformEntities())
            {
                try
                {
                    var Post = new Post
                    {
                        Post1 = model.Post


                    };
                    context.Posts.Add(Post);
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
        [Route("api/deletePost")]
        public object deletePost([FromBody] PostsViewModel model)
        {


            using (var context = new DB_SocialPlatformEntities())
            {
                try
                {
                    var likes = context.Likes.Where(l => l.PostId == model.Id);
                    context.Likes.RemoveRange(likes);

                    var comments = context.Comments.Where(c => c.PostId == model.Id);
                    context.Comments.RemoveRange(comments);

                    context.Posts.Remove(context.Posts.First(c => c.Id == model.Id));
                    context.SaveChanges();

                    return Request.CreateResponse(HttpStatusCode.OK);
                }
                catch (Exception ex)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest);
                }

            }
        }

        
    }
}
