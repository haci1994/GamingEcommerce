using GamingEcommerce.DAL.DataContext.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace GamingEcommerce.DAL.DataContext
{
    public class DataInitializer
    {
        private readonly AppDbContext _dbContext;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;

        public DataInitializer(AppDbContext dbContext, RoleManager<IdentityRole> roleManager, UserManager<AppUser> userManager)
        {
            _dbContext = dbContext;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task Initialize()
        {
            await CreateSuperAdmin();
            await _dbContext.Database.MigrateAsync();
        }

        public async Task CreateSuperAdmin()
        {
            List<string> roles = ["Admin", "Client"];

            foreach (string role in roles)
            {
                var existRole = await _roleManager.RoleExistsAsync(role);

                if(existRole) continue;

                await _roleManager.CreateAsync(new IdentityRole { Name = role });
            }

            var existUser = await _userManager.FindByNameAsync("superadmin");

            if (existUser != null) return;

            var superAdmin = new AppUser
            {
                UserName = "superadmin",
                FirstName = "superadmin",
                LastName= "superadmin"
            };

            var result = await _userManager.CreateAsync(superAdmin, "1234");

            if(!result.Succeeded) return;

            await _userManager.AddToRoleAsync(superAdmin, "superadmin");
        }
    }
}
