using CounterInfo.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace CounterInfo.Server
{
    class TodoDb : DbContext
    {
        public TodoDb(DbContextOptions<TodoDb> options)
            : base(options) {
        
        }

        public DbSet<Todo> Todos => Set<Todo>();


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Todo>().ToTable("Todo");
        }
    }
}
