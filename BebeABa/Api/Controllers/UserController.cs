using Api.Business.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Models;
using Shared.ApiUtilities;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Route("api/BebeABa")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserBusiness _userBusiness;

        public UserController(IUserBusiness userBusiness)
        {
            _userBusiness = userBusiness;
        }

        [HttpPost("Users")]
        public async Task<ActionResult<Response>> CreateUser(UserModel user)
        {
            var response = await _userBusiness.CreateUser(user);
            return Ok(response);
        }
    }
}
