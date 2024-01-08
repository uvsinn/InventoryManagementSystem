
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Web.Http.Controllers;

namespace InventoryManagementSystem.Models.Models
{
    public class BasicAuthenticationAttribute : Attribute, IAuthorizationFilter
    {
        private const string Realm = "My Realm";
        static Microsoft.AspNetCore.Http.IHttpContextAccessor _mHttpContextAccessor;
        static Microsoft.AspNetCore.Http.HttpContext Current => _mHttpContextAccessor?.HttpContext;
        
        public void OnAuthorization(AuthorizationFilterContext actionContext)
        {
            string authHeader = actionContext.HttpContext.Request.Headers["Authorization"];
             
            string username;
            string password;
            if (authHeader != null && authHeader.StartsWith("Basic"))
            {
                //Extract credentials
                string encodedUsernamePassword = authHeader.Substring("Basic ".Length).Trim(); Encoding encoding =Encoding.GetEncoding("iso-8859-1");
                string usernamePassword = encoding.GetString(Convert.FromBase64String(encodedUsernamePassword));
                int seperatorIndex = usernamePassword.IndexOf(':');

                username = usernamePassword.Substring(0, seperatorIndex);
                password = usernamePassword.Substring(seperatorIndex + 1);
            }
            else
            {
                //Handle what happens if that isn't the case
                throw new Exception("The authorization header is either empty or isn't Basic.");
            }
                if (UserValidate.Login(username, password))
                {
                    var UserDetails = UserValidate.GetUserDetails(username, password);
                    var identity = new GenericIdentity(username);
                    identity.AddClaim(new Claim("Email", UserDetails.Email));
                    identity.AddClaim(new Claim(ClaimTypes.Name, UserDetails.UserName));
                    identity.AddClaim(new Claim("ID", Convert.ToString(UserDetails.ID)));
                    IPrincipal principal = new GenericPrincipal(identity, UserDetails.Roles.Split(','));
                    Thread.CurrentPrincipal = principal;
                    if (Current != null)
                    {

                        Current.User = (ClaimsPrincipal)principal;
                    }
                }
                else
                {
                    actionContext.Result = new JsonResult("Unauthorized Access")
                    {
                        StatusCode = StatusCodes.Status401Unauthorized
                    };

                    //actionContext.Response = actionContext.HttpContext.Request
                    //    .CreateResponse(HttpStatusCode.Unauthorized);
                }
         
        }
    }
}