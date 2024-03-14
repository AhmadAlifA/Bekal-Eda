using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Framework.Auth
{
    public class ContextAccessor
    {
        public Guid Id { get; set; }
        public ContextAccessor(IHttpContextAccessor contextAccessor)
        {
            Id = new Guid(contextAccessor.HttpContext.User.FindFirstValue("Id"));
        }
    }
}
