using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace StickyNotes.Models;
/// <summary>
///  Represents an external user in the database
/// </summary>
/// <param name="Id">user id</param>
/// <param name="Name">user name</param>
[Table("users")]
internal record UserDtoDetails
{
    [Key]
    [Column("ID")]
    public int Id { get; }
    [Column("CREATED_DATE")]
    public DateTime DateCreated { get; }
    [Column("NAME")]
    public string Name { get; set; }
}