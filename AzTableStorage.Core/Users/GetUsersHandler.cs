using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AzTableStorage.Dto.Users;
using Nicoco.Lib.Cqs.Query;

namespace AzTableStorage.Core.Users
{
    public class GetUsersHandler : IQueryHandlerAsync<GetUsersRequest, GetUsersResponse>
    {
        private readonly IUserRepository _userRepository;

        public GetUsersHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<GetUsersResponse> HandleAsync(GetUsersRequest request)
        {
            var result = await _userRepository.GetAll();
            
            var users = result.Select(x => new UserDto
            {
                Id = x.Id,
                FullName = x.FullName,
                Email = x.Email
            }).ToList();

            return new GetUsersResponse
            {
                Users = users
            };
        }
    }

    public class GetUsersRequest
    {
    }

    public class GetUsersResponse
    {
        public List<UserDto> Users { get; set; }
    }

}