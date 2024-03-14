using Framework.Core.Enums;

namespace User.Domain.Dtos
{
    public class UserDto
    {
        public Guid? Id { get; set; }
        public string UserName { get; set; } = default!;
        public string Password { get; set; } = default!;
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public UserTypeEnum? Type { get; set; } = UserTypeEnum.Customer;
        public RecordStatusEnum? Status { get; set; } = RecordStatusEnum.InActive;
    }

    public class LoginDto
    {
        public Guid Id { get; set; }
        public string UserName { get; set; } = default!;
        public string Password { get; set; } = default!;
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string FullName 
        { 
            get 
            { 
                return FirstName + " " + LastName;
            } 
        }
        public RecordStatusEnum Status { get; set; } = RecordStatusEnum.InActive;
        public List<string> Roles { get; set; } = new List<string>();
        public string Token { get; set; } = default!;
        public DateTime Expiration { get; set; }
    }
}
