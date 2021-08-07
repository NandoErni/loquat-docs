using LoquatDocs.EntityFramework.Model;
using LoquatDocs.Model;
using LoquatDocs.Model.Dialog;
using LoquatDocs.Model.Resource;
using LoquatDocs.ViewModel.Repository;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Document = LoquatDocs.EntityFramework.Model.Document;

namespace LoquatDocs.ViewModel {
  public partial class SearchDocumentsViewModel : ObservableObject {

    private LoquatDocsDbRepository _repository;

    private ObservableCollection<DocumentListItem> _documentListItems;

    public ObservableCollection<DocumentListItem> DocumentListItems {
      get => _documentListItems;
    }

    public SearchDocumentsViewModel() {
      _repository = new LoquatDocsDbRepository();
      _documentListItems = new ObservableCollection<DocumentListItem>();
    }

    public async Task EditDocument(string documentPath) {
      return;
    }

    public async Task SafeDeleteDocument(string documentPath) {
      string documentTitle = await GetTitleOfPath(documentPath);
      var dialog = new DecisionDialog(Resource.GetResource(RESOURCE_KEY, "FileDialogTitle"), Resource.GetFormattedResource(RESOURCE_KEY, "PromptSureToDeleteDocument", documentTitle));
      if (!await dialog.ShowAsync()) {
        return;
      }
      try {
        _documentListItems.Remove(_documentListItems.FirstOrDefault(d => d.PathToDocument.Equals(documentPath)));
        await _repository.DeleteDocument(documentPath);
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

    public void Search(string queryText, SearchArguments searchArguments) {
      UpdateDocumentList(_repository.GetDocuments((d) => DoesQueryTextMatch(queryText, d, searchArguments)));
    }

    private async Task<string> GetTitleOfPath(string documentPath) {
      return (await _repository.GetDocument(documentPath))?.Title;
    }

    private bool DoesQueryTextMatch(string queryText, Document document, SearchArguments searchArguments) {
      return (document.Title.IndexOf(queryText, StringComparison.OrdinalIgnoreCase) >= 0 && searchArguments.HasFlag(SearchArguments.Title))
        || (document.Tags.Any(t => t.TagId.IndexOf(queryText, StringComparison.OrdinalIgnoreCase) >= 0) && searchArguments.HasFlag(SearchArguments.Tags))
        || (document.Groupname.IndexOf(queryText, StringComparison.OrdinalIgnoreCase) >= 0 && searchArguments.HasFlag(SearchArguments.GroupName))
        || (document.DocumentPath.IndexOf(queryText, StringComparison.OrdinalIgnoreCase) >= 0 && searchArguments.HasFlag(SearchArguments.FilePath))
        || (document.DocumentDate.ToLongDateString().IndexOf(queryText, StringComparison.OrdinalIgnoreCase) >= 0 && searchArguments.HasFlag(SearchArguments.DocumentDate))
        || (document.Invoices.FirstOrDefault()?.DueDate.ToLongDateString().IndexOf(queryText, StringComparison.OrdinalIgnoreCase) >= 0 && searchArguments.HasFlag(SearchArguments.DocumentDueDate));
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

    public void InitilizeList() {
      App.QueueOnUiThread(() => UpdateDocumentList(_repository.GetDocuments((d) => true)));
    }

    private void UpdateDocumentList(IEnumerable<IGrouping<string, Document>> source) {
      _documentListItems.Clear();
      foreach (var groupItem in source) {
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
