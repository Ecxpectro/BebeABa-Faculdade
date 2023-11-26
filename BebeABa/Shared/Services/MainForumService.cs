using Microsoft.Extensions.Options;
using Shared.ApiUtilities;
using Shared.Models;
using Shared.Services.Interfaces;
using System.Threading.Tasks;

namespace Shared.Services
{
    public class MainForumService : IMainForumService
    {
        private readonly HostService _service;
        private readonly RestApiEndPoints _host;
        public MainForumService(IOptions<HostService> service)
        {
            _service = service.Value;
            _host = new RestApiEndPoints(_service);
        }
        public async Task<Response> CreateForum(MainForumModel mainForum) => await RestUtility.WebServiceAsync
            ($"{_host.MainForumServiceEndpoint}",
                string.Empty,
                mainForum,
                "POST",
                string.Empty,
                string.Empty);

        public async Task<Response> DeleteForum(long id) => await RestUtility.WebServiceAsync(
          $"{_host.MainForumServiceEndpoint}/{id}",
            string.Empty,
            null,
            "DELETE",
            string.Empty,
            string.Empty);

        public async Task<Response> GetAllForum() => await RestUtility.WebServiceAsync
            ($"{_host.MainForumServiceEndpoint}/GetAllForum",
                string.Empty,
                null,
                "GET",
                string.Empty,
                string.Empty);
    }
}
