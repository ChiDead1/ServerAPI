using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ClassLibrary584;

[Table("countries")]
public partial class Country
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    [StringLength(10)]
    public string Name { get; set; } = null!;

    [Column("loc2")]
    [StringLength(2)]
    [Unicode(false)]
    public string Loc2 { get; set; } = null!;

    [Column("loc3")]
    [StringLength(3)]
    [Unicode(false)]
    public string Loc3 { get; set; } = null!;

    [InverseProperty("Country")]
    public virtual ICollection<City> Cities { get; } = new List<City>();
}
