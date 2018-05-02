using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TravelWebApp.Models;

namespace TravelWebApp.Services
{
    public class GrandeTravelDbContext : IdentityDbContext
    {
        public DbSet<Profile> ProfileTbl { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB; Database=GrandeTravelDB; Trusted_Connection=True");
        }
    }
}
