using System;
using Azure;
using Microsoft.EntityFrameworkCore;

namespace RelEF
{
    public class MyDbContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<Tag> Tags { get; set; }

        string ConnectionString = "Data Source=localhost; Initial Catalog=RelEf; Integrated Security=false; ***; TrustServerCertificate=True;";
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionString);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }


    }
}