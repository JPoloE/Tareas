using Microsoft.EntityFrameworkCore;

namespace Tareas.Models
{
    public class ItemDBContext : DbContext
    {
        public ItemDBContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Items> Items { get; set; }
    }
}
