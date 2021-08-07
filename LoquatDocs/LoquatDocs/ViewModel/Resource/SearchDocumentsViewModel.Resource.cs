using LoquatDocs.Model.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoquatDocs.ViewModel {
  public partial class SearchDocumentsViewModel {
    private const string RESOURCE_KEY = "SearchDocuments";

    public string ErrorFileNotExistResource => Resource.GetResource(RESOURCE_KEY, "ErrorFileNotExist");

    public string ErrorWhileDeleteResource => Resource.GetResource(RESOURCE_KEY, "ErrorWhileDelete");

    public string ErrorWhileOpenFileResource => Resource.GetResource(RESOURCE_KEY, "ErrorWhileOpenFile");

    public string FileDialogTitleResource => Resource.GetResource(RESOURCE_KEY, "FileDialogTitle");

    public string PromptSureToDeleteDocumentResource => Resource.GetResource(RESOURCE_KEY, "PromptSureToDeleteDocument");

    public string SuccessDeleteResource => Resource.GetResource(RESOURCE_KEY, "SuccessDelete");

    public string AddDocumentResource => Resource.GetResource(RESOURCE_KEY, "AddDocument");

    public string SearchFilterResource => Resource.GetResource(RESOURCE_KEY, "SearchFilter");

    public string SearchTitleResource => Resource.GetResource(RESOURCE_KEY, "SearchTitle");

    public string ListTitleResource => Resource.GetResource(RESOURCE_KEY, "ListTitle");
  }
}
