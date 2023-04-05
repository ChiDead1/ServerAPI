﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ClassLibrary584;

    [Table("EasternNovelLibary")]
[Index(nameof(Name))]
[Index(nameof(Lat))]
[Index(nameof(Lon))]
public class EasternNovelLibary
{
    #region Properties
    /// <summary>
    /// The unique id and primary key for this City
    /// </summary>
    [Key]
    [Required]
    public int Id { get; set; }

    /// <summary>
    /// City name (in UTF8 format)
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// EasternNovelLibary latitude
    /// </summary>
    [Column(TypeName = "decimal(7,4)")]
    public decimal Lat { get; set; }

    /// <summary>
    /// EasternNovelLibary longitude
    /// </summary>
    [Column(TypeName = "decimal(7,4)")]
    public decimal Lon { get; set; }

    /// <summary>
    /// Novel Id (foreign key)
    /// </summary>
    [ForeignKey(nameof(Novel))]
    public int CountryId { get; set; }
    #endregion

    #region Navigation Properties
    /// <summary>
    /// The country related to this city.
    /// </summary>
    public Novel? Novel { get; set; }
    #endregion
}