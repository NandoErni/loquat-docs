using LoquatDocs.EntityFramework;
using LoquatDocs.EntityFramework.Model;
using LoquatDocs.Model;
using LoquatDocs.Model.Dialog;
using LoquatDocs.Model.Resource;
using Microsoft.EntityFrameworkCore;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Document = LoquatDocs.EntityFramework.Model.Document;

namespace LoquatDocs.ViewModel {
  public partial class SearchDocumentsViewModel : ObservableObject {

    private Config.Config _config = new Config.Config();

    private ObservableCollection<DocumentListItem> _documentListItems;

    public ObservableCollection<DocumentListItem> DocumentListItems {
      get => _documentListItems;
    }

    public SearchDocumentsViewModel() {
      _documentListItems = new ObservableCollection<DocumentListItem>();
    }

    public async Task EditDocument(string documentPath) {
      return;
    }

    public async Task SafeDeleteDocument(string documentPath) {
      string documentTitle = GetTitleOfPath(documentPath);
      var dialog = new DecisionDialog(Resource.GetResource(RESOURCE_KEY, "FileDialogTitle"), Resource.GetFormattedResource(RESOURCE_KEY, "PromptSureToDeleteDocument", documentTitle));
      if (!await dialog.ShowAsync()) {
        return;
      }
      try {
        _documentListItems.Remove(_documentListItems.FirstOrDefault(d => d.PathToDocument.Equals(documentPath)));
        await DeleteDocument(documentPath);
      } catch {
        var dialogError = new InfoDialog(Resource.GetResource(RESOURCE_KEY, "FileDialogTitle"),
          Resource.GetFormattedResource(RESOURCE_KEY, "ErrorWhileDelete", documentTitle));
        await dialogError.ShowAsync();
        return;
      }
      var dialogSuccess = new InfoDialog(Resource.GetResource(RESOURCE_KEY, "FileDialogTitle"),
        Resource.GetFormattedResource(RESOURCE_KEY, "SuccessDelete", documentTitle));
      await dialogSuccess.ShowAsync();
    }

    public async Task Search(string queryText, SearchArguments searchArguments) {
      using (LoquatDocsDbContext ctx = new LoquatDocsDbContext(_config.DatabaseFilePath)) {
        var documents = ctx.Documents.Include(d => d.Invoices).Include(d => d.Tags).AsEnumerable()
          .Where(d => DoesQueryTextMatch(queryText, d, searchArguments))
          .GroupBy(d => d.Groupname);
        UpdateDocumentList(documents);
      }
    }

    private string GetTitleOfPath(string documentPath) {
      using (LoquatDocsDbContext ctx = new LoquatDocsDbContext(_config.DatabaseFilePath)) {
        return ctx.Documents.FirstOrDefault(d => d.DocumentPath.Equals(documentPath))?.Title;
      }
    }

    private bool DoesQueryTextMatch(string queryText, Document document, SearchArguments searchArguments) {
      return (document.Title.IndexOf(queryText, StringComparison.OrdinalIgnoreCase) >= 0 && searchArguments.HasFlag(SearchArguments.Title))
        || (document.Tags.Any(t => t.TagId.IndexOf(queryText, StringComparison.OrdinalIgnoreCase) >= 0) && searchArguments.HasFlag(SearchArguments.Tags))
        || (document.Groupname.IndexOf(queryText, StringComparison.OrdinalIgnoreCase) >= 0 && searchArguments.HasFlag(SearchArguments.GroupName))
        || (document.DocumentPath.IndexOf(queryText, StringComparison.OrdinalIgnoreCase) >= 0 && searchArguments.HasFlag(SearchArguments.FilePath))
        || (document.DocumentDate.ToLongDateString().IndexOf(queryText, StringComparison.OrdinalIgnoreCase) >= 0 && searchArguments.HasFlag(SearchArguments.DocumentDate))
        || (document.Invoices.FirstOrDefault()?.DueDate.ToLongDateString().IndexOf(queryText, StringComparison.OrdinalIgnoreCase) >= 0 && searchArguments.HasFlag(SearchArguments.DocumentDueDate));
    }

    public async Task DeleteDocument(string documentPath) {
      using (LoquatDocsDbContext ctx = new LoquatDocsDbContext(_config.DatabaseFilePath)) {
        var document = await ctx.Documents.FirstAsync(doc => doc.DocumentPath.Equals(documentPath));
        ctx.Documents.Remove(document);

        await ctx.SaveChangesAsync();
      }
    }

    public async Task OpenFileLocation(string filePath) {
      if (!await FileExist(filePath)) {
        return;
      }
      Process.Start("explorer.exe", "/select, \"" + filePath + "\"");
    }

    public async Task OpenFile(string filePath) {
      if (!await FileExist(filePath)) {
        return;
      }
      try {
        Process.Start(new ProcessStartInfo(filePath) { UseShellExecute = true });
      } catch {
        var dialog = new InfoDialog(Resource.GetResource(RESOURCE_KEY, "FileDialogTitle"), 
          Resource.GetFormattedResource(RESOURCE_KEY, "ErrorWhileOpenFile", filePath));
        await dialog.ShowAsync();
        await OpenFileLocation(filePath);
      }
    }

    private async Task<bool> FileExist(string filePath) {
      if (!File.Exists(filePath)) {
        var dialog = new InfoDialog(Resource.GetResource(RESOURCE_KEY, "FileDialogTitle"),
          Resource.GetFormattedResource(RESOURCE_KEY, "ErrorFileNotExist", filePath));
        await dialog.ShowAsync();
        return false;
      }
      return true;
    }

    public async Task InitilizeList() {
      using (LoquatDocsDbContext ctx = new LoquatDocsDbContext(_config.DatabaseFilePath)) {
        var data = (await ctx.Documents.Include(d => d.Invoices).ToListAsync()).GroupBy(d => d.Groupname);
        App.QueueOnUiThread(() => UpdateDocumentList(data));
      }
    }

    private void UpdateDocumentList(IEnumerable<IGrouping<string, Document>> source) {
      _documentListItems.Clear();
      foreach (IGrouping<string, Document> groupItem in source) {
        _documentListItems.Add(new DocumentListItem(groupItem.Key));
        AddDocuments(groupItem);
      }
    }

    private void AddDocuments(IEnumerable<Document> documents) {
      foreach (var document in documents) {
        Invoice invoice = null;
        if (document.Invoices != null && document.Invoices.Any()) {
          invoice = document.Invoices.FirstOrDefault();
        }
        _documentListItems.Add(new DocumentListItem(document, invoice));
      }
    }
  }
}
