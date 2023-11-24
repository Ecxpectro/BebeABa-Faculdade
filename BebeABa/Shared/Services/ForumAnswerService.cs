using Microsoft.Extensions.Options;
using Shared.ApiUtilities;
using Shared.Models;
using Shared.Services.Interfaces;
using System.Threading.Tasks;

namespace Shared.Services
{
    public class ForumAnswerService : IForumAnswerService
    {
        private readonly HostService _service;
        private readonly RestApiEndPoints _host;
        public ForumAnswerService(IOptions<HostService> service)
        {
            _service = service.Value;
            _host = new RestApiEndPoints(_service);
        }

        public async Task<Response> CreateAnswer(ForumAnswerModel forumAnswer) => await RestUtility.WebServiceAsync
            ($"{_host.ForumAnswerServiceEndpoint}",
                string.Empty,
                forumAnswer,
                "POST",
                string.Empty,
                string.Empty);
    }
}
