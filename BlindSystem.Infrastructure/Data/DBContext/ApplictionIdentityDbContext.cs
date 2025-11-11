using BlindSystem.Identities.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BlindSystem.Identities.IdentitiesDbContext
{
    public class ApplictionIdentityDbContext : IdentityDbContext<ApplicationUser>
    {
        protected ApplictionIdentityDbContext()
        {
        }
        public ApplictionIdentityDbContext(DbContextOptions options) : base(options)
        {

        }
        //Add-Migration SeedDevices -Context ApplictionIdentityDbContext
        //Update-Database -Context ApplictionIdentityDbContext

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }




    }

}

