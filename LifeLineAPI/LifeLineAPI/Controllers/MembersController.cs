using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DataAccessLayer;

namespace LifeLineAPI.Controllers
{
    public class MembersController : ApiController
    {

        //localhost:50101/api/members/?email=rayanulhassan@outlook.com&password=020hassan
        //localhost:50101/api/members/

        public HttpResponseMessage get(string email = "all", string password = "all")
        {
            using(lifelinedbEntities entities = new lifelinedbEntities())
            {

                
                if(email == "all" && password == "all")
                {
                    return Request.CreateResponse(HttpStatusCode.OK, entities.Members.ToList());
                }
                else
                {
                    try
                    {
                        Member member = entities.Members.FirstOrDefault(m => m.email == email);
                        if(member.password == password)
                        {
                            return Request.CreateResponse(HttpStatusCode.Found, entities.Members.Where(m => m.email == email).ToList());
                        }
                        else
                        {
                            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No record found for email " + email);
                        }
                    }
                    catch(Exception ex)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex.Message);
                    }
                    
                }
                
            }
        }

        public HttpResponseMessage get(string email)
        {
            using (lifelinedbEntities entities = new lifelinedbEntities())
            {

                var entity =  entities.Members.FirstOrDefault(m => m.email == email);
                if(entity != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, entity);

                }

                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Member with email " + email + " not found");
                }

            }
        }

        // public HttpResponseMessage post([FromBody] Member member)
        //{
        //    try
        //    {
        //        using(lifelinedbEntities entites = new lifelinedbEntities())
        //        {
        //            entites.Members.Add(member);
        //            entites.SaveChanges();

        //            var message = Request.CreateResponse(HttpStatusCode.Created, member);
        //            message.Headers.Location = new Uri(Request.RequestUri + member.memberId.ToString());
        //            return message;
        //        }
        //    }
        //    catch(Exception ex)
        //    {
        //        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
        //    }
        //}
        //http://localhost:50101/api/members/?age=21&firstName=hasan&lastName=hasa&email=hasa&password=hasa&gender=has/
        public HttpResponseMessage post([FromUri] Member member)
        {
            try
            {
                using (lifelinedbEntities entites = new lifelinedbEntities())
                {
                    entites.Members.Add(member);
                    entites.SaveChanges();

                    var message = Request.CreateResponse(HttpStatusCode.Created, member);
                    message.Headers.Location = new Uri(Request.RequestUri + member.memberId.ToString());
                    return message;
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        public HttpResponseMessage Put(int id, [FromUri]string fName = "same", [FromUri]string lName = "same")
        {
            try
            {
                using (lifelinedbEntities entities = new lifelinedbEntities())
                {
                    var entity = entities.Members.FirstOrDefault(m => m.memberId == id);
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound,
                        "Member with Id " + id.ToString() + " not found to update");
                    }
                    else
                    {
                        if(fName != "same")
                        {
                            entity.firstName = fName;
                            entities.SaveChanges();
                            return Request.CreateResponse(HttpStatusCode.OK, entity);
                        }
                        else if(lName != "same")
                        {
                            entity.lastName = lName;
                            entities.SaveChanges();
                            return Request.CreateResponse(HttpStatusCode.OK, entity);
                        }
                        else
                        {
                            return Request.CreateResponse(HttpStatusCode.BadRequest,"No information Provided"); 
                        }
                        //entity.FirstName = employee.FirstName;
                        //entity.LastName = employee.LastName;
                        //entity.Gender = employee.Gender;
                        //entity.Salary = employee.Salary;

                        //entities.SaveChanges();

                        //return Request.CreateResponse(HttpStatusCode.OK, entity);
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }


    }
}
