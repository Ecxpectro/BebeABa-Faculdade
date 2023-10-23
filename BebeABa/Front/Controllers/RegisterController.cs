using Front.ViewModels.Interface;
using Microsoft.AspNetCore.Mvc;
using Shared.Models;
using Shared.ApiUtilities;
using System.Threading.Tasks;

namespace Front.Controllers
{
    public class RegisterController : Controller
    {
        private readonly IUserViewModel _userViewModel;

        public RegisterController(IUserViewModel userViewModel)
        {
            _userViewModel = userViewModel;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> SaveUser(UserModel user)
        {
            var isOk = false;
            Response response;

            response = await _userViewModel.CreateUser(user);
            if (response.Status == Shared.Enums.StatusCode.Success)
            {
                isOk = true;
            }


            return Json(new { success = isOk });
        }
    }
}
