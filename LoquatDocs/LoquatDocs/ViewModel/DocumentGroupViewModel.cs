using LoquatDocs.Model.Dialog;
using LoquatDocs.Model.Resource;
using LoquatDocs.ViewModel.Repository;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoquatDocs.ViewModel {
  public partial class DocumentGroupViewModel : ObservableObject {

    private LoquatDocsDbRepository _repository;

    private Config.Config _config = new Config.Config();

    private ObservableCollection<string> _documentGroupNames = new ObservableCollection<string>();

    public ObservableCollection<string> Groups {
      get => _documentGroupNames;
    }

    public DocumentGroupViewModel() {
      _repository = new LoquatDocsDbRepository();
    }

    public async Task InitilizeDocumentGroups() {
      foreach (var groupname in await _repository.GetAllGroupnames()) {
        _documentGroupNames.Add(groupname);
      }
    }

    public async Task SaveGroupAsync(string groupName) {
      await _repository.SaveGroup(groupName);
      _documentGroupNames.Add(groupName);
      SortGroupList();
    }

    public async Task PromptAndDeleteGroupAsync(string groupName) {
      DecisionDialog decisionDialog = new DecisionDialog(PromptDeleteGroupTitleResource(groupName), PromptDeleteGroupResource(groupName));
      if (!await decisionDialog.ShowAsync()) {
        return;
      }

      await DeleteGroup(groupName);
    }

    private async Task DeleteGroup(string groupName) {
      if (!await _repository.GroupExist(groupName)) {
        return;
      }

      await PromptAndDeleteDocumentsOfGroup(groupName);

      await _repository.DeleteGroup(groupName);
      _documentGroupNames.Remove(groupName);
    }

    private async Task PromptAndDeleteDocumentsOfGroup(string groupName) {

      var documentsOfGroup = await _repository.GetAllDocumentsOfGroup(groupName);
      if (!documentsOfGroup.Any()) {
        return;
      }

      var documentList = BuildListOfDocuments(documentsOfGroup);
      var dialog = new DecisionDialog(PromptDeleteGroupTitleResource(groupName), PromptDeleteDocumentsResource(documentList));
      if (!await dialog.ShowAsync()) {
        return;
      }
      await _repository.RemoveDocuments(documentsOfGroup);
    }

    private string BuildListOfDocuments(List<EntityFramework.Model.Document> documentsOfGroup) {
      StringBuilder builder = new StringBuilder();
      builder.AppendLine();
      foreach (var document in documentsOfGroup) {
        builder.AppendLine(document.Title);
      }

      return builder.ToString();
    }

    private void SortGroupList() {
      var sortableList = new List<string>(_documentGroupNames);
      sortableList.Sort();

      for (int i = 0; i < sortableList.Count; i++) {
        _documentGroupNames.Move(_documentGroupNames.IndexOf(sortableList[i]), i);
      }
    }
  }
}
