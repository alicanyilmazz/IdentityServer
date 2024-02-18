namespace IdentityServer.Client3.Models
{
    public class CookieInformationDto
    {
        public List<CookieClaims> CookieClaims { get; set; } = new List<CookieClaims>();
        public List<CookieAuthenticationProperties> CookieAuthenticationProperties { get; set; } = new List<CookieAuthenticationProperties>();
    }
    public class CookieClaims
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
    public class CookieAuthenticationProperties
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
