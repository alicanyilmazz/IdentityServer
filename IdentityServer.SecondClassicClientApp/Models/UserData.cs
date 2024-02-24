namespace IdentityServer.Client2.Models
{
    public class UserData
    {
        public bool IsCookieDataAvailable { get; set; }
        public bool IsImageDataAvailable { get; set; }
        public ImageDto ImageData { get; set; }
        public CookieInformationDto CookieInformation { get; set; }
    }
}
