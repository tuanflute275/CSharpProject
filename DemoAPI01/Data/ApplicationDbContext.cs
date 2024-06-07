using DemoAPI01.Models.Domains;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace DemoAPI01.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}
