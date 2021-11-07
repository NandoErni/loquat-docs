using LoquatDocs.Model.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoquatDocs.ViewModel {
  public partial class DocumentViewModel {

    public const string RESOURCE_KEY = "Document";

    public string AddDocumentResource => _resourceProvider.GetResource(RESOURCE_KEY, "AddDocument");

    public string DiscardDocumentResource => _resourceProvider.GetResource(RESOURCE_KEY, "DiscardDocument");

    public string DiscardDocumentDecisionResource => _resourceProvider.GetResource(RESOURCE_KEY, "DiscardDocumentDecision");

    public string DocumentResource => _resourceProvider.GetResource(RESOURCE_KEY, "Document");

    public string DocumentAlreadyExistsAtLocationResource(string documentPath) => string.Format(_resourceProvider.GetResource(RESOURCE_KEY, "DocumentAlreadyExistsAtLocation"), documentPath);

    public string ErrorCantSaveResource => _resourceProvider.GetResource(RESOURCE_KEY, "ErrorCantSave");

    public string ErrorSavingToDatabaseResource => _resourceProvider.GetResource(RESOURCE_KEY, "ErrorSavingToDatabase");

    public string SavedSuccessfullyResource => _resourceProvider.GetResource(RESOURCE_KEY, "SavedSuccessfully");

    public string AddTagResource => _resourceProvider.GetResource(RESOURCE_KEY, "AddTag");

    public string DueDateResource => _resourceProvider.GetResource(RESOURCE_KEY, "DueDate");

    public string DateResource => _resourceProvider.GetResource(RESOURCE_KEY, "Date");

    public string IsInvoiceResource => _resourceProvider.GetResource(RESOURCE_KEY, "IsInvoice");

    public string IsPayedResource => _resourceProvider.GetResource(RESOURCE_KEY, "IsPayed");

    public string PathResource => _resourceProvider.GetResource(RESOURCE_KEY, "Path");

    public string GroupNameResource => _resourceProvider.GetResource(RESOURCE_KEY, "GroupName");

    public string GroupNamePlaceholderResource => _resourceProvider.GetResource(RESOURCE_KEY, "GroupNamePlaceholder");

    public string TitleResource => _resourceProvider.GetResource(RESOURCE_KEY, "Title");

    public string ChooseFileResource => _resourceProvider.GetResource(RESOURCE_KEY, "ChooseFile");

    public string GroupResource => _resourceProvider.GetResource(DocumentGroupViewModel.RESOURCE_KEY, "Group");

    public string SaveResource => _resourceProvider.GeneralResources.Save;

    public string DiscardResource => _resourceProvider.GeneralResources.Discard;

  }
}
