using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Design;

namespace LoquatDocs.EntityFramework.Model {
  public class Document {
    [Key]
    public string Path { get; set; }

    public string Title { get; set; }
  }
}
