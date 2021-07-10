using LoquatDocs.EntityFramework.Model;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace LoquatDocs.EntityFramework {
  public class LoquatDocsDbContext : DbContext {

    private const string DEFAULT_DATABASE_PATH = @"D:\Projects\Visual Studio\LoquatDocs\LoquatDocs.EntityFramework\mydata.db";

    public string DbPath { get; private set; }

    public DbSet<Document> Documents { get; set; }
    public DbSet<Group> Groups { get; set; }
    public DbSet<Alias> Aliases { get; set; }
    public DbSet<AliasGroup> AliasGroups { get; set; }
    public DbSet<Invoice> Invoices { get; set; }
    public DbSet<Tag> Tags { get; set; }

    public LoquatDocsDbContext(string dbPath = DEFAULT_DATABASE_PATH) { // todo: delete default string
      DbPath = dbPath;
    }

    public async Task CreateOrUpdateDatabaseAsync() {
      await Database.MigrateAsync();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
      optionsBuilder.UseSqlite($"Data Source={DbPath}");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
      modelBuilder.Entity<Alias>()
        .HasKey(nameof(Alias.AliasName), nameof(Alias.AliasGroupName));

      SeedData(modelBuilder);
    }

    private void SeedData(ModelBuilder modelBuilder) {
      modelBuilder.Entity<Group>().HasData(
        new Group() { Groupname = "Miscellaneous" }
        );
    }
  }
}
