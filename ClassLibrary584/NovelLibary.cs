﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ClassLibrary584;

    [Table("NovelLibary")]
[Index(nameof(Name))]
[Index(nameof(ISO2))]
[Index(nameof(ISO3))]
public class NovelLibary
{
    #region Properties
    /// <summary>
    /// The unique id and primary key for this Country
    /// </summary>
    [Key]
    [Required]
    public int Id { get; set; }

    /// <summary>
    /// Country name (in UTF8 format)
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Country code (in ISO 3166-1 ALPHA-2 format)
    /// </summary>
    public string ISO2 { get; set; } = null!;

    /// <summary>
    /// Country code (in ISO 3166-1 ALPHA-3 format)
    /// </summary>
    public string ISO3 { get; set; } = null!;
    #endregion

    #region Navigation Properties
    /// <summary>
    /// A collection of all the cities related to this country.
    /// </summary>
    public ICollection<EasternNovelLibary>? EasternN { get; set; } = null!;
    #endregion
}
