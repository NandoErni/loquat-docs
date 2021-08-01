using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoquatDocs.EntityFramework.Model {
  public class Invoice {

    [Required]
    public bool IsPayed { get; set; }

    [Required]
    public DateTime DueDate { get; set; }

    [Key]
    [ForeignKey(nameof(Document))]
    public string DocumentPath { get; set; }

    public virtual Document Document { get; set; }
  }
}
