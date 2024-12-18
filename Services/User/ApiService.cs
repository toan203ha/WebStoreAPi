using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WebStore.Models;
public class ApiResponse
{
    public IEnumerable<Users> Data { get; set; }
    public Users DataUnique { get; set; } // Chỉ dùng cho khi trả về 1 đối tượng duy nhất  public
    bool IsEmpty { get; set; }
    public string token { get; set; }
}

public class ApiService
{
    private readonly HttpClient _httpClient;

    public ApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    // Phương thức lấy tất cả người dùng
    public async Task<IEnumerable<Users>> GetAllUser(string token)
    {
        // Đảm bảo token được gắn vào header Authorization
        _httpClient.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        try
        {
            // Gửi yêu cầu GET tới API
            var response = await _httpClient.GetAsync("users");

            // Kiểm tra trạng thái phản hồi
            if (response.IsSuccessStatusCode)
            {
                // Nếu thành công, đọc nội dung và giải mã JSON
                var responseContent = await response.Content.ReadAsStringAsync();
                var apiResponse = JsonConvert.DeserializeObject<ApiResponse>(responseContent);
                return apiResponse.Data;  // Trả về danh sách người dùng
            }
            else
            {
                // Xử lý lỗi với trạng thái không thành công
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"API Error: {response.StatusCode} - {errorContent}");
            }
        }
        catch (HttpRequestException ex)
        {
            // Xử lý lỗi kết nối hoặc yêu cầu HTTP
            Console.WriteLine($"HTTP Request Error: {ex.Message}");
            throw;
        }
        catch (Exception ex)
        {
            // Xử lý các lỗi khác
            Console.WriteLine($"Unexpected Error: {ex.Message}");
            throw;
        }
    }


    // Phương thức lấy thông tin người dùng theo ID
    public async Task<Users> GetUserById(string id)
    {
        var response = await _httpClient.GetStringAsync($"http://localhost:3000/api/users/{id}");

        // Deserialize trực tiếp dữ liệu trả về thành một đối tượng Users
        var user = JsonConvert.DeserializeObject<Users>(response);

        // Trả về đối tượng người dùng
        return user;
    }

    // Phương thức cập nhật người dùng
    // Cập nhật thông tin người dùng
    public async Task<bool> UpdateUser(string id, Users user)
    {
        Console.WriteLine($"Debug: Entering UpdateUser method with ID: {id}");
 
        var json = JsonConvert.SerializeObject(user);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        Console.WriteLine($"Debug: JSON content being sent: {json}");

        HttpResponseMessage response;
        try
        {
            response = await _httpClient.PutAsync($"http://localhost:3000/api/users/{id}", content);
            Console.WriteLine($"Debug: Received response with status code: {response.StatusCode}");

            var responseContent = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Debug: Response content: {responseContent}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Debug: Exception occurred: {ex.Message}");
            return false;
        }
        return response.IsSuccessStatusCode;
    }

    // Phương thức xóa người dùng
    public async Task<bool> DeleteUser(string id)
    {
        var response = await _httpClient.DeleteAsync($"users/{id}");  
        return response.IsSuccessStatusCode;
    }

    // tạo người dùng
    public async Task<bool> CreateUser(Users user)
    {
        var json = JsonConvert.SerializeObject(user);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync("users", content);  
        return response.IsSuccessStatusCode;
    }
}
