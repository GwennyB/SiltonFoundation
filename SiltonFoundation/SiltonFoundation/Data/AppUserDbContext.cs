using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SiltonFoundation.Models;

namespace SiltonFoundation.Data
{

    // This class reflects the database context for the 'SiltonUser' database. It is derived from the Microsoft Identity library for authentication, and build relies on Entity Framework Core (ORM).
    public class AppUserDbContext : IdentityDbContext<AppUser>
    {
        public AppUserDbContext(DbContextOptions<AppUserDbContext> options) : base(options)
        {

        }



    }
}
