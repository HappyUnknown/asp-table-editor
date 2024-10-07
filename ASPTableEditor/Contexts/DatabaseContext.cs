using ASPTableEditor.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace ASPTableEditor.Contexts
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        // Define DbSets for your entities (tables)
        public DbSet<Employee> Employees { get; set; }

        // Override OnModelCreating to configure model relationships and constraints
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Customize entity configurations if needed
        }
    }
}
