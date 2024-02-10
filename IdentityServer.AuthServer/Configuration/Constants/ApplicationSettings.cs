using System.Text;
using static System.Net.WebRequestMethods;

namespace IdentityServer.AuthServer.Configuration.Constants
{
    public class IdentityServerEndpoints
    {
        public static class Endpoint
        {
            public const string SIGN_IN_OIDC = "signin-oidc";
            public const string SIGN_OUT_CALLBACK_OIDC = "signout-callback-oidc";
        }
    }
    public enum ApplicationCode
    {
        Client2Mvc,
        Client3Mvc
    }
    public class ApplicationSettings
    {
        public static class Address
        {
            public const string CLIENT_2_MVC = "https://localhost:7266/";
            public const string CLIENT_3_MVC = "https://localhost:7290/";
        }
    }
    public class GetEndpoint
    {
        public static List<string> GetRedirectUris(List<ApplicationCode> application)
        {
            StringBuilder endpoint = new();
            List<string> list = new();
            foreach (ApplicationCode applicationCode in application)
            {
                switch (applicationCode)
                {
                    case ApplicationCode.Client2Mvc:
                        endpoint.Append(ApplicationSettings.Address.CLIENT_2_MVC);
                        break;
                    case ApplicationCode.Client3Mvc:
                        endpoint.Append(ApplicationSettings.Address.CLIENT_3_MVC);
                        break;
                    default:
                        throw new ArgumentException("Application endpoint could not found!");
                }
                endpoint.Append(IdentityServerEndpoints.Endpoint.SIGN_IN_OIDC);
                list.Add(endpoint.ToString());
                endpoint.Clear();
            }
            return list; 
        }
        public static List<string> GetPostLogoutRedirectUris(List<ApplicationCode> application)
        {            
            List<string> list = new();
            StringBuilder endpoint = new();
            foreach (var applicationCode in application)
            {
                switch (applicationCode)
                {
                    case ApplicationCode.Client2Mvc:
                        endpoint.Append(ApplicationSettings.Address.CLIENT_2_MVC);                     
                        break;
                    case ApplicationCode.Client3Mvc:
                        endpoint.Append(ApplicationSettings.Address.CLIENT_3_MVC);
                        break;
                    default:
                        throw new ArgumentException("Application endpoint could not found!");
                }
                endpoint.Append(IdentityServerEndpoints.Endpoint.SIGN_OUT_CALLBACK_OIDC);
                list.Add(endpoint.ToString());
                endpoint.Clear();
            }
            return list;
        }
    }

}
