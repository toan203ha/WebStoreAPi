using Microsoft.AspNetCore.Mvc;
using WebStore.Models;

namespace WebStore.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProService _proservice;

        public ProductController(ProService apiService)
        {
            _proservice = apiService;
        }

        public async Task<IActionResult> Index()
        {
            var token = HttpContext.Request.Cookies["authToken"];
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Auth");
            }
            var users = await _proservice.GetAllPro(token);
            return View(users);
        }

        public async Task<IActionResult> ThongTinPro(string id)
        {
            var product = await _proservice.GetProById(id);
            if (product == null)
            {
                Console.WriteLine($"Debug: Product with ID: {id} not found.");
                return NotFound();
            }
            // Debug: Log thông tin sản phẩm
            Console.WriteLine($"Debug: Product found - ID: {product._id}, Name: {product.TEN}");

            return View(product);
        }



        public async Task<IActionResult> EditPro(string id)
        {
            // Debug: Log thông tin ID
            Console.WriteLine($"Debug: Entering EditUser GET method with ID: {id}");

            var pro = await _proservice.GetProById(id);

            // Debug: Kiểm tra nếu không tìm thấy 
            if (pro == null)
            {
                Console.WriteLine($"Debug: pro with ID: {id} not found.");
                return NotFound();
            }

            // Debug: Log thông tin tìm thấy
             return View(pro);
        }

        [HttpPost]
        public async Task<IActionResult> EditPro(Product pro)
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
                var success = await _proservice.UpdatePro(pro._id, pro);

                // Debug: Log kết quả cập nhật
                if (success)
                {
                    Console.WriteLine($"Debug: Successfully updated pro with ID: {pro._id}");
                    return RedirectToAction("Index");
                }
                else
                {
                    Console.WriteLine($"Debug: Update failed for pro with ID: {pro._id}");
                    ModelState.AddModelError("", "Cập nhật không thành công.");
                }
            }
            else
            {
                Console.WriteLine("Debug: ModelState is not valid.");
            }
            return View(pro);
        }


        // xóa 
        [HttpPost]
        public async Task<IActionResult> DeletePro(string id)
        {
            try
            {
                var success = await _proservice.DeletePro(id);
                if (success)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Xóa người dùng không thành công.");
                    return View("Index");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting user: {ex.Message}");
                ModelState.AddModelError("", "Có lỗi xảy ra khi xóa người dùng.");
                return View("Index");
            }
        }
        // tạo 
        public IActionResult CreatePro()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreatePro(Product user)
        {
            if (ModelState.IsValid)
            {

                var success = await _proservice.CreatePro(user);
                if (success)
                {
                    Console.WriteLine("Debug: User created successfully!");
                    TempData["Message"] = "User created successfully!";
                    TempData["MessageType"] = "success";
                    return RedirectToAction("Index");

                }
                else
                {
                    Console.WriteLine("Error: Failed to create user.");
                    TempData["Message"] = "Failed to create user.";
                    TempData["MessageType"] = "error";
                }

            }

            // Log that ModelState is not valid
            Console.WriteLine("Error: ModelState is not valid.");
            foreach (var value in ModelState.Values)
            {
                foreach (var error in value.Errors)
                {
                    Console.WriteLine($"Error: {error.ErrorMessage}");
                }
            }

            TempData["Message"] = "Invalid data.";
            TempData["MessageType"] = "error";
            return View(user);
        }


    }
}

