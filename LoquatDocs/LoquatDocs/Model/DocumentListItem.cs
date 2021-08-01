using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFModel = LoquatDocs.EntityFramework.Model;

namespace LoquatDocs.Model {
  public class DocumentListItem {

    public DocumentListItem(EFModel.Document document, EFModel.Invoice invoice = null) {
      IsGroup = false;
      Title = document.Title;
      Date = document.DocumentDate;
      PathToDocument = document.DocumentPath;
      if (invoice is not null) {
        IsInvoice = true;
        IsInvoicePayed = invoice.IsPayed;
        TimeLeftToPay = invoice.DueDate - DateTime.Now;
      }
    }

    public DocumentListItem(EFModel.Group group) : this(group.Groupname) {}

    public DocumentListItem(string groupName) {
      IsGroup = true;
      Title = groupName;
    }

    public bool IsGroup { get; set; }

    public bool IsDocument => !IsGroup;

    public string Title { get; set; }

    public DateTime Date { get; set; }

    public string DateAsString => Date.ToShortDateString();

    public bool IsInvoice { get; set; } = false;

    public bool IsInvoicePayed { get; set; }

    public TimeSpan TimeLeftToPay { get; set; }

    public string PathToDocument { get; set; }

    public string PayedText => IsInvoicePayed ? "Payed" : $"Time left to pay: {TimeLeftToPay.Days}";
  }
}
