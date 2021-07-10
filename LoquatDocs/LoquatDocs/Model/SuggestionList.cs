using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoquatDocs.Model {
  public class SuggestionList<T> {

    public List<T> BaseList;

    public List<T> FilteredList;

    public SuggestionList() {
      BaseList = new List<T>();
      FilteredList = new List<T>();
    }
  }
}
