using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;

namespace LoquatDocs.Pages {
  public sealed partial class DocumentListItem : UserControl {

    public bool IsGroup { get; set; }

    public bool IsDocument => !IsGroup;

    public string Title { get; set; }

    public DateTime Date { get; set; }

    public string DateAsString => Date.ToShortDateString();

    public bool IsInvoice { get; set; }

    public bool IsInvoicePayed { get; set; }

    public TimeSpan TimeLeftToPay { get; set; }

    public string PayedText => IsInvoicePayed ? "Payed" : $"Time left to pay: {TimeLeftToPay.Days}";

    public string PathToDocument { get; set; }

    public DocumentListItem() {
      this.InitializeComponent();
    }

    private void OpenDocument(object sender, RoutedEventArgs e) {

    }
  }
}
