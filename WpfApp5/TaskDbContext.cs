using Microsoft.EntityFrameworkCore;
using WpfApp5.Models;
using Microsoft.EntityFrameworkCore.Sqlite;

namespace WpfApp5.DataAccess
{
    public class TaskDbContext : DbContext
    {
        public DbSet<TaskItem> Tasks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite("Data Source=tasks.db");
            }
        }
    }
}
