using LoquatDocs.EntityFramework.Model;
using Microsoft.EntityFrameworkCore;

namespace LoquatDocs.EntityFramework {
  public class LoquatDocsDbContext : DbContext {

    public string DbPath { get; private set; }

    public DbSet<Document> Documents { get; set; }

    public LoquatDocsDbContext(string dbPath, bool createNewDatabase = false) {
      DbPath = dbPath;
      
      if (createNewDatabase) {
        CreateNewDatabase();
      }
    }

    public void CreateNewDatabase() {
      Database.Migrate();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
      optionsBuilder.UseSqlite($"Data Source={DbPath}");
    }
  }
}
