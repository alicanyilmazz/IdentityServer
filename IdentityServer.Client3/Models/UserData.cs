namespace IdentityServer.Client3.Models
{
    public class UserData
    {
        public bool IsCookieDataAvailable { get; set; }
        public bool IsMovieDataAvailable { get; set; }
        public MovieDto MovieData { get; set; }
        public CookieInformationDto CookieInformation { get; set; }
    }
}
