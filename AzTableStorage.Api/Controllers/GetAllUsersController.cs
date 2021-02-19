using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using AzTableStorage.Core.Users;
using Nicoco.Lib.Cqs.Query;

namespace AzTableStorage.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GetAllUsersController : ControllerBase
    {
        private readonly IQueryDispatcher _queryDispatcher;

        public GetAllUsersController(IQueryDispatcher queryDispatcher)
        {
            _queryDispatcher = queryDispatcher;
        }

        [HttpGet]
        public async Task<GetUsersResponse> Get()
        {
            return await _queryDispatcher.DispatchAsync<GetUsersRequest, GetUsersResponse>(new GetUsersRequest());
        }
    }
}
