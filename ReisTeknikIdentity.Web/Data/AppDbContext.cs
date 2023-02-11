using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ReisTeknikIdentity.Web.IdentityModels;
using ReisTeknikIdentity.Web.Models;
using System.Reflection.Emit;
using System.Security.Policy;

namespace ReisTeknikIdentity.Web.Data
{
    public class AppDbContext : IdentityDbContext<AppUser, AppRole,string>
    {

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {



        }

        public DbSet<Image> Images { get; set; }
        public DbSet<Logo> Logos { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<Contact> Contacts { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            var role = new AppRole()
            {
                Name = "Admin",
                NormalizedName="ADMIN"
            };

           // pass is Fe159-+
            var user = new AppUser()
            {
                Email = "fkekinci@gmail.com",
                UserName = "eternity",
                NormalizedEmail = "FKEKINCI@GMAIL.COM",
                NormalizedUserName = "ETERNITY",
                PasswordHash = "AQAAAAIAAYagAAAAEBoDN9Pe7XeIFGjkmkUFLfo20iHUIWvDx+oO94SRWBjSuHe0p70jsBdfb2GbXvgPjw==",
                EmailConfirmed = true
            };
            var userReis =  new AppUser()
            {
                Email = "reis@reisteknik.com",
                UserName = "reis",
                NormalizedEmail = "REIS@REISTEKNIK.COM",
                NormalizedUserName = "REIS",
                PasswordHash = "AQAAAAIAAYagAAAAEBoDN9Pe7XeIFGjkmkUFLfo20iHUIWvDx+oO94SRWBjSuHe0p70jsBdfb2GbXvgPjw==", 
                EmailConfirmed = true
            };

           

            IdentityUserRole<string> userRole = new IdentityUserRole<string>()
            {               
                UserId = user.Id,
                RoleId = role.Id                
            };
            IdentityUserRole<string> userRole1 = new IdentityUserRole<string>()
            {
                UserId = userReis.Id,
                RoleId = role.Id
            };

            builder.Entity<AppRole>().HasData(role);

            builder.Entity<AppUser>().HasData(user,userReis);
            builder.Entity<IdentityUserRole<string>>().HasNoKey().HasData(userRole, userRole1);

            base.OnModelCreating(builder);
            
           
        }





    }
}
