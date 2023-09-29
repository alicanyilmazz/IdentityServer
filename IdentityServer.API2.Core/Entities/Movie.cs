using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityServer.API2.Core.Entities
{
    public class Movie
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public DateTime ReleaseDate { get; set; }
    }
}
