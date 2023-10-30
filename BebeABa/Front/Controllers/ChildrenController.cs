using Microsoft.AspNetCore.Mvc;

namespace Front.Controllers
{
    public class ChildrenController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
