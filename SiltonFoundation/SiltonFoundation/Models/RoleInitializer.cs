using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SiltonFoundation.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SiltonFoundation.Models
{
    public class RoleInitializer
    {
        private static readonly List<IdentityRole> Roles = new List<IdentityRole>()
        {
            new IdentityRole{Name = AppRoles.General, NormalizedName = AppRoles.General.ToUpper(), ConcurrencyStamp = Guid.NewGuid().ToString()},
            new IdentityRole{Name = AppRoles.Applicant, NormalizedName = AppRoles.Applicant.ToUpper(), ConcurrencyStamp = Guid.NewGuid().ToString()},
            new IdentityRole{Name = AppRoles.Evaluator, NormalizedName = AppRoles.Evaluator.ToUpper(), ConcurrencyStamp = Guid.NewGuid().ToString()},
            new IdentityRole{Name = AppRoles.Admin, NormalizedName = AppRoles.Admin.ToUpper(), ConcurrencyStamp = Guid.NewGuid().ToString()}
        };

        public static void SeedData(IServiceProvider servProv)
        {
            using (var dbContext = new AppUserDbContext(servProv.GetRequiredService<DbContextOptions<AppUserDbContext>>()))
            {
                dbContext.Database.EnsureCreated();
                AddRoles(dbContext);
            }
        }

        private static void AddRoles(AppUserDbContext context)
        {
            if (context.Roles.Any()) return;

            foreach (var role in Roles)
            {
                context.Roles.Add(role);
                context.SaveChanges();
            }
        }
    }
}



