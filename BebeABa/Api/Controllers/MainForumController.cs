using Api.Business.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.ApiUtilities;
using Shared.Models;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Route("api/BebeABa")]
    [ApiController]
    public class MainForumController : ControllerBase
    {
        private readonly IMainForumBusiness _mainForumBusiness;

        public MainForumController(IMainForumBusiness mainForumBusiness)
        {
            _mainForumBusiness = mainForumBusiness;
        }
        [HttpPost("MainForum")]
        public async Task<IActionResult> CreateForum(MainForumModel mainForum) => Ok(await _mainForumBusiness.CreateForum(mainForum));

        [HttpGet("MainForum/GetAllForum")]
        public async Task<ActionResult<Response>> GetAllForum() => Ok(await _mainForumBusiness.GetAllForum());
        [HttpDelete("MainForum/{id:long}")]
        public async Task<IActionResult> Delete(long id) => Ok(await _mainForumBusiness.Delete(id));
    }
}
