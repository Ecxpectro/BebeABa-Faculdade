using Microsoft.Extensions.Options;
using Shared.ApiUtilities;
using Shared.Models;
using Shared.Services.Interfaces;
using System.Threading.Tasks;

namespace Shared.Services
{
    public class ChildrenTimeLineService : IChildrenTimeLineService
    {
        private readonly HostService _service;
        private readonly RestApiEndPoints _host;
        public ChildrenTimeLineService(IOptions<HostService> service)
        {
            _service = service.Value;
            _host = new RestApiEndPoints(_service);
        }

        public async Task<Response> Save(ChildrenTimeLineModel childrenTimeLine) => await RestUtility.WebServiceAsync
            ($"{_host.ChildrenTimelineEndpoint}",
                string.Empty,
                childrenTimeLine,
                "POST",
                string.Empty,
                string.Empty);
    }
}
