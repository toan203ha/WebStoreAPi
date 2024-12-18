using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

public class AuthServices
{
    private readonly IHttpClientFactory _httpClientFactory;

    public AuthServices(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<(bool IsSuccess, string Token, string Cookies, string ErrorMessage)> LoginAsync(string email, string password)
    {
        var client = _httpClientFactory.CreateClient(); // Sử dụng IHttpClientFactory để tạo HttpClient
        var loginData = new
        {
            emailCus = email,
            passWord = password
        };

        var jsonContent = new StringContent(
            JsonSerializer.Serialize(loginData),
            Encoding.UTF8,
            "application/json"
        );

        try
        {
            var response = await client.PostAsync("http://localhost:3000/api/users/login", jsonContent);
            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                var tokenData = JsonSerializer.Deserialize<LoginResponse>(responseBody);

                // Lấy cookie từ header Set-Cookie
                var cookies = response.Headers.TryGetValues("Set-Cookie", out var cookieValues)
                    ? string.Join("; ", cookieValues)
                    : null;
                return (true, tokenData.Token, cookies, null);
            }
            else
            {
                return (false, null, null, "Invalid credentials.");
            }
        }
        catch (Exception ex)
        {
            return (false, null, null, $"Error: {ex.Message}");
        }
    }
}

public class LoginResponse
{
    public string Message { get; set; }
    public string Token { get; set; }
}
