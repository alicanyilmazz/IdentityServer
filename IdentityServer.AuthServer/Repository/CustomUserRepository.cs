using IdentityServer.AuthServer.Models;
using Microsoft.EntityFrameworkCore;
using static IdentityServer4.Models.IdentityResources;

namespace IdentityServer.AuthServer.Repository
{
    public class CustomUserRepository : ICustomUserRepository
    {
        private readonly CustomDbContext _customDbContext;

        public CustomUserRepository(CustomDbContext customDbContext)
        {
            _customDbContext = customDbContext;
        }

        public async Task<CustomUser> FindByEmail(string email)
        {
            return await _customDbContext.CustomUsers.FirstOrDefaultAsync(x => x.Email == email);
        }

        public async Task<CustomUser> FindById(int id)
        {
            return await _customDbContext.CustomUsers.FindAsync(id);
        }

        public async Task<bool> Validate(string email, string password)
        {
            return await _customDbContext.CustomUsers.AnyAsync(x=>x.Email == email && x.Password == password);
        }
    }
}
