using WebStore.Models;

namespace WebStore.Services
{
    public class ApiResponses
    {
        public bool isEmpty { get; set; }
        public List<Product> data { get; set; }
    }
    public class ApiResponsesOder
    {
        public bool isEmpty { get; set; }
        public List<Oders> data { get; set; }
    }
}
