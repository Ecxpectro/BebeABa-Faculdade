using Api.Business.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Shared.Models;
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
        public async Task<IActionResult> CreateUser(UserModel user) => Ok(await _userBusiness.CreateUser(user));

        [HttpPost("Users/Login")]
        public async Task<IActionResult> Login(UserModel user) => Ok(await _userBusiness.Login(user));
    }
}
