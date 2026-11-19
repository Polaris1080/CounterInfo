namespace CounterInfo.Server;

using CounterInfo.Server.Models;
using Microsoft.EntityFrameworkCore;



class TodoDb : DbContext
{
    public TodoDb(DbContextOptions<TodoDb> options) : base(options)
    {

    }

    #region Sets
    public DbSet<Todo> Todos => Set<Todo>();
    public DbSet<CounterInfoData> CounterInfoData => Set<CounterInfoData>();
    public DbSet<CounterInfomation> CounterInfomations => Set<CounterInfomation>();
    public DbSet<CounterSetting> CounterSettings => Set<CounterSetting>();
    #endregion

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Todo>().ToTable("Todo");
        modelBuilder.Entity<CounterInfoData>().ToTable(t => t.ExcludeFromMigrations());
        modelBuilder.Entity<CounterInfomation>().ToTable("CounterInfomation");
        modelBuilder.Entity<CounterSetting>().ToTable(nameof(CounterSetting));
    }
}
