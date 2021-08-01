using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoquatDocs.EntityFramework.Model {
  public class Tag {
    public string TagId { get; set; }

    [ForeignKey(nameof(Document))]
    public string DocumentPath { get; set; }

    public virtual Document Document { get; set; }
  }
}
