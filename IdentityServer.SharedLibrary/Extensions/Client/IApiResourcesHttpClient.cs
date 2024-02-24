using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityServer.SharedLibrary.Extensions.Client
{
    public interface IApiResourcesHttpClient
    {
        Task<HttpClient> GetHttpClientAsync();
    }
}
