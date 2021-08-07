using LoquatDocs.Model.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoquatDocs.ViewModel {
  public partial class DocumentViewModel {

    public const string RESOURCE_KEY = "Document";

    public string AddDocumentResource => Resource.GetResource(RESOURCE_KEY, "AddDocument");

    public string DiscardDocumentResource => Resource.GetResource(RESOURCE_KEY, "DiscardDocument");

    public string DiscardDocumentDecisionResource => Resource.GetResource(RESOURCE_KEY, "DiscardDocumentDecision");

    public string DocumentResource => Resource.GetResource(RESOURCE_KEY, "Document");

    public string DocumentAlreadyExistsAtLocationResource => Resource.GetResource(RESOURCE_KEY, "DocumentAlreadyExistsAtLocation");

    public string ErrorCantSaveResource => Resource.GetResource(RESOURCE_KEY, "ErrorCantSave");

    public string ErrorSavingToDatabaseResource => Resource.GetResource(RESOURCE_KEY, "ErrorSavingToDatabase");

    public string SavedSuccessfullyResource => Resource.GetResource(RESOURCE_KEY, "SavedSuccessfully");

    public string AddTagResource => Resource.GetResource(RESOURCE_KEY, "AddTag");

    public string DueDateResource => Resource.GetResource(RESOURCE_KEY, "DueDate");

    public string DateResource => Resource.GetResource(RESOURCE_KEY, "Date");

    public string IsInvoiceResource => Resource.GetResource(RESOURCE_KEY, "IsInvoice");

    public string IsPayedResource => Resource.GetResource(RESOURCE_KEY, "IsPayed");

    public string PathResource => Resource.GetResource(RESOURCE_KEY, "Path");

    public string GroupNameResource => Resource.GetResource(RESOURCE_KEY, "GroupName");

    public string GroupNamePlaceholderResource => Resource.GetResource(RESOURCE_KEY, "GroupNamePlaceholder");

    public string TitleResource => Resource.GetResource(RESOURCE_KEY, "Title");

    public string ChooseFileResource => Resource.GetResource(RESOURCE_KEY, "ChooseFile");

    public string GroupResource => Resource.GetResource(DocumentGroupViewModel.RESOURCE_KEY, "Group");

    public string SaveResource => General.Save;

    public string DiscardResource => General.Discard;

  }
}
