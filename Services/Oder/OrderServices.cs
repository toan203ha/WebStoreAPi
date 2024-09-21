using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WebStore.Models;

namespace WebStore.Services.Oder
{
    public class OrderServices
    {
        private readonly HttpClient _httpClient;

        public OrderServices(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Đổi từ Product sang Oder
        public async Task<IEnumerable<Oders>> GetAllOders()
        {
            try
            {
                var response = await _httpClient.GetStringAsync($"http://localhost:3000/api/donhang/all");
                return JsonConvert.DeserializeObject<IEnumerable<Oders>>(response);
            }
            catch (HttpRequestException e)
            {
                // Xử lý lỗi yêu cầu HTTP
                Console.WriteLine($"Request error: {e.Message}");
                return Enumerable.Empty<Oders>();
            }
            catch (Exception e)
            {
                // Xử lý lỗi khác
                Console.WriteLine($"Unexpected error: {e.Message}");
                return Enumerable.Empty<Oders>();
            }
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
