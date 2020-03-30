using Microsoft.EntityFrameworkCore;
using System;

namespace Database
{
    public class DataContext : DbContext
    {
        public DataContext()
        {
        }

        public DataContext(DbContextOptions<DataContext> config) : base(config)
        {
        }

        public DbSet<Models.Customer> Customers { get; set; }
        public DbSet<Models.Article> Articles { get; set; }
        public DbSet<Models.Sale> Sales { get; set; }
        public DbSet<Models.SaleArticle> SaleArticles { get; set; }
        public DbSet<Models.User> Users { get; set; }

        internal static object BeginTransaction()
        {
            throw new NotImplementedException();
        }
    }
}
