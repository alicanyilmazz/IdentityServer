using Microsoft.EntityFrameworkCore;

namespace IdentityServer.AuthServer.Models
{
    /*
     Generally, it is not recommended to use it with the IdentityServer Custom Membership system, 
     so we will briefly show this branch to show how to do it. For this reason, we will add the 
     users while the DB is running, if you want, you can define a form page and import them. 
     Again, passwords should not be saved openly in the db, but as we said, we do not focus on these. 
     If you use Microsoft Identity, it will already do these operations for you.    
     ! We are just showing you how to communicate with Identity Server and your Custom Membership system.
     */
    public class CustomDbContext : DbContext
    {
        public DbSet<CustomUser> CustomUsers { get; set; }
        public CustomDbContext(DbContextOptions options) : base(options)
        {
            
        }

        //
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CustomUser>().HasData(new CustomUser
            {
                Id = 1,
                Email = "testalicanyilmaz@gmail.com",
                Password = "password",
                City = "Istanbul",
                Username = "alican04"
            }, 
            new CustomUser
            {
                Id = 2,
                Email = "test2alicanyilmaz@gmail.com",
                Password = "password2",
                City = "Istanbul",
                Username = "alican02"
            });
            base.OnModelCreating(modelBuilder);
        }
    }
}
