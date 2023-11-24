using Front.ViewModels.Interface;
using Microsoft.AspNetCore.Mvc;
using Shared.Models;
using Shared.ApiUtilities;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System;
using Newtonsoft.Json;
using Front.ViewModels;
using Shared.Helpers;
using System.Linq;
using Shared;

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

        public async Task<IActionResult> SaveUser(IFormCollection formCollection)
        {
            var isOk = false;
            string msg = string.Empty;
            Response response;

            try
            {
                var files = formCollection.Files;
                var user = JsonConvert.DeserializeObject<UserModel>(formCollection["UserJson"]);
                if (user != null)
                {
                    if (files.Any() && files[0] is { Length: > 0 })
                    {
                        var extension = files[0].FileName.Split('.').Last();
                        if (extension == "jpeg" || extension == "jpg" || extension == "png")
                        {

                            var fileName = $"{FunctionsHelper.GenerateNumbersRandom(0, 999999)}{files[0].FileName}";
                            var path = $"{Constants.UserImagesPath}/{fileName}";

                            using (var fs = System.IO.File.Create(path))
                            { await files[0].CopyToAsync(fs); }
                            user.UserFilePath = fileName;
                        }
                        else
                        {
                            msg = "Invalid format for the image";
                        }
                    }
                    response = await _userViewModel.CreateUser(user);
                    if (response.Status == Shared.Enums.StatusCode.Success)
                    {
                        var cookieValue = FunctionsHelper.Encrypt(JsonConvert.SerializeObject(user));
                        var cookieOption = new CookieOptions { Expires = DateTime.Now.AddDays(360) };
                        HttpContext.Response.Cookies.Append("userLoggedBebeABa", cookieValue, cookieOption);
                        isOk = true;
                    }
                }
            }
            catch (Exception ex)
            { }

            return Json(new { success = isOk, message = msg });
        }
    }
}
