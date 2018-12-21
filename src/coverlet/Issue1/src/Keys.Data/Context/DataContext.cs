using System.Diagnostics.CodeAnalysis;
using Keys.Data.Context.Contracts;
using Keys.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Keys.Data.Context
{
    [ExcludeFromCodeCoverage]
    public class DataContext : DbContext, IDataContext
    {
        public virtual DbSet<Key> Keys { get; set; }

        public DataContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Key>()
                .HasIndex(k => new {k.KeyType, k.Uuid})
                .IsUnique();

            builder.Entity<Key>()
                .HasIndex(k => new { k.KeyType, k.Kid })
                .IsUnique();
        }
    }
}
