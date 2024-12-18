using System.Text;
using Newtonsoft.Json;
using WebStore.Models; 
using System.Text.Json;
 
namespace WebStore.Services.Oder
{
    public class OrderServices
    {
        private readonly HttpClient _httpClient;

        public OrderServices(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("http://localhost:3000/api/"); 

        }


        // Lấy tất cả
        public async Task<IEnumerable<Oders>> GetAllOders(string? token)
        {
            // Đảm bảo token được gắn vào header Authorization
            _httpClient.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            try
            {
                // Gửi yêu cầu GET tới API
                var response = await _httpClient.GetAsync("http://localhost:3000/api/oderv2/");
              
                // Kiểm tra trạng thái phản hồi
                if (response.IsSuccessStatusCode)
                {
                    // Nếu thành công, đọc nội dung và giải mã JSON
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var apiResponse = JsonConvert.DeserializeObject<ApiResponsesOder>(responseContent);

                    // Trả về danh sách 
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

        public async Task<bool> UpdateOder(string id, Oders oders)
        {
            Console.WriteLine($"Debug: Entering UpdateUser method with ID: {id}");

            // Kiểm tra xem các trường quan trọng có giá trị hợp lệ hay không
            if (oders == null)
            {
                Console.WriteLine("Debug: Oders object is null");
                return false;
            }

   

            var json = JsonConvert.SerializeObject(oders);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            Console.WriteLine($"Debug: JSON content being sent: {json}");

            HttpResponseMessage response;
            try
            {
                response = await _httpClient.PutAsync($"http://localhost:3000/api/oderv2/orders/{id}", content);

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




        public async Task<bool> CancelOrder(string id, Oders order)
        {
            Console.WriteLine($"Debug: Entering UpdateUser method with ID: {id}");
            Console.WriteLine($"Debug: Order data to be updated - id: {order._id}");

            var json = JsonConvert.SerializeObject(order);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            Console.WriteLine($"Debug: JSON content being sent: {json}");

            HttpResponseMessage response;
            try
            {
                response = await _httpClient.PutAsync($"http://localhost:3000/api/donhang/cancel/{id}", content);
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
        public async Task<bool> RestoreOrder(string id, Oders order)
        {
            Console.WriteLine($"Debug: Entering UpdateUser method with ID: {id}");
            Console.WriteLine($"Debug: Order data to be updated - id: {order._id}");

            var json = JsonConvert.SerializeObject(order);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            Console.WriteLine($"Debug: JSON content being sent: {json}");

            HttpResponseMessage response;
            try
            {
                response = await _httpClient.PutAsync($"http://localhost:3000/api/donhang/restore/{id}", content);
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
    }
}
