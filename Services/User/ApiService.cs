using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;  
using WebStore.Models;

public class ApiService
{
    private readonly HttpClient _httpClient;

    public ApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    // Lấy thông tin người dùng theo ID
    public async Task<Users> GetUserById(string id)
    {
        var response = await _httpClient.GetStringAsync($"http://localhost:3000/api/users/{id}");
        return JsonConvert.DeserializeObject<Users>(response);
    }

    // Lấy tất cả người dùng
    public async Task<IEnumerable<Users>> GetAllUser()
    {
        var response = await _httpClient.GetStringAsync($"http://localhost:3000/api/all/users");
        return JsonConvert.DeserializeObject<IEnumerable<Users>>(response);
    }

    // Cập nhật thông tin người dùng
    public async Task<bool> UpdateUser(string id, Users user)
    {
        Console.WriteLine($"Debug: Entering UpdateUser method with ID: {id}");
        Console.WriteLine($"Debug: User data to be updated - Email: {user.Email}, PhoneNumber: {user.PhoneNumber}, Img: {user.Img}");

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
    // xóa thông tin người dùng
    public async Task<bool> DeleteUser(string id)
    {
        try
        {
            var response = await _httpClient.DeleteAsync($"http://localhost:3000/api/users/delete/{id}");
            Console.WriteLine($"Debug: Received response with status code: {response.StatusCode}");
            var responseContent = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Debug: Response content: {responseContent}");
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Debug: Exception occurred: {ex.Message}");
            return false;
        }
    }

    public async Task<bool> CreateUser(Users user)
    {
        // Serialize the user object to JSON
        var json = JsonConvert.SerializeObject(user);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        HttpResponseMessage response;
        try
        {
            // Debug: Log the JSON payload being sent
            Console.WriteLine($"Debug: Sending JSON to API: {json}");

            // Send a POST request to the Node.js API
            response = await _httpClient.PostAsync("http://localhost:3000/api/users/create", content);

            // Debug: Log the response status code
            Console.WriteLine($"Debug: Received response with status code: {response.StatusCode}");

            // Debug: Log the response content if the request was not successful
            if (!response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Debug: Response content: {responseContent}");
            }

            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            // Log the exception for debugging purposes
            Console.WriteLine($"Error: Exception occurred: {ex.Message}");
            return false;
        }
    }




}
