using RotamaticApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Security;

namespace RotamaticApp.Controllers
{
    public class AuthController : ApiController
    {
        public AuthController()
        {
            String AdminRole = "Admin";
            if (!Roles.RoleExists(AdminRole))
                Roles.CreateRole(AdminRole);

            String BasicUserRole = "User";
            if (!Roles.RoleExists(BasicUserRole))
                Roles.CreateRole(BasicUserRole);

            String AdminEmail = "admin@gmail.com";
            String AdminPassword = "admin1982";
            if (Membership.FindUsersByName(AdminEmail).Count == 0) { 
                Membership.CreateUser(AdminEmail, AdminPassword, AdminEmail);
                Roles.AddUserToRole(AdminEmail, AdminRole);
            }
            
            String UserEmail = "user@gmail.com";
            String UserPassword = "user1982";
            if (Membership.FindUsersByName(UserEmail).Count == 0){
                Membership.CreateUser(UserEmail, UserPassword, UserEmail);
                Roles.AddUserToRole(UserEmail, BasicUserRole);
            }
        }

        
        [HttpPost]
        [ActionName("sign-in")]
        public HttpResponseMessage SignIn([FromBody]LoginViewModel LoginVM)
        {
            bool isValid = Membership.ValidateUser(LoginVM.Username, LoginVM.Password);
            if (isValid)
            {
                FormsAuthentication.SetAuthCookie(LoginVM.Username.ToLower(), false);
                SessionData SessionData = CreateSessionData(LoginVM);
                return Request.CreateResponse(HttpStatusCode.OK, SessionData);
            }
            return Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "Login Failed");
        }

        [HttpPost]
        [ActionName("sign-out")]
        public HttpResponseMessage SignOut()
        {
            FormsAuthentication.SignOut();
            var name = System.Web.HttpContext.Current.User.Identity.Name;
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        private SessionData CreateSessionData(LoginViewModel LoginVM)
        {
            // The object to return to the client.
            SessionData SessionData = new SessionData
            {
                Username = LoginVM.Username.ToLower(),
                EmailAddress = LoginVM.Username.ToLower(),
                UserRoles = new List<string>()
            };

            // Check Group Membership...
            foreach (String role in Roles.GetRolesForUser(LoginVM.Username.ToLower()))
            {
                SessionData.UserRoles.Add(role);
            };

            return SessionData;
        }

        private String Get()
        {
            return "Auth Controller Working!";
        }
    }
}
