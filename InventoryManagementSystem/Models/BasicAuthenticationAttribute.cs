using InventoryManagementSystem.Models;
using System;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace InventoryManagementSystem.Models.Models
{
    public class BasicAuthenticationAttribute : AuthorizationFilterAttribute
    {
        private const string Realm = "My Realm";
        static Microsoft.AspNetCore.Http.IHttpContextAccessor _mHttpContextAccessor;
        static Microsoft.AspNetCore.Http.HttpContext Current => _mHttpContextAccessor.HttpContext;
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if (actionContext.Request.Headers.Authorization == null)
            {
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
                if (actionContext.Response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    actionContext.Response.Headers.Add("WWW-Authenticate", string.Format("Basic realm=\"{0}\"", Realm));
                }
            }
            else
            {
                string authenticationToken = actionContext.Request.Headers.Authorization.Parameter;
                string decodedAuthenticationToken = Encoding.UTF8.GetString(Convert.FromBase64String(authenticationToken));
                string[] usernamePasswordArray = decodedAuthenticationToken.Split(':');
                string username = usernamePasswordArray[0];
                string password = usernamePasswordArray[1];
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
                    actionContext.Response = actionContext.Request
                        .CreateResponse(HttpStatusCode.Unauthorized);
                }
            }
        }
    }
}