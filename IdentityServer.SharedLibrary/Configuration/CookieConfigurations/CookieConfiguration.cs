using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityServer.SharedLibrary.Configuration.CookieConfigurations
{
    public class CookieConfiguration
    {
        public string? DefaultScheme { get; set; }
        public string? DefaultChallengeScheme { get; set; }
        public string? Authority { get; set; }
        public string? ClientId { get; set; }
        public string? ClientSecret { get; set; }
        public string? ReturnType { get; set; }
    }
}
