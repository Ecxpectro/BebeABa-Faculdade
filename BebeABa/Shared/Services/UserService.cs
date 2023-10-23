using Microsoft.Extensions.Options;
using Shared.ApiUtilities;
using Shared.Models;
using Shared.Services.Interfaces;
using System.Threading.Tasks;

namespace Shared.Services
{
    public class UserService : IUserService
    {
        private readonly HostService _service;
        private readonly RestApiEndPoints _host;
        public UserService(IOptions<HostService> service)
        {
            _service = service.Value;
            _host = new RestApiEndPoints(_service);
        }
        public async Task<Response> CreateUser(UserModel user) => await RestUtility.WebServiceAsync
            ($"{_host.UserEndpoint}",
                string.Empty,
                user,
                "POST",
                string.Empty,
                string.Empty);
    }
}
