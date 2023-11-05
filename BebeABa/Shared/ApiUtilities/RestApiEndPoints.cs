using Shared.ApiUtilities;

namespace Shared.ApiUtilities
{
    public class RestApiEndPoints
    {
        private readonly HostService _hostService;

        public RestApiEndPoints(HostService hostService)
        { _hostService = hostService; }

        public string BaseAddress => _hostService.Host;


        public string UserEndpoint => $"{BaseAddress}BebeABa/Users";
        public string ChildrenEndpoint => $"{BaseAddress}BebeABa/Children";
        public string ChildrenTimelineEndpoint => $"{BaseAddress}BebeABa/ChildrenTimeLine";

    }
}
