using Microsoft.AspNetCore.Mvc;

namespace WebStore.Controllers
{
    public class VoucherController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
