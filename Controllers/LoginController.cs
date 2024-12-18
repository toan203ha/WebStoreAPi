using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebStore.Services;

public class LoginController : Controller
{
    private readonly AuthServices _authService;

    public LoginController(AuthServices authService)
    {
        _authService = authService;
    }
    [HttpGet]
    public IActionResult Login()
    {
        return View(); 
    }


    [HttpPost]
    public async Task<IActionResult> Login(string email, string password)
    {
        var (isSuccess, token, cookies, errorMessage) = await _authService.LoginAsync(email, password);

        if (isSuccess)
        {
            // Lưu cookie vào response
            if (!string.IsNullOrEmpty(cookies))
            {
                Response.Headers.Append("Set-Cookie", cookies);
            }

            // Lưu token vào session (hoặc tùy chỉnh theo nhu cầu)
            //HttpContext.Session.SetString("AuthToken", token);

            return RedirectToAction("Index", "Home");
        }

        ModelState.AddModelError("", errorMessage);
        return View();
    }
}
