using CounterInfo.Server.Models;
using CounterInfoServer.Models;
using Microsoft.EntityFrameworkCore;

namespace CounterInfo.Server
{
    class TodoDb : DbContext
    {
        public TodoDb(DbContextOptions<TodoDb> options)
            : base(options) {
        
        }

        public DbSet<Todo> Todos => Set<Todo>();
        public DbSet<CounterInfomation> CounterInfomations => Set<CounterInfomation>();


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Todo>().ToTable("Todo");
            modelBuilder.Entity<CounterInfomation>().ToTable("CounterInfomation");
        }
    }
}
