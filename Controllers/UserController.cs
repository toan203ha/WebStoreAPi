using Azure;
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
        // phân quyền
        var token = HttpContext.Request.Cookies["authToken"];
        if (string.IsNullOrEmpty(token))
        {
            return RedirectToAction("Login", "Auth"); 
        }
        //
        var users = await _apiService.GetAllUser(token);
        return View(users);
    }

    public async Task<IActionResult> ThongTinTK(string id)
    {
        try
        {
            var user = await _apiService.GetUserById(id);  

            if (user == null)
            {
                return NotFound();
            }

            return View(user);  
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            return StatusCode(500, "Internal Server Error");
        }
    }

    public async Task<IActionResult> EditUser(string id)
    {
        var user = await _apiService.GetUserById(id);
        Console.Write(user);

        if (user == null)
        {
            return NotFound();
        }
        return View(user);
    }

    [HttpPost]
    public async Task<IActionResult> EditUser(Users user)
    {
        if (ModelState.IsValid)
        {
            var success = await _apiService.UpdateUser(user._id, user);
            Console.WriteLine($"Debug: Received response with status code: {user._id}");

            if (success)
            {
                return RedirectToAction("Index");
            }
            else
            {
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
    [HttpGet]
    public IActionResult CreateUser()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> CreateUser(Users user)
    {
        if (ModelState.IsValid)
        {

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
