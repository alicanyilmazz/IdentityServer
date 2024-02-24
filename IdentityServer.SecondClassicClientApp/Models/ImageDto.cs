namespace IdentityServer.Client2.Models
{
    public class ImageDto
    {
        public List<Image> Data { get; set; }
        public int StatusCode { get; set; }
        public string? Error { get; set; }
    }
    public class Image
    {
        public string? Path { get; set; }
    }
}
