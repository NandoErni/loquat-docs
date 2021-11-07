using LoquatDocs.EntityFramework;
using LoquatDocs.Model;
using LoquatDocs.Model.Resource;
using LoquatDocs.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Serilog;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Windows.Storage;
using EF = LoquatDocs.EntityFramework.Model;
using EFFactory = LoquatDocs.Converter.EntityFrameworkModelFactory;

namespace LoquatDocs.ViewModel {
  public partial class DocumentViewModel : ObservableObject {

    ILoquatDocsDbRepository _repository;

    private Document _document = new Document();

    private SuggestionList<string> _groupNameSuggestionList;

    private SuggestionList<string> _tagSuggestionList;

    private INotificationService _notification;

    private ILogger _logger;

    private IResourceProvider _resourceProvider;

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
      set { 
        SetProperty(ref _document.IsInvoice, value);
        InvoiceDueDate = DateOfDocument.AddDays(30);
      }
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

    public DocumentViewModel(INotificationService notificationService, ILogger logger, ILoquatDocsDbRepository repository,
      IResourceProvider resourceProvider) {
      _resourceProvider = resourceProvider;
      _logger = logger;
      _notification = notificationService;
      _repository = repository;
      SaveCommand = new AsyncRelayCommand(SaveDocumentAsync);
      DiscardCommand = new AsyncRelayCommand(DiscardDocumentAsync);
      ChoosePathCommand = new AsyncRelayCommand(ChoosePathAndValidateAsync);
      _groupNameSuggestionList = new SuggestionList<string>();
      _tagSuggestionList = new SuggestionList<string>();
      Task.Run(InitilizeSuggestionsAsync);
    }

    public async Task SaveDocumentAsync() {
      if (!IsDocumentValidToSave()) {
        await _notification.NotifyError(ErrorCantSaveResource);
        return;
      }

      try {
        await SaveDocumentToDatabaseAsync();
        ResetValues();
      } catch (DbUpdateException e) {
        await _notification.NotifyError(ErrorSavingToDatabaseResource);
        _logger.Error(e.ToString());
        return;
      }
      await Task.WhenAll(_notification.NotifySuccess(SavedSuccessfullyResource), 
        InitilizeSuggestionsAsync());

    }

    private async Task SaveDocumentToDatabaseAsync() {
      await AddGroupIfNotExistentAsync();
      await _repository.SaveDocument(EFFactory.CreateDocument(GroupName, Title, DocumentPath, DateOfDocument));
      await _repository.SaveTags(EFFactory.CreateTag(Tags, DocumentPath));

      if (IsInvoice) {
        await _repository.SaveInvoice(EFFactory.CreateInvoice(DocumentPath, InvoiceDueDate, IsPayed));
      }
    }

    private async Task AddGroupIfNotExistentAsync() {
      if (!await _repository.GroupExist(GroupName)) {
        await _repository.SaveGroup(GroupName);
      }
    }

    public async Task DiscardDocumentAsync() {
      if (await _notification.NotifyDecision(DiscardDocumentResource, DiscardDocumentDecisionResource)) {
        ResetValues();
      }
    }

    public async Task ChoosePathAndValidateAsync() {
      StandardFilePicker picker = StandardFilePicker.UniversalFilePicker;
      StorageFile document = await picker.PickSingleFileAsync();
      if (document is null) {
        return;
      }

      if (await _repository.DocumentExist(document.Path)) {
        await _notification.NotifyError(DocumentAlreadyExistsAtLocationResource(document.Path));
      } else {
        DocumentPath = document.Path;
      }
    }

    private bool IsDocumentValidToSave() {
      return !string.IsNullOrWhiteSpace(GroupName)
        && !string.IsNullOrWhiteSpace(Title)
        && !string.IsNullOrWhiteSpace(DocumentPath);
    }

    private void ResetValues() {
      _document = new Document();
      OnPropertyChanged(string.Empty);
    }

    public async Task InitilizeSuggestionsAsync() {
      _groupNameSuggestionList.BaseList = await _repository.GetAllGroupnames();
      _tagSuggestionList.BaseList = await _repository.GetAllTagIds();
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
        .Where(x => x.Contains(tag ?? string.Empty, StringComparison.CurrentCultureIgnoreCase)).Distinct().ToList();

      if (!newList.SequenceEqual(_tagSuggestionList.FilteredList)) {
        _tagSuggestionList.FilteredList = newList;
        OnPropertyChanged(nameof(TagSuggestions));
      }
    }

    public void AddTagToList(string tag) {
      if (string.IsNullOrWhiteSpace(tag) || Tags.Contains(tag)) {
        return;
      }
      Tags.Insert(0, tag);
    }
  }
}
