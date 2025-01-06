using Microsoft.EntityFrameworkCore;
using SecureAuthSystem.Models;
using System.Collections.Generic;

namespace SecureAuthSystem.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
    }
}
