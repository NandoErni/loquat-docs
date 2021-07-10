using System;
using System.Collections.ObjectModel;

namespace LoquatDocs.Model {
  public class Document {
    public const int STANDARD_INVOICE_PAYMENT_DAYS = 30;

    public string GroupName;

    public string Title;

    public DateTime DateOfDocument = DateTime.Now;

    public string DocumentPath;

    public ObservableCollection<string> Tags = new ObservableCollection<string>();

    public bool IsInvoice;

    public bool IsPayed;

    public DateTime InvoiceDueDate = DateTime.Now.AddDays(30);
  }
}
