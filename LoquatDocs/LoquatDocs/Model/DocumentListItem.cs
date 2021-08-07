using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using EFModel = LoquatDocs.EntityFramework.Model;

namespace LoquatDocs.Model {
  public class DocumentListItem : INotifyPropertyChanged {

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

    public DocumentListItem(string groupName) {
      IsGroup = true;
      Title = groupName;
    }

    public event PropertyChangedEventHandler PropertyChanged;

    private bool _isGroup;
    public bool IsGroup {
      get => _isGroup;
      set {
        _isGroup = value;
        NotifyPropertyChanged();
        NotifyPropertyChanged(nameof(IsDocument));
      } 
    }

    public bool IsDocument => !IsGroup;

    private string _title = string.Empty;
    public string Title {
      get => _title;
      set {
        _title = value;
        NotifyPropertyChanged();
      }
    }

    private DateTime _date;
    public DateTime Date {
      get => _date;
      set {
        _date = value;
        NotifyPropertyChanged();
        NotifyPropertyChanged(nameof(DateAsString));
      }
    }

    public string DateAsString => Date.ToShortDateString();

    private bool _isInvoice;
    public bool IsInvoice {
      get => _isInvoice;
      set {
        _isInvoice = value;
        NotifyPropertyChanged();
        NotifyPropertyChanged(nameof(IsInvoiceAndNotPayed));
      }
    }

    private bool _isInvoicePayed;
    public bool IsInvoicePayed {
      get => _isInvoicePayed;
      set {
        _isInvoicePayed = value;
        NotifyPropertyChanged();
        NotifyPropertyChanged(nameof(PayedText));
        NotifyPropertyChanged(nameof(IsInvoiceAndNotPayed));
      }
    }

    public bool IsInvoiceAndNotPayed => IsInvoice && !IsInvoicePayed;

    private TimeSpan _timeLeftToPay;
    public TimeSpan TimeLeftToPay {
      get => _timeLeftToPay;
      set {
        _timeLeftToPay = value;
        NotifyPropertyChanged();
      }
    }

    private string _pathToDocument = string.Empty;
    public string PathToDocument {
      get => _pathToDocument;
      set {
        _pathToDocument = value;
        NotifyPropertyChanged();
      }
    }

    public string PayedText => IsInvoicePayed ? "Payed" : $"Days left to pay: {TimeLeftToPay.Days}"; // todo: resource

    private void NotifyPropertyChanged([CallerMemberName] string propertyName = "") {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
  }
}
