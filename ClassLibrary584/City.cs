using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ClassLibrary584;

[Table("cities")]
public partial class City
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    [StringLength(10)]
    public string Name { get; set; } = null!;

    [Column("lat", TypeName = "decimal(18, 4)")]
    public decimal Lat { get; set; }

    [Column("lon", TypeName = "decimal(18, 0)")]
    public decimal Lon { get; set; }
    
    public int Popluations { get; set; }

    [Column("countryID")]
    public int? CountryId { get; set; }

    [ForeignKey("CountryId")] 
    [InverseProperty("Cities")]
    public virtual Country? Country { get; set; }
}
