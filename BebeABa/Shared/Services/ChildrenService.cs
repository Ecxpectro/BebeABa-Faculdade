using Microsoft.Extensions.Options;
using Shared.ApiUtilities;
using Shared.Models;
using Shared.Services.Interfaces;
using System.Threading.Tasks;

namespace Shared.Services
{
    public class ChildrenService : IChildrenService
    {
        private readonly HostService _service;
        private readonly RestApiEndPoints _host;
        public ChildrenService(IOptions<HostService> service)
        {
            _service = service.Value;
            _host = new RestApiEndPoints(_service);
        }
        public async Task<Response> CreateChildren(ChildrenModel children) => await RestUtility.WebServiceAsync
            ($"{_host.ChildrenEndpoint}",
                string.Empty,
                children,
                "POST",
                string.Empty,
                string.Empty);
        public async Task<Response> GetChildrenById(long childrenId) => await RestUtility.WebServiceAsync(
        $"{_host.ChildrenEndpoint}/{childrenId}",
        string.Empty,
        null,
        "GET",
        string.Empty,
        string.Empty);

		public async Task<Response> GetChildrenByUserId(long userId) => await RestUtility.WebServiceAsync(
		$"{_host.ChildrenEndpoint}/GetByUserId/{userId}",
		string.Empty,
		null,
		"GET",
		string.Empty,
		string.Empty);
	}
}
