using LoquatDocs.EntityFramework.Model;
using Microsoft.EntityFrameworkCore;

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

    public LoquatDocsDbContext(string dbPath = DEFAULT_DATABASE_PATH, bool createOrUpdateDatabase = false) {
      DbPath = dbPath;
      
      if (createOrUpdateDatabase) {
        CreateOrUpdateDatabase();
      }
    }

    public void CreateOrUpdateDatabase() {
      Database.Migrate();
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
        new Group() { Groupname = "Tax" },
        new Group() { Groupname = "Banking" },
        new Group() { Groupname = "Contracts" },
        new Group() { Groupname = "Career" }
        );
    }
  }
}
