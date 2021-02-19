using System.Threading.Tasks;
using AzTableStorage.Domain.Users;
using Nicoco.Lib.Cqs.Query;

namespace AzTableStorage.Core.Users
{
    public class CreateUserHandler : IQueryHandlerAsync<CreateUserCommand, CreateUserResponse>
    {
        private readonly IUserService _userService;

        public CreateUserHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<CreateUserResponse> HandleAsync(CreateUserCommand command)
        {
            var user = new User("user_2", command.Email)
            {
                FullName = command.FullName
            };

            
            await _userService.Create(user);

            return new CreateUserResponse
            {
                User = user
            };
        }
    }

    public class CreateUserResponse
    {
        public User User { get; set; }
    }

    public class CreateUserCommand
    {
        public string Email { get; set; }
        public string FullName { get; set; }
    }
}