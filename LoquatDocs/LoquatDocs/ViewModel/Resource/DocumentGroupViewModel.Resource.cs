using LoquatDocs.Model.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoquatDocs.ViewModel {
  public partial class DocumentGroupViewModel {
    public const string RESOURCE_KEY = "DocumentGroup";

    public string AddDocumentGroupResource => Resource.GetResource(RESOURCE_KEY, "AddDocumentGroup");

    public string PromptDeleteDocumentsResource => Resource.GetResource(RESOURCE_KEY, "PromptDeleteDocuments");

    public string PromptDeleteGroupResource => Resource.GetResource(RESOURCE_KEY, "PromptDeleteGroup");

    public string PromptDeleteGroupTitleResource => Resource.GetResource(RESOURCE_KEY, "PromptDeleteGroupTitle");

    public string AddGroupResource => Resource.GetResource(RESOURCE_KEY, "AddGroup");

    public string GroupResource => Resource.GetResource(RESOURCE_KEY, "Group");
  }
}
