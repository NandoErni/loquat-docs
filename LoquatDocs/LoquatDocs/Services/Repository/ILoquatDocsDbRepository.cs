using LoquatDocs.EntityFramework.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoquatDocs.Services {
  public interface ILoquatDocsDbRepository {
    Task<bool> AnyDatabaseUpdates();
    Task CreateOrUpdateDatabaseAsync(string dbPath);
    Task DeleteDocument(string documentPath);
    Task DeleteGroup(string groupName);
    Task<bool> DocumentExist(string documentPath);
    Task<List<Document>> GetAllDocumentsOfGroup(string groupName);
    Task<List<string>> GetAllGroupnames();
    Task<List<string>> GetAllTagIds();
    Task<Document> GetDocument(string documentPath);
    Task<List<IGrouping<string, Document>>> GetDocuments(Func<Document, bool> predicate);
    Task<bool> GroupExist(string groupName);
    Task PayInvoice(string documentPath);
    Task RemoveDocuments(List<Document> documentsOfGroup);
    Task SaveDocument(Document document);
    Task SaveGroup(string groupName);
    Task SaveInvoice(Invoice invoice);
    Task SaveTags(List<Tag> tags);
  }
}