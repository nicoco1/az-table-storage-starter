using System.Threading.Tasks;
using AzTableStorage.Domain.Users;

namespace AzTableStorage.Core.Users
{
    public interface IUserService
    {
        Task Create(User user);
    }

    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task Create(User user)
        {
            await _userRepository.Create(user);
        }
    }
}