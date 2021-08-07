using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoquatDocs.ViewModel {
  public enum SearchArguments {
    None = 0,
    Title = 1,
    Tags = 2,
    GroupName = 4,
    FilePath = 8,
    DocumentDate = 16,
    DocumentDueDate = 32,
  }
}
