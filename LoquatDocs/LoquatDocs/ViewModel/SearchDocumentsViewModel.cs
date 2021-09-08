using LoquatDocs.EntityFramework.Model;
using LoquatDocs.Model;
using LoquatDocs.Services;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Serilog;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Document = LoquatDocs.EntityFramework.Model.Document;

namespace LoquatDocs.ViewModel {
  public partial class SearchDocumentsViewModel : ObservableObject {

    private ILoquatDocsDbRepository _repository;

    private Search _search = new Search();

    private INotificationService _notification;

    private ILogger _logger;

    private IProcessHelperService _processHelper;

    public ObservableCollection<DocumentListItem> DocumentListItems {
      get => _search.DocumentListItems;
    }

    public string QueryText {
      get => _search.QueryText;
      set => SetProperty(ref _search.QueryText, value);
    }

    public SearchDocumentsViewModel(INotificationService notificationService, ILogger logger, 
      IProcessHelperService processHelper, ILoquatDocsDbRepository repository) {
      _logger = logger;
      _notification = notificationService;
      _processHelper = processHelper;
      _repository = repository;
      _search.DocumentListItems = new ObservableCollection<DocumentListItem>();
    }

    public async Task EditDocument(string documentPath) {
      return;
    }

    public async Task SafeDeleteDocument(string documentPath) {
      string documentTitle = await GetTitleOfPath(documentPath);
      if (!await _notification.NotifyDecision(FileDialogTitleResource, PromptSureToDeleteDocumentResource(documentTitle))) {
        return;
      }
      try {
        _search.DocumentListItems.Remove(_search.DocumentListItems.FirstOrDefault(d => d.PathToDocument.Equals(documentPath)));
        await _repository.DeleteDocument(documentPath);
      } catch (Exception e) {
        await _notification.NotifyInfo(FileDialogTitleResource, ErrorWhileDeleteResource(documentTitle));
        _logger.Error(e.ToString());
        return;
      }
      await _notification.NotifyInfo(FileDialogTitleResource, SuccessDeleteResource(documentTitle));
    }

    public void Search(SearchArguments searchArguments) {
      UpdateDocumentList(_repository.GetDocuments((d) => DoesQueryTextMatch(_search.QueryText, d, searchArguments)));
    }

    private async Task<string> GetTitleOfPath(string documentPath) {
      return (await _repository.GetDocument(documentPath))?.Title;
    }

    private bool DoesQueryTextMatch(string queryText, Document document, SearchArguments searchArguments) {
      return (!document.Invoices.FirstOrDefault()?.IsPayed == true || !searchArguments.HasFlag(SearchArguments.OnlyInvoicesLeftToPay)) && 
        ((document.Title.IndexOf(queryText, StringComparison.OrdinalIgnoreCase) >= 0 && searchArguments.HasFlag(SearchArguments.Title))
        || (document.Tags.Any(t => t.TagId.IndexOf(queryText, StringComparison.OrdinalIgnoreCase) >= 0) && searchArguments.HasFlag(SearchArguments.Tags))
        || (document.Groupname.IndexOf(queryText, StringComparison.OrdinalIgnoreCase) >= 0 && searchArguments.HasFlag(SearchArguments.GroupName))
        || (document.DocumentPath.IndexOf(queryText, StringComparison.OrdinalIgnoreCase) >= 0 && searchArguments.HasFlag(SearchArguments.FilePath))
        || (document.DocumentDate.ToLongDateString().IndexOf(queryText, StringComparison.OrdinalIgnoreCase) >= 0 && searchArguments.HasFlag(SearchArguments.DocumentDate))
        || (document.Invoices.FirstOrDefault()?.DueDate.ToLongDateString().IndexOf(queryText, StringComparison.OrdinalIgnoreCase) >= 0 && searchArguments.HasFlag(SearchArguments.DocumentDueDate)));
    }

    public async Task OpenFileLocation(string filePath) {
      if (!await FileExist(filePath)) {
        return;
      }
      _processHelper.OpenFileLocationInExplorer(filePath);
    }

    public async Task OpenFile(string filePath) {
      if (!await FileExist(filePath)) {
        return;
      }
      try {
        _processHelper.OpenFileInExplorer(filePath);
      } catch (Exception e) {
        await _notification.NotifyInfo(FileDialogTitleResource, ErrorWhileOpenFileResource(filePath));
        await OpenFileLocation(filePath);
        _logger.Error(e.ToString());
      }
    }

    private async Task<bool> FileExist(string filePath) {
      if (!File.Exists(filePath)) {
        await _notification.NotifyInfo(FileDialogTitleResource, ErrorFileNotExistResource(filePath));
        return false;
      }
      return true;
    }

    public void InitilizeList() {
      App.QueueOnUiThread(() => UpdateDocumentList(_repository.GetDocuments((d) => true)));
    }

    private void UpdateDocumentList(IEnumerable<IGrouping<string, Document>> source) {
      _search.DocumentListItems.Clear();
      foreach (var groupItem in source) {
        _search.DocumentListItems.Add(new DocumentListItem(groupItem.Key));
        AddDocuments(groupItem);
      }
    }

    private void AddDocuments(IEnumerable<Document> documents) {
      foreach (var document in documents) {
        Invoice invoice = null;
        if (document.Invoices != null && document.Invoices.Any()) {
          invoice = document.Invoices.FirstOrDefault();
        }
        _search.DocumentListItems.Add(new DocumentListItem(document, invoice));
      }
    }

    public async Task PayInvoice(string documentPath) {
      await _repository.PayInvoice(documentPath);
      _search.DocumentListItems.FirstOrDefault(doc => doc.PathToDocument.Equals(documentPath)).IsInvoicePayed = true;
    }
  }
}
