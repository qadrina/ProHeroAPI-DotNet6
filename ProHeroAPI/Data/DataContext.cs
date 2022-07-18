using Microsoft.EntityFrameworkCore;
namespace ProHeroAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        public DbSet<ProHero> ProHeroes { get; set; } //ProHeroes is the table name in DB
    }
}
