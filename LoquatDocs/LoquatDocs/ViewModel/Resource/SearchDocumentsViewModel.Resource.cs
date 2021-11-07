using LoquatDocs.Model.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoquatDocs.ViewModel {
  public partial class SearchDocumentsViewModel {
    private const string RESOURCE_KEY = "SearchDocuments";

    public string ErrorFileNotExistResource(string filePath) => string.Format(_resourceProvider.GetResource(RESOURCE_KEY, "ErrorFileNotExist"), filePath);

    public string ErrorWhileDeleteResource(string documentTitle) => string.Format(_resourceProvider.GetResource(RESOURCE_KEY, "ErrorWhileDelete"), documentTitle);

    public string ErrorWhileOpenFileResource(string filePath) => string.Format(_resourceProvider.GetResource(RESOURCE_KEY, "ErrorWhileOpenFile"), filePath);

    public string FileDialogTitleResource => _resourceProvider.GetResource(RESOURCE_KEY, "FileDialogTitle");

    public string PromptSureToDeleteDocumentResource(string documentTitle) => string.Format(_resourceProvider.GetResource(RESOURCE_KEY, "PromptSureToDeleteDocument"), documentTitle);

    public string SuccessDeleteResource(string documentTitle) => string.Format(_resourceProvider.GetResource(RESOURCE_KEY, "SuccessDelete"), documentTitle);

    public string AddDocumentResource => _resourceProvider.GetResource(RESOURCE_KEY, "AddDocument");

    public string SearchFilterResource => _resourceProvider.GetResource(RESOURCE_KEY, "SearchFilter");

    public string SearchTitleResource => _resourceProvider.GetResource(RESOURCE_KEY, "SearchTitle");

    public string ListTitleResource => _resourceProvider.GetResource(RESOURCE_KEY, "ListTitle");

    public static string PayResource => ServiceProvider.GetService<IResourceProvider>().GetResource(RESOURCE_KEY, "Pay");

    public static string PayedResource => ServiceProvider.GetService<IResourceProvider>().GetResource(RESOURCE_KEY, "Payed");

    public static string DaysLeftToPayResource(int days) => string.Format(ServiceProvider.GetService<IResourceProvider>().GetResource(RESOURCE_KEY, "DaysLeftToPay"), days);

    public string FilterTitleResource => _resourceProvider.GetResource(RESOURCE_KEY, "FilterTitle");

    public string FilterTagsResource => _resourceProvider.GetResource(RESOURCE_KEY, "FilterTags");

    public string FilterGroupNameResource => _resourceProvider.GetResource(RESOURCE_KEY, "FilterGroupName");

    public string FilterFilePathResource => _resourceProvider.GetResource(RESOURCE_KEY, "FilterFilePath");

    public string FilterDocumentDateResource => _resourceProvider.GetResource(RESOURCE_KEY, "FilterDocumentDate");

    public string FilterDocumentDueDateResource => _resourceProvider.GetResource(RESOURCE_KEY, "FilterDocumentDueDate");

    public string FilterOnlyInvoicesLeftToPayResource => _resourceProvider.GetResource(RESOURCE_KEY, "FilterOnlyInvoicesLeftToPay");

    public string InvoicePaymentResource => _resourceProvider.GetResource(RESOURCE_KEY, "InvoicePayment");

    public string PromptPayInvoiceResource => _resourceProvider.GetResource(RESOURCE_KEY, "PromptPayInvoice");

    public string InvoicePaymentConfirmedResource => _resourceProvider.GetResource(RESOURCE_KEY, "InvoicePaymentConfirmed");
  }
}
