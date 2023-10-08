namespace IdentityServer.Client1.Models
{
    public class ProductDto
    {
        public List<Product> Data { get; set; }
        public int StatusCode { get; set; }
        public string? Error { get; set; }
    }
    public class Product
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public decimal Price { get; set; }
    }
}
