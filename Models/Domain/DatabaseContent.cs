using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace OnlineBookStore.Models.Domain
{
    public class DatabaseContent:IdentityDbContext<DefaultUser>
    {
        public DatabaseContent(DbContextOptions<DatabaseContent> options):base(options) 
        {
        }
        public DbSet<Genre> Genere { get; set; }
        public DbSet<Author>  Author { get; set; }
        public DbSet<Publisher> Publisher { get; set; }
        public DbSet<Book> Book { get; set; }
    }
}
