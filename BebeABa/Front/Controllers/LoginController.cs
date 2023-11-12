using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Shared.Models;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System;
using Front.ViewModels.Interface;
using Shared.ApiUtilities;
using Shared.Helpers;

namespace Front.Controllers
{
    public class LoginController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserViewModel _userViewModel;

        public LoginController(IHttpContextAccessor httpContextAccessor, IUserViewModel userViewModel)
        {
            _httpContextAccessor = httpContextAccessor;
            _userViewModel = userViewModel;
        }
        public IActionResult Index()
        {
            UserModel user = null;
            var cookieValue = _httpContextAccessor.HttpContext?.Request.Cookies["userLoggedBebeABa"];
            if (cookieValue != null)
            { user = JsonConvert.DeserializeObject<UserModel>(FunctionsHelper.Decrypt(cookieValue)); }
            ViewBag.UserLogged = user;
            return View();
        }

        public async Task<IActionResult> Login(UserModel user)
        {
            
            UserModel userLogged = null;
            var isOk = false;
            var msg = string.Empty;
            try
            {
                Response response = null;
                response = await _userViewModel.Login(user);
                if (response.Status == Shared.Enums.StatusCode.Success)
                {
                    userLogged = JsonConvert.DeserializeObject<UserModel>(response.Result.ToString());

                    if (userLogged != null)
                    {
                        userLogged.CheckRememberMe = user.CheckRememberMe;
                        isOk = true;
                        var cookieValue = _httpContextAccessor.HttpContext?.Request.Cookies["userLoggedBebeABa"];
                        if (cookieValue != null)
                        { Response.Cookies.Delete("userLoggedBebeABa"); }

                        cookieValue = FunctionsHelper.Encrypt(JsonConvert.SerializeObject(userLogged));
                        var cookieOption = new CookieOptions { Expires = DateTime.Now.AddDays(360) };
                        HttpContext.Response.Cookies.Append("userLoggedBebeABa", cookieValue, cookieOption);
                    }
                }
               

            }
            catch (Exception ex)
            { msg = ex.Message; }

            return Json(new { success = isOk, user = userLogged, msg });
        }
    }
}
