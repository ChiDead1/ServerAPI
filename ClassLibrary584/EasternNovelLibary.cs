using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ClassLibrary584;

    [Table("EasternNovelLibary")]
[Index(nameof(Name))]
[Index(nameof(Chapter))]
[Index(nameof(Author))]
public class EasternNovelLibary
{
    #region Properties
    /// <summary>
    /// The unique id and primary key for this easternNovelLibary
    /// </summary>
    [Key]
    [Required]
    public int Id { get; set; }

    /// <summary>
    /// easternNovelLibary name (in UTF8 format)
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// EasternNovelLibary Chapteritude
    /// </summary>
    [Column(TypeName = "decimal(7,4)")]
    public decimal Chapter { get; set; }

    /// <summary>
    /// easternNovelLibary name (in UTF8 format)
    /// </summary>
    [Column(TypeName = "decimal(7,4)")]
    public decimal Author { get; set; }

    /// <summary>
    /// Novel Id (foreign key)
    /// </summary>
    [ForeignKey(nameof(Novel))]
    public int CountryId { get; set; }
    #endregion

    #region Navigation Properties
    /// <summary>
    /// The country reChaptered to this easternNovelLibary.
    /// </summary>
    public NovelLibary? Novel { get; set; }
    #endregion
    
}
