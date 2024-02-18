namespace IdentityServer.Client3.Models
{
    public class NoDataContent<T>
    {
        public T Data { get; set; }
        public int StatusCode { get; set; }
        public string? Error { get; set; }
    }
}
