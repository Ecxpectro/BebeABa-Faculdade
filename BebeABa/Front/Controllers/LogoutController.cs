using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Front.Controllers
{
    public class LogoutController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LogoutController(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public IActionResult Logout()
        {
            var cookieValue = _httpContextAccessor.HttpContext?.Request.Cookies["userLoggedBebeABa"];
            if (cookieValue != null)
            { Response.Cookies.Delete("userLoggedBebeABa"); }

            return RedirectToAction("Index", "Login");
        }

    }
}
