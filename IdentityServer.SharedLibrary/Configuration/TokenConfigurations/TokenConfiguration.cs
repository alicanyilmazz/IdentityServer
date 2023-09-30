using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityServer.SharedLibrary.Configuration.TokenConfigurations
{
    public class TokenConfiguration
    {
        public List<string>? Audience { get; set; }
        public string? Authority { get; set; }
    }
}
