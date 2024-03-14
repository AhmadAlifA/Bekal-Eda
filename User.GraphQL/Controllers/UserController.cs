using Microsoft.AspNetCore.Mvc;
using User.Domain.Services;
using User.GraphQL.Schema.Mutation;

namespace User.GraphQL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;
        public UserController(IUserService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginTypeInput payload)
        {
            try
            {
                var result = await _service.Login(payload.UserName, payload.Password);
                return Ok(result);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
            }
            return BadRequest();
        }
    }
}
