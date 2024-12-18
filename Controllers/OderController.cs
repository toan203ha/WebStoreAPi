using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Models;
using WebStore.Services.Oder;
 
namespace WebStore.Controllers
{
    public class OderController : Controller
    {
        private readonly OrderServices _orderService;

        public OderController(OrderServices orderService)
        {
            _orderService = orderService;
        }

        public async Task<IActionResult> Index()
        {
            var token = HttpContext.Request.Cookies["authToken"];
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Auth");
            }

            var orders = await _orderService.GetAllOders(token);
            if (orders == null)
            {
                Console.WriteLine("Orders is null!");
            }
            return View(orders);
        }

        [HttpGet]
        public async Task<IActionResult> EditOder(string id)
        {
            // Debug: Log thông tin ID
            Console.WriteLine($"Debug: Entering EditOder GET method with ID: {id}");

            var token = HttpContext.Request.Cookies["authToken"];
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Auth");
            }

            var pro = await _orderService.GetAllOders(token);

            // Sử dụng FirstOrDefault để tìm đơn hàng với ID
            var proid = pro.FirstOrDefault(m => m._id == id);

            // Debug: Kiểm tra nếu không tìm thấy
            if (proid == null)
            {
                Console.WriteLine($"Debug: pro with ID: {id} not found.");
                return NotFound();
            }

            // Debug: Log thông tin tìm thấy
            Console.WriteLine($"Debug: Found order with ID: {id}");

            return View(proid); // Trả về proid (đơn hàng)
        }


        [HttpPost]
        public async Task<IActionResult> EditOder(Oders oders)
        {
            if (!ModelState.IsValid)
            {
                // Log all validation errors
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine($"Debug: ModelState Error - {error.ErrorMessage}");
                }
            }

            if (ModelState.IsValid)
            {
                var success = await _orderService.UpdateOder(oders._id, oders);

                // Debug: Log kết quả cập nhật
                if (success)
                {
                    Console.WriteLine($"Debug: Successfully updated order with ID: {oders._id}");
                    return RedirectToAction("Index");
                }
                else
                {
                    Console.WriteLine($"Debug: Update failed for order with ID: {oders._id}");
                    ModelState.AddModelError("", "Cập nhật không thành công.");
                }
            }
            else
            {
                Console.WriteLine("Debug: ModelState is not valid.");
            }
            // Return the same page with errors and data
            var orders = await _orderService.GetAllOders(HttpContext.Request.Cookies["authToken"]);
            return View("Index", orders);
        }




        public async Task<IActionResult> Filter(string delivered, string canceled, string pending)
        {
            var orders = await _orderService.GetAllOders("123123");

            // Lọc dữ liệu dựa trên tham số
            if (!string.IsNullOrEmpty(delivered))
            {
                bool deliveredFilter = delivered == "true";
                orders = orders.Where(o => o.daGiao == deliveredFilter).ToList();
            }

            if (!string.IsNullOrEmpty(canceled))
            {
                bool canceledFilter = canceled == "true";
                orders = orders.Where(o => o.daHuy == canceledFilter).ToList();
            }

            if (!string.IsNullOrEmpty(pending))
            {
                bool pendingFilter = pending == "true";
                orders = orders.Where(o => o.dangChoXacNhan == pendingFilter).ToList();
            }
            return Json(orders);
        }
        public async Task<IActionResult> Odercancel(string id,Oders orders)
        {
            try
            {
                var success = await _orderService.CancelOrder(id,orders);
                if (success)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Đã Hủy.");
                    return View("Index");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting user: {ex.Message}");
                ModelState.AddModelError("", "Có lỗi xảy ra khi cập nhật.");
                return View("Index");
            }
        }

        public async Task<IActionResult> Oderrestore(string id, Oders orders)
        {
            try
            {
                var success = await _orderService.RestoreOrder(id, orders);
                if (success)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Đã Khôi Phục.");
                    return View("Index");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting user: {ex.Message}");
                ModelState.AddModelError("", "Có lỗi xảy ra khi cập nhật.");
                return View("Index");
            }
        }

    }
}
