using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Design;

namespace LoquatDocs.EntityFramework.Model {
  public class Document {
    [Key]
    public string DocumentPath { get; set; }

    [Required]
    public string Title { get; set; }

    [Required]
    public DateTime DocumentDate { get; set; }

    [Required]
    public DateTime DocumentAdded { get; set; }

    [ForeignKey(nameof(Group))]
    public string Groupname { get; set; }

    public virtual Group Group { get; set; }

    public List<Tag> Tags { get; set; }

    public virtual List<Invoice> Invoices { get; set; }
  }
}
