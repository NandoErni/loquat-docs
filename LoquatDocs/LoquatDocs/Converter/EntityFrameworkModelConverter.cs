using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EF = LoquatDocs.EntityFramework.Model;

namespace LoquatDocs.Converter {
  public class EntityFrameworkModelConverter {
    public static EF.Invoice ConvertToEFInvoice(string documentPath, DateTime dueDate, bool isPayed) {
      EF.Invoice invoice = new EF.Invoice();

      invoice.DocumentPath = documentPath;
      invoice.DueDate = dueDate;
      invoice.IsPayed = isPayed;

      return invoice;
    }

    public static List<EF.Tag> ConvertToEFTag(IList<string> tags, string documentPath) {
      var efTags = new List<EF.Tag>();

      foreach (string tag in tags) {
        efTags.Add(new EF.Tag() { TagId = tag, DocumentPath = documentPath });
      }

      return efTags;
    }

    public static EF.Document ConvertToEFDocument(string groupName, string title, string documentPath, DateTime dateOfDocument) {
      var document = new EF.Document();
      document.Groupname = groupName;
      document.Title = title;
      document.DocumentPath = documentPath;
      document.DocumentDate = dateOfDocument;
      document.DocumentAdded = DateTime.Now;

      return document;
    }
  }
}
