using Microsoft.AspNetCore.Mvc;

namespace WebStore.Controllers
{
    public class NXBController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
