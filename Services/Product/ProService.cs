 
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;  
using WebStore.Models;

public class ProService
{
    private readonly HttpClient _httpClient;

    public ProService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    // Lấy thông tin theo ID
    public async Task<Product> GetProById(string id)
    {
        var response = await _httpClient.GetStringAsync($"http://localhost:3000/api/products/info/{id}");
        return JsonConvert.DeserializeObject<Product>(response);
    }

    // Lấy tất cả
    public async Task<IEnumerable<Product>> GetAllPro()
    {
        var response = await _httpClient.GetStringAsync($"http://localhost:3000/api/products");
        return JsonConvert.DeserializeObject<IEnumerable<Product>>(response);
    }

    // Cập nhật thông tin
    public async Task<bool> UpdatePro(string id, Product user)
    {
        Console.WriteLine($"Debug: Entering UpdateUser method with ID: {id}");
        Console.WriteLine($"Debug: User data to be updated - Email: {user.TEN} , PhoneNumber:  {user.PRICE}");

        var json = JsonConvert.SerializeObject(user);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        Console.WriteLine($"Debug: JSON content being sent: {json}");

        HttpResponseMessage response;
        try
        {
            response = await _httpClient.PutAsync($"http://localhost:3000/api/products/update/all/{id}", content);
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



    // xóa thông tin 
    public async Task<bool> DeletePro(string id)
    {
        try
        {
            var response = await _httpClient.DeleteAsync($"http://localhost:3000/api/products/delete/{id}");
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

    // Tạo thông tin
    public async Task<bool> CreatePro(Product user)
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
            response = await _httpClient.PostAsync("http://localhost:3000/api/products/create", content);

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
