namespace IdentityServer.Client3.Models
{
    public class MovieDto
    {
        public List<Movie> Data { get; set; }
        public int StatusCode { get; set; }
        public string? Error { get; set; }
    }
    public class Movie
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string? ImageUrl { get; set; }
        public string? Score { get; set; }
    }
}
