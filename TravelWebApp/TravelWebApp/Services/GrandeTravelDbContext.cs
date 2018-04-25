using DataService;
using Microsoft.EntityFrameworkCore;

namespace TravelWebApp.Services
{
    public class GrandeTravelDbContext : AppDbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB; Database=GrandeTravelDB; Trusted_Connection=True");
        }
    }
}
