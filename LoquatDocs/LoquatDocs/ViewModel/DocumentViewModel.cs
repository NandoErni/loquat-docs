using LoquatDocs.EntityFramework;
using LoquatDocs.Model;
using LoquatDocs.Model.Dialog;
using Microsoft.ApplicationModel.Resources;
using Microsoft.EntityFrameworkCore;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Windows.Storage;
using EF = LoquatDocs.EntityFramework.Model;
using EFConverter = LoquatDocs.Converter.EntityFrameworkModelConverter;
using LoquatDocs.Model.Resource;

namespace LoquatDocs.ViewModel {
  public class DocumentViewModel : ObservableObject {

    private Document _document = new Document();

    private SuggestionList<string> _groupNameSuggestionList;

    private SuggestionList<string> _tagSuggestionList;

    public string GroupName {
      get => _document.GroupName;
      set => SetProperty(ref _document.GroupName, value);
    }

    public List<string> GroupNameSuggestions {
      get => _groupNameSuggestionList.FilteredList;
    }

    public string Title {
      get => _document.Title;
      set => SetProperty(ref _document.Title, value);
    }

    public DateTime DateOfDocument {
      get => _document.DateOfDocument;
      set => SetProperty(ref _document.DateOfDocument, value);
    }

    public string DocumentPath {
      get => _document.DocumentPath;
      set => SetProperty(ref _document.DocumentPath, value);
    }

    public ObservableCollection<string> Tags {
      get => _document.Tags;
    }

    public List<string> TagSuggestions {
      get => _tagSuggestionList.FilteredList;
    }

    public bool IsInvoice {
      get => _document.IsInvoice;
      set => SetProperty(ref _document.IsInvoice, value);
    }

    public bool IsPayed {
      get => _document.IsPayed;
      set => SetProperty(ref _document.IsPayed, value);
    }

    public DateTime InvoiceDueDate {
      get => _document.InvoiceDueDate;
      set => SetProperty(ref _document.InvoiceDueDate, value);
    }

    public IAsyncRelayCommand SaveCommand { get; }

    public IAsyncRelayCommand DiscardCommand { get; }

    public IAsyncRelayCommand ChoosePathCommand { get; }

    public DocumentViewModel() {
      SaveCommand = new AsyncRelayCommand(SaveDocumentAsync);
      DiscardCommand = new AsyncRelayCommand(DiscardDocumentAsync);
      ChoosePathCommand = new AsyncRelayCommand(ChoosePathAndValidateAsync);
      _groupNameSuggestionList = new SuggestionList<string>();
      _tagSuggestionList = new SuggestionList<string>();
      Task.Run(InitilizeSuggestionsAsync);
    }

    public async Task SaveDocumentAsync() {
      if (!IsDocumentValidToSave()) {
        await InfoDialog.CreateAndShowErrorAsync(Resource.GetResource("Document", "ErrorCantSave"));
        return;
      }

      try {
        await SaveDocumentToDatabaseAsync();
      } catch (DbUpdateException) {
        // todo: log
        await InfoDialog.CreateAndShowErrorAsync(Resource.GetResource("Document", "ErrorSavingToDatabase"));
        return;
      }
      await InfoDialog.CreateAndShowSuccessAsync(Resource.GetResource("Document", "SavedSuccessfully"));
    }

    private async Task SaveDocumentToDatabaseAsync() {
      using (LoquatDocsDbContext ctx = new LoquatDocsDbContext()) {
        await AddGroupIfNotExistentAsync(ctx);
        await ctx.Documents.AddAsync(EFConverter.ConvertToEFDocument(GroupName, Title, DocumentPath, DateOfDocument));
        await ctx.Tags.AddRangeAsync(EFConverter.ConvertToEFTag(Tags, DocumentPath));

        if (IsInvoice) {
          await ctx.Invoices.AddAsync(EFConverter.ConvertToEFInvoice(DocumentPath, InvoiceDueDate, IsPayed));
        }

        await ctx.SaveChangesAsync();
      }
    }

    private async Task AddGroupIfNotExistentAsync(LoquatDocsDbContext ctx) {
      if (await ctx.Groups.FindAsync(GroupName) is null) {
        await ctx.Groups.AddAsync(new EF.Group() { Groupname = GroupName });
      }
    }

    public async Task DiscardDocumentAsync() {
      if (await DecisionDialog.CreateAndShowAsync(Resource.GetResource("Document", "DiscardDocument"), 
        Resource.GetResource("Document", "DiscardDocumentDecision"))) {
        ResetValues();
      }
    }

    public async Task ChoosePathAndValidateAsync() {
      StandardFilePicker picker = StandardFilePicker.UniversalFilePicker;
      StorageFile document = await picker.PickSingleFileAsync();
      if (await DoesDocumentAlreadyExistAsync(document.Path)) {
        await InfoDialog.CreateAndShowErrorAsync(Resource.GetFormattedResource("Document", "DocumentAlreadyExistsAtLocation", document.Path));
      } else {
        DocumentPath = document.Path;
      }
    }

    private bool IsDocumentValidToSave() {
      return !string.IsNullOrWhiteSpace(GroupName)
        && !string.IsNullOrWhiteSpace(Title)
        && !string.IsNullOrWhiteSpace(DocumentPath);
    }

    private async Task<bool> DoesDocumentAlreadyExistAsync(string documentPath) {
      using (LoquatDocsDbContext ctx = new LoquatDocsDbContext()) {
        return await ctx.Documents.FindAsync(documentPath) is not null;
      }
    }

    private void ResetValues() {
      _document = new Document();
      OnPropertyChanged(string.Empty);
    }

    public async Task InitilizeSuggestionsAsync() {
      using (LoquatDocsDbContext ctx = new LoquatDocsDbContext()) {
        _groupNameSuggestionList.BaseList = await ctx.Groups.Select(x => x.Groupname).ToListAsync();
        _tagSuggestionList.BaseList = await ctx.Tags.Select(x => x.TagId).ToListAsync();
      }
    }

    public void UpdateFilteredGroupNameSuggestions() {
      List<string> newList = _groupNameSuggestionList.BaseList
        .Where(x => x.Contains(GroupName ?? string.Empty, StringComparison.CurrentCultureIgnoreCase)).ToList();

      if (!newList.SequenceEqual(_groupNameSuggestionList.FilteredList)) {
        _groupNameSuggestionList.FilteredList = newList;
        OnPropertyChanged(nameof(GroupNameSuggestions));
      }
    }

    public void UpdateFilteredTagSuggestionList(string tag) {
      List<string> newList = _tagSuggestionList.BaseList
        .Where(x => x.Contains(tag ?? string.Empty, StringComparison.CurrentCultureIgnoreCase)).ToList();

      if (!newList.SequenceEqual(_tagSuggestionList.FilteredList)) {
        _tagSuggestionList.FilteredList = newList;
        OnPropertyChanged(nameof(TagSuggestions));
      }
    }

    public void AddTagToList(string tag) {
      if (string.IsNullOrWhiteSpace(tag) || Tags.Contains(tag)) {
        return;
      }
      Tags.Add(tag);
    }
  }
}
