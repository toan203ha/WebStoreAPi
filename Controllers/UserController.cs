using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebStore.Models;

public class UsersController : Controller
{
    private readonly ApiService _apiService;

    public UsersController(ApiService apiService)
    {
        _apiService = apiService;
    }

    public async Task<IActionResult> Index()
    {
        var users = await _apiService.GetAllUser();
        return View(users);
    }

    public async Task<IActionResult> ThongTinTK(string id)
    {
        var user = await _apiService.GetUserById(id);
        return View(user);
    }

    public async Task<IActionResult> EditUser(string id)
    {
        // Debug: Log thông tin ID
        Console.WriteLine($"Debug: Entering EditUser GET method with ID: {id}");

        var user = await _apiService.GetUserById(id);

        // Debug: Kiểm tra nếu không tìm thấy người dùng
        if (user == null)
        {
            Console.WriteLine($"Debug: User with ID: {id} not found.");
            return NotFound();
        }

        // Debug: Log thông tin người dùng tìm thấy
        Console.WriteLine($"Debug: User found - Email: {user.Email}, PhoneNumber: {user.PhoneNumber}");
        return View(user);
    }

    [HttpPost]
    public async Task<IActionResult> EditUser(Users user)
    {
        // Debug: Log thông tin người dùng và trạng thái ModelState
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
            var success = await _apiService.UpdateUser(user._id, user);

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


    // xóa người dùng
    [HttpPost]
     public async Task<IActionResult> DeleteUser(string id)
    {
        try
        {
            var success = await _apiService.DeleteUser(id);
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
    // tạo người dùng
    public IActionResult CreateUser()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> CreateUser(Users user)
    {
        if (ModelState.IsValid)
        {
            Console.WriteLine($"Debug: ModelState is valid. Attempting to create user with Email: {user.Email}");

            var success = await _apiService.CreateUser(user);
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
