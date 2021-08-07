using LoquatDocs.EntityFramework;
using LoquatDocs.EntityFramework.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoquatDocs.ViewModel.Repository {
  public class LoquatDocsDbRepository {

    private Config.Config _config = new Config.Config();

    private LoquatDocsDbContext GetNewDbContext() {
      return new LoquatDocsDbContext(_config.DatabaseFilePath);
    }

    public async Task<List<string>> GetAllGroupnames() {
      using (LoquatDocsDbContext ctx = GetNewDbContext()) {
        return await ctx.Groups.Select(g => g.Groupname).ToListAsync();
      }
    }

    public async Task<List<string>> GetAllTagIds() {
      using (LoquatDocsDbContext ctx = GetNewDbContext()) {
        return await ctx.Tags.Select(g => g.TagId).ToListAsync();
      }
    }

    public async Task SaveGroup(string groupName) {
      if (string.IsNullOrWhiteSpace(groupName)) {
        return;
      }

      using (LoquatDocsDbContext ctx = GetNewDbContext()) {
        await ctx.Groups.AddAsync(new Group() { Groupname = groupName });
        await ctx.SaveChangesAsync();
      }
    }

    public async Task DeleteGroup(string groupName) {
      using (LoquatDocsDbContext ctx = GetNewDbContext()) {
        var groupToDelete = await ctx.Groups.FirstOrDefaultAsync(g => g.Groupname.Equals(groupName));
        
        if (groupToDelete is null) {
          return;
        }

        ctx.Groups.Remove(groupToDelete);
        await ctx.SaveChangesAsync();
      }
    }

    public async Task<bool> GroupExist(string groupName) {
      using (LoquatDocsDbContext ctx = GetNewDbContext()) {
        return await ctx.Groups.AnyAsync(g => g.Groupname.Equals(groupName));
      }
    }

    public async Task<List<Document>> GetAllDocumentsOfGroup(string groupName) {
      using (LoquatDocsDbContext ctx = GetNewDbContext()) {
        return await ctx.Documents.Where(document => document.Groupname.Equals(groupName)).ToListAsync();
      }
    }

    public async Task RemoveDocuments(List<Document> documentsOfGroup) {
      using(LoquatDocsDbContext ctx = GetNewDbContext()) {
        ctx.Documents.RemoveRange(documentsOfGroup);
        await ctx.SaveChangesAsync();
      }
    }

    public async Task SaveDocument(Document document) {
      using (LoquatDocsDbContext ctx = GetNewDbContext()) {
        await ctx.Documents.AddAsync(document);
        await ctx.SaveChangesAsync();
      }
    }

    public async Task SaveTags(List<Tag> tags) {
      using (LoquatDocsDbContext ctx = GetNewDbContext()) {
        await ctx.Tags.AddRangeAsync(tags);
        await ctx.SaveChangesAsync();
      }
    }

    public async Task SaveInvoice(Invoice invoice) {
      using (LoquatDocsDbContext ctx = GetNewDbContext()) {
        await ctx.Invoices.AddAsync(invoice);
        await ctx.SaveChangesAsync();
      }
    }

    public async Task<bool> DocumentExist(string documentPath) {
      using (LoquatDocsDbContext ctx = GetNewDbContext()) {
        return await ctx.Documents.AnyAsync(d => d.DocumentPath.Equals(documentPath));
      }
    }

    public List<IGrouping<string, Document>> GetDocuments(Func<Document, bool> predicate) {
      using (LoquatDocsDbContext ctx = GetNewDbContext()) {
        return ctx.Documents.Include(d => d.Invoices).Include(d => d.Tags).AsEnumerable()
         .Where(d => predicate(d))
         .GroupBy(d => d.Groupname).ToList();
      }
    }

    public async Task<Document> GetDocument(string documentPath) {

      using (LoquatDocsDbContext ctx = GetNewDbContext()) {
        return await ctx.Documents.FirstOrDefaultAsync(d => d.DocumentPath.Equals(documentPath));
      }
    }

    public async Task DeleteDocument(string documentPath) {
      using (LoquatDocsDbContext ctx = GetNewDbContext()) {
        var document = await ctx.Documents.FirstAsync(doc => doc.DocumentPath.Equals(documentPath));
        ctx.Documents.Remove(document);

        await ctx.SaveChangesAsync();
      }
    }

    public async Task CreateOrUpdateDatabaseAsync(string dbPath) {
      using (var ctx = new LoquatDocsDbContext(dbPath)) {

        await ctx.Database.MigrateAsync();
      }
    }

    public async Task PayInvoice(string documentPath) {
      using (LoquatDocsDbContext ctx = GetNewDbContext()) {
        (await ctx.Invoices.FirstOrDefaultAsync(i => i.DocumentPath.Equals(documentPath))).IsPayed = true;
        await ctx.SaveChangesAsync();
      }
    }
  }
}
