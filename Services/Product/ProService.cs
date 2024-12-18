 
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Azure;
using Newtonsoft.Json;  
using WebStore.Models;
using WebStore.Services;

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
            var response = await _httpClient.GetStringAsync($"http://localhost:3000/api/products/{id}");

        // Deserialize trực tiếp dữ liệu trả về thành một đối tượng Users
        var product = JsonConvert.DeserializeObject<Product>(response);

        // Trả về đối tượng người dùng
        return product;
    }



    // Lấy tất cả
    public async Task<IEnumerable<Product>> GetAllPro(string? token)
    {
        // Đảm bảo token được gắn vào header Authorization
        _httpClient.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        try
        {
            // Gửi yêu cầu GET tới API
            var response = await _httpClient.GetAsync("http://localhost:3000/api/products/");

            // Kiểm tra trạng thái phản hồi
            if (response.IsSuccessStatusCode)
            {
                // Nếu thành công, đọc nội dung và giải mã JSON
                var responseContent = await response.Content.ReadAsStringAsync();
                var apiResponse = JsonConvert.DeserializeObject<ApiResponses>(responseContent);

                // Trả về danh sách sản phẩm
                return apiResponse.data;
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



    //// Cập nhật thông tin
    //public async Task<bool> UpdatePro(string id, Product product)
    //{
   
    //    var json = JsonConvert.SerializeObject(product);
    //    var content = new StringContent(json, Encoding.UTF8, "application/json");
 
    //    HttpResponseMessage response;
    //    try
    //    {
    //        response = await _httpClient.PutAsync($"http://localhost:3000/api/products/{id}", content);
 
    //        var responseContent = await response.Content.ReadAsStringAsync();
    //     }
    //    catch (Exception ex)
    //    {
    //        Console.WriteLine($"Debug: Exception occurred: {ex.Message}");
    //        return false;
    //    }
    //    return response.IsSuccessStatusCode;
    //}

    public async Task<bool> UpdatePro(string id, Product user)
    {
        Console.WriteLine($"Debug: Entering UpdateUser method with ID: {id}");

        var json = JsonConvert.SerializeObject(user);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        Console.WriteLine($"Debug: JSON content being sent: {json}");

        HttpResponseMessage response;
        try
        {
            response = await _httpClient.PutAsync($"http://localhost:3000/api/products/{id}", content);
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
            var response = await _httpClient.DeleteAsync($"http://localhost:3000/api/products/{id}");
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

    public async Task<bool> CreatePro(Product product)
    {
        var json = JsonConvert.SerializeObject(product);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

            var   response = await _httpClient.PostAsync("http://localhost:3000/api/products/", content);
        return response.IsSuccessStatusCode;
    }
    // Tạo thông tin
    //public async Task<bool> CreatePro(Product product)
    //{
    //    // Serialize the user object to JSON
    //    var json = JsonConvert.SerializeObject(product);
    //    var content = new StringContent(json, Encoding.UTF8, "application/json");

    //    HttpResponseMessage response;
    //    try
    //    {
    //        // Debug: Log the JSON payload being sent
    //        Console.WriteLine($"Debug: Sending JSON to API: {json}");

    //        // Send a POST request to the Node.js API
    //        response = await _httpClient.PostAsync("http://localhost:3000/api/products/", content);

    //        // Debug: Log the response status code
    //        Console.WriteLine($"Debug: Received response with status code: {response.StatusCode}");

    //        // Debug: Log the response content if the request was not successful
    //        if (!response.IsSuccessStatusCode)
    //        {
    //            var responseContent = await response.Content.ReadAsStringAsync();
    //            Console.WriteLine($"Debug: Response content: {responseContent}");
    //        }

    //        return response.IsSuccessStatusCode;
    //    }
    //    catch (Exception ex)
    //    {
    //        // Log the exception for debugging purposes
    //        Console.WriteLine($"Error: Exception occurred: {ex.Message}");
    //        return false;
    //    }
    //}




}
