using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class UsersController : ApiController
    {
        [HttpGet]
        [Route("api/getAllUsers")]
        public object getAllUsers()
        {


            using (var context = new DB_SocialPlatformEntities())
            {
                var posts = (from u in context.USERS
                             where u.ActiveUser == true
                             select new
                             {
                                 Id = u.Id,
                                 Email = u.Email,
                                 UserName = u.UserName,
                                 MobileNumber = u.MobileNumber,



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
                    if (user != null)

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
        [HttpPost]
        [Route("api/Registeration")]
        public object Registeration([FromBody] UsersViewModel model)
        {


            using (var context = new DB_SocialPlatformEntities())
            {
                try
                {
                    var user = context.USERS.Any(u => u.Email == model.Email);
                    if (!user)
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

    }
}
