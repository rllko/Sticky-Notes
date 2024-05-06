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
internal class NoteDetailDto
{
    [Key]
    [Column("OWNER_ID")]
    public int Owner_Id { get; }
    [Column("NOTE_ID")]
    public long Note_Id { get; }
    [Column("CREATED_DATE")]
    public DateTime Created_Date { get; }
    [Column("LAST_MODIFIED")]
    public DateTime Last_Modified { get; set; }
    [Column("CONTENT")]
    public string Content { get; set; }
    [Column("CATEGORY")]
    public string Category { get; set; }
}