using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebStore.Models;

namespace WebStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            // Lấy dữ liệu từ session
            string token = HttpContext.Session.GetString("Token");

            // Kiểm tra nếu token có tồn tại trong session hay không
            if (token != null)
            {
                // Thực hiện hành động với token
                Console.WriteLine("Token: " + token);
            }
            else
            {
                // Nếu không có token trong session
                Console.WriteLine("No token found in session.");
            }

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}