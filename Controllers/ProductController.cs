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
            var users = await _proservice.GetAllPro();
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

            var user = await _proservice.GetProById(id);

            // Debug: Kiểm tra nếu không tìm thấy 
            if (user == null)
            {
                Console.WriteLine($"Debug: User with ID: {id} not found.");
                return NotFound();
            }

            // Debug: Log thông tin tìm thấy
            Console.WriteLine($"Debug: User found - Email: {user.TEN}, PhoneNumber: {user.PRICE}");
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> EditPro(Product user)
        {
            // Debug: Log thông tin và trạng thái ModelState
            Console.WriteLine($"Debug: Entering EditUser POST method with user ID: {user._id}");
            Console.WriteLine($"Debug: ModelState is valid: {ModelState.IsValid}");

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
                var success = await _proservice.UpdatePro(user._id, user);

                // Debug: Log kết quả cập nhật
                if (success)
                {
                    Console.WriteLine($"Debug: Successfully updated user with ID: {user._id}");
                    return RedirectToAction("Index");
                }
                else
                {
                    Console.WriteLine($"Debug: Update failed for user with ID: {user._id}");
                    ModelState.AddModelError("", "Cập nhật không thành công.");
                }
            }
            else
            {
                Console.WriteLine("Debug: ModelState is not valid.");
            }
            return View(user);
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

