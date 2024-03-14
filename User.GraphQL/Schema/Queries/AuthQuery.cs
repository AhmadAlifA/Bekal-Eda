using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using User.Domain.Dtos;
using User.Domain.Services;

namespace User.GraphQL.Schema.Queries
{
    [ExtendObjectType(typeof(Query))]
    public class AuthQuery
    {
        private readonly IConfiguration _configuration;
        private readonly IUserService _service;
        public AuthQuery(IConfiguration configuration, IUserService service)
        {
            _configuration = configuration;
            _service = service;
        }
        public string Hello()
        {
            return "Hello GraphQl";
        }

        public async Task<LoginDto> LoginAsync(string username, string password)
        {
            LoginDto result = await _service.Login(username, password);
            if(result.Id != new Guid())
            {
                var claims = new List<Claim>()
                {
                    new Claim("Id", result.Id.ToString()),
                    new Claim(ClaimTypes.Name, result.UserName),
                    new Claim("FullName", result.FullName)
                };

                foreach(var item in result.Roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, item.ToLower()));
                }

                var token = GetToken(claims);
                result.Token = new JwtSecurityTokenHandler().WriteToken(token);
                result.Expiration = token.ValidTo;
                return result;
            }
            return new LoginDto();
        }

        private JwtSecurityToken GetToken(List<Claim> claims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes
                (_configuration["JWT:Secret"] ?? "None"));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddDays(Convert.ToDouble(_configuration["JWT:ExpireDays"])),
                claims: claims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256));

            return token;
        }
    }
}
