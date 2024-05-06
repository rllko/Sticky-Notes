using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StickyNotes.Models;


/// <summary>
/// a reference to a note in the database
/// </summary>
/// <remarks>
/// Contains the mutable properties Content and Category
/// to update the ones in the db. 
/// </remarks>
[Table("NOTES")]
public record NoteDetailDto
{

    [Key]
    [Column("NOTE_ID")]
    public long Note_Id { get; set; }
    [Column("OWNER_ID")]
    public int Owner_Id { get; set; }
    [Column("CREATED_DATE")]
    public DateTime Created_Date { get; set; }
    [Column("LAST_MODIFIED")]
    public DateTime? Last_Modified { get; set; }
    [Column("CONTENT")]
    public string Content { get; set; }
    [Column("CATEGORY")]
    public string Category { get; set; }
}

[Table("NOTES")]
public record NoteDto
{
    [Key]
    [Column("NOTE_ID")]
    public long Note_Id { get; init; }
    [Column("OWNER_ID")]
    public long Owner_Id { get; set; }
    [Column("CONTENT")]
    public string Content { get; set; }
    [Column("CATEGORY")]
    public string Category { get; set; }
    [Column("LAST_MODIFIED")]
    public DateTime? Last_Modified { get; set; }
}