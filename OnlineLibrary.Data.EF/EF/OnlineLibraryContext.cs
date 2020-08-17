using Microsoft.EntityFrameworkCore;
using OnlineLibrary.Data.EF.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineLibrary.Data.EF.EF
{
    public class OnlineLibraryContext : DbContext
    {
        public OnlineLibraryContext(string connectionString) : base(_getContextOptions(connectionString))
        {

        }
        private static DbContextOptions<OnlineLibraryContext> _getContextOptions(string connectionString)
        {
            var optionsBuilder = new DbContextOptionsBuilder<OnlineLibraryContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return optionsBuilder.Options;
        }
        public DbSet<Book> Books { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>(builder =>
            {
                builder.HasKey(x => x.Id);
                builder.Property(x => x.Name)
                    .IsRequired(true)
                    .HasMaxLength(150);

                builder.Property(x => x.Author)
                    .IsRequired(true)
                    .HasMaxLength(150);

                builder.Property(x => x.ShortDescription)
                    .IsRequired(true)
                    .HasMaxLength(500);

                builder.Property(x => x.Body)
                    .IsRequired(true)
                    .HasMaxLength(10000);

                builder.Property(x => x.CoverImageName)
                    .IsRequired(true)
                    .HasMaxLength(50);

                builder.Property(x => x.ReleaseYear)
                    .IsRequired(true);
            });
        }
    }
}
