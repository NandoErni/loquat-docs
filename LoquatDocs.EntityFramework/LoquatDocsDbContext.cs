using LoquatDocs.EntityFramework.Model;
using Microsoft.EntityFrameworkCore;

namespace LoquatDocs.EntityFramework {
  public class LoquatDocsDbContext : DbContext {

    public string DbPath { get; private set; }

    public DbSet<Document> Documents { get; set; }

    public LoquatDocsDbContext(string dbPath) {
      DbPath = dbPath;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
      optionsBuilder.UseSqlite(DbPath); // todo: db path
    }
  }
}
