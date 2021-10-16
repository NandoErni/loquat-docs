using LoquatDocs.Model.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoquatDocs.ViewModel {
  public partial class SearchDocumentsViewModel {
    private const string RESOURCE_KEY = "SearchDocuments";

    public string ErrorFileNotExistResource(string filePath) => string.Format(Resource.GetResource(RESOURCE_KEY, "ErrorFileNotExist"), filePath);

    public string ErrorWhileDeleteResource(string documentTitle) => string.Format(Resource.GetResource(RESOURCE_KEY, "ErrorWhileDelete"), documentTitle);

    public string ErrorWhileOpenFileResource(string filePath) => string.Format(Resource.GetResource(RESOURCE_KEY, "ErrorWhileOpenFile"), filePath);

    public string FileDialogTitleResource => Resource.GetResource(RESOURCE_KEY, "FileDialogTitle");

    public string PromptSureToDeleteDocumentResource(string documentTitle) => string.Format(Resource.GetResource(RESOURCE_KEY, "PromptSureToDeleteDocument"), documentTitle);

    public string SuccessDeleteResource(string documentTitle) => string.Format(Resource.GetResource(RESOURCE_KEY, "SuccessDelete"), documentTitle);

    public string AddDocumentResource => Resource.GetResource(RESOURCE_KEY, "AddDocument");

    public string SearchFilterResource => Resource.GetResource(RESOURCE_KEY, "SearchFilter");

    public string SearchTitleResource => Resource.GetResource(RESOURCE_KEY, "SearchTitle");

    public string ListTitleResource => Resource.GetResource(RESOURCE_KEY, "ListTitle");

    public static string PayResource => Resource.GetResource(RESOURCE_KEY, "Pay");

    public static string PayedResource => Resource.GetResource(RESOURCE_KEY, "Payed");

    public static string DaysLeftToPayResource(int days) => string.Format(Resource.GetResource(RESOURCE_KEY, "DaysLeftToPay"), days);

    public string FilterTitleResource => Resource.GetResource(RESOURCE_KEY, "FilterTitle");

    public string FilterTagsResource => Resource.GetResource(RESOURCE_KEY, "FilterTags");

    public string FilterGroupNameResource => Resource.GetResource(RESOURCE_KEY, "FilterGroupName");

    public string FilterFilePathResource => Resource.GetResource(RESOURCE_KEY, "FilterFilePath");

    public string FilterDocumentDateResource => Resource.GetResource(RESOURCE_KEY, "FilterDocumentDate");

    public string FilterDocumentDueDateResource => Resource.GetResource(RESOURCE_KEY, "FilterDocumentDueDate");

    public string FilterOnlyInvoicesLeftToPayResource => Resource.GetResource(RESOURCE_KEY, "FilterOnlyInvoicesLeftToPay");

    public string InvoicePaymentResource => Resource.GetResource(RESOURCE_KEY, "InvoicePayment");

    public string PromptPayInvoiceResource => Resource.GetResource(RESOURCE_KEY, "PromptPayInvoice");

    public string InvoicePaymentConfirmedResource => Resource.GetResource(RESOURCE_KEY, "InvoicePaymentConfirmed");
  }
}
