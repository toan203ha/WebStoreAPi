using Microsoft.AspNetCore.Mvc;

namespace WebStore.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
