using Api.Business.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Shared.Models;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Route("api/BebeABa")]
    [ApiController]
    public class ForumAnswerController : ControllerBase
    {
        private readonly IForumAnswerBusiness _forumAnswerBusiness;

        public ForumAnswerController(IForumAnswerBusiness forumAnswerBusiness)
        {
            _forumAnswerBusiness = forumAnswerBusiness;
        }
        [HttpPost("ForumAnswer")]
        public async Task<IActionResult> CreateAnswer(ForumAnswerModel forumAnswer) => Ok(await _forumAnswerBusiness.CreateAnswer(forumAnswer));
    }
}
