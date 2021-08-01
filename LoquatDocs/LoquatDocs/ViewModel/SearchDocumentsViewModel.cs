using LoquatDocs.EntityFramework;
using LoquatDocs.EntityFramework.Model;
using LoquatDocs.Model;
using LoquatDocs.Model.Dialog;
using Microsoft.EntityFrameworkCore;
using Microsoft.Toolkit.Mvvm.ComponentModel;
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
  public class SearchDocumentsViewModel : ObservableObject {

    private ObservableCollection<DocumentListItem> _documentListItems;

    public ObservableCollection<DocumentListItem> DocumentListItems {
      get => _documentListItems;
    }

    public SearchDocumentsViewModel() {
      _documentListItems = new ObservableCollection<DocumentListItem>();
      UpdateList();
    }

    public void UpdateList() {
      using (LoquatDocsDbContext ctx = new LoquatDocsDbContext()) {
        foreach (IGrouping<string, Document> groupItem in ctx.Documents.Include(d => d.Invoices).AsEnumerable().GroupBy(d => d.Groupname)) {
          _documentListItems.Add(new DocumentListItem(groupItem.Key));
          AddDocuments(groupItem);
          
        }
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
        Process.Start(filePath);
      } catch {
        await OpenFileLocation(filePath);
      }
    }

    private async Task<bool> FileExist(string filePath) {
      if (!File.Exists(filePath)) {
        var dialog = new InfoDialog("File", $"The File at {filePath} does not exist.");
        await dialog.ShowAsync();
        return false;
      }
      return true;
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
