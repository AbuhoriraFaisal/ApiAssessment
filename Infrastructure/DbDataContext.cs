using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class DbDataContext : DbContext
    {
        public DbDataContext(DbContextOptions<DbDataContext> options)
            : base(options)
        {

        }
         
           
        public DbSet<Customer> Customer { get; set; }
        public DbSet<Invoice> Invoice { get; set; }
    }
}
