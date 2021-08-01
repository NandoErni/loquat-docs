using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoquatDocs.EntityFramework.Model {
  public class Group {
    [Key]
    public string Groupname { get; set; }

    public virtual List<Document> Documents { get; set; }
  }
}
