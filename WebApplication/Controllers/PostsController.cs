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
                                 Id=c.Id,
                                 comment=c.Comment1,
                                 PostId=c.PostId


                             }).ToArray();
                return Json(comments);
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

                        PostId=model.PostId


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
        [Route("api/addLikes")]
        public object addLikes([FromBody]LikesViewModel model)
        {


            using (var context = new DB_SocialPlatformEntities())
            {
                try
                {
                    var Like = new Like
                    {
                        Like1=true,

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
                                    PostId=c.PostId


                                }).ToArray();
                return Json(comments);
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

        [HttpPost]
        [Route("api/Registeration")]
        public object Registeration([FromBody] UsersViewModel model)
        {


            using (var context = new DB_SocialPlatformEntities())
            {
                try
                {
                    var user = context.USERS.Any(u => u.Email == model.Email);
                    if(!user)
                    {
                        var users = new USER

                        {
                            Email = model.Email,

                            Password = model.Password,
                            UserName = model.UserName,
                            MobileNumber = model.MobileNumber,
                            ActiveUser = true



                        };
                        context.USERS.Add(users);
                        context.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK);
                    }
                  
                   

                   
                }
                catch (Exception ex)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest);
                }

            }
            return Request.CreateResponse(HttpStatusCode.BadRequest);
        }
        [HttpGet]
        [Route("api/getAllUsers")]
        public object getAllUsers()
        {


            using (var context = new DB_SocialPlatformEntities())
            {
                var posts = (from u in context.USERS
                             where u.ActiveUser==true
                             select new
                             {
                                 Id = u.Id,
                                 Email=u.Email,
                                 UserName=u.UserName,
                                 MobileNumber=u.MobileNumber,
                                 


                             }).ToArray();
                return Json(posts);
            }
        }

        [HttpPost]
        [Route("api/deactivateUser")]
        public object deactivateUser([FromBody] UsersViewModel model)
        {


            using (var context = new DB_SocialPlatformEntities())
            {
                var user = context.USERS.First(u => u.Id == model.Id);
                try
                {
                    if(user !=null)

                    {

                        user.ActiveUser = false;



                    };

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
