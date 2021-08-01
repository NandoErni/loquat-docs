using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoquatDocs.EntityFramework.Model {
  public class AliasGroup {
    [Key]
    public string AliasGroupName { get; set; }

    public virtual List<Alias> Aliases { get; set; }
  }
}
