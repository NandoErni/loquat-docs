using LoquatDocs.EntityFramework;
using LoquatDocs.EntityFramework.Model;
using LoquatDocs.Model;
using LoquatDocs.Model.Dialog;
using LoquatDocs.Model.Resource;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoquatDocs.ViewModel {
  public class DocumentGroupViewModel : ObservableObject {
    private const string RESOURCE_KEY = "DocumentGroup";

    private ObservableCollection<string> _documentGroupNames;

    public ObservableCollection<string> Groups {
      get => _documentGroupNames;
    }

    public DocumentGroupViewModel() {
      InitilizeDocumentGroups();
    }

    private void InitilizeDocumentGroups() {
      _documentGroupNames = new ObservableCollection<string>();
      using (LoquatDocsDbContext ctx = new LoquatDocsDbContext()) {
        foreach (var Group in ctx.Groups) {
          _documentGroupNames.Add(Group.Groupname);
        }
      }
    }

    public async Task SaveGroupAsync(string groupName) {
      using (LoquatDocsDbContext ctx = new LoquatDocsDbContext()) {
        ctx.Groups.Add(new EntityFramework.Model.Group() { Groupname = groupName });
        await ctx.SaveChangesAsync();
      }
      _documentGroupNames.Add(groupName);
      SortGroupList();
    }

    public async Task PromptAndDeleteGroupAsync(string groupName) {
      DecisionDialog decisionDialog = new DecisionDialog(Resource.GetFormattedResource(RESOURCE_KEY, "PromptDeleteGroupTitle", groupName),
        Resource.GetFormattedResource(RESOURCE_KEY, "PromptDeleteGroup", groupName));
      if (!await decisionDialog.ShowAsync()) {
        return;
      }

      await DeleteGroup(groupName);
    }

    private async Task DeleteGroup(string groupName) {
      using (LoquatDocsDbContext ctx = new LoquatDocsDbContext()) {
        Group groupToDelete = ctx.Groups.Where(group => group.Groupname.Equals(groupName)).FirstOrDefault();

        if (groupToDelete is null) {
          return;
        }

        await PromptAndDeleteDocumentsOfGroup(groupToDelete, ctx);

        ctx.Groups.Remove(groupToDelete);
        await ctx.SaveChangesAsync();
        _documentGroupNames.Remove(groupName);
      }
    }

    private async Task PromptAndDeleteDocumentsOfGroup(Group groupToDelete, LoquatDocsDbContext context) {
      var documentsOfGroupToDelete = context.Documents.Where(document => document.Group.Equals(groupToDelete));
      if (documentsOfGroupToDelete.Any()) {
        StringBuilder builder = new StringBuilder();
        foreach (var document in documentsOfGroupToDelete) {
          builder.AppendLine(document.Title);
        }
        var dialog = new DecisionDialog(Resource.GetFormattedResource(RESOURCE_KEY, "PromptDeleteGroupTitle", groupToDelete.Groupname),
          Resource.GetFormattedResource(RESOURCE_KEY, "PromptDeleteDocuments", builder.ToString()));
        if (!await dialog.ShowAsync()) {
          return;
        }
        context.Documents.RemoveRange(documentsOfGroupToDelete);
        await context.SaveChangesAsync();
      }
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
