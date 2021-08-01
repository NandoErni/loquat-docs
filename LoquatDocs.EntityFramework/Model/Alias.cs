using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoquatDocs.EntityFramework.Model {
  public class Alias {
    public string AliasName { get; set; }

    [ForeignKey(nameof(AliasGroup))]
    public string AliasGroupName { get; set; }
    public virtual AliasGroup AliasGroup { get; set; }
  }
}
