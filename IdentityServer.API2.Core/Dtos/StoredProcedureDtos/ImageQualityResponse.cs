using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityServer.API2.Core.Dtos.StoredProcedureDtos
{
    public class ImageQualityResponse
    {
        public string Name { get; set; }
        public int Rate { get; set; }
        public int ResizeWidth { get; set; }
        public bool IsOriginal { get; set; }
    }
}
