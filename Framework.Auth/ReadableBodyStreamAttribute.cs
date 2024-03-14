using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace Framework.Auth
{
    public class ReadableBodyStreamAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            Guid id = new Guid(context.HttpContext.User.Claims.First(claim => claim.Type == "Id").Value);
            var userName = context.HttpContext.User.Claims.First(claim => claim.Type == ClaimTypes.Name).Value;
            new ClaimsContext(id, userName);
        }
    }
    public class ClaimsContext
    {
        private static Guid _id;
        private static string _userName;
        public ClaimsContext(Guid id, string userName)
        {
            _id = id;
            _userName = userName;
        }

        public static Guid Id()
        {
            return _id;
        }

        public static string UserName()
        {
            return _userName;
        }
    }

}
