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
            var orders = await _orderService.GetAllOders();
            return View(orders);
        }

        public async Task<IActionResult> Filter(string delivered, string canceled, string pending)
        {
            var orders = await _orderService.GetAllOders();

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
