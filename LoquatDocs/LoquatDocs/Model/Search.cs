using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoquatDocs.Model {
  public class Search {
    public string QueryText = string.Empty;
    public ObservableCollection<DocumentListItem> DocumentListItems;
  }
}
