using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using eRoomIT.Models.Entities;

[Table("PhanQuyen")]
public class Role{
    [Key]
    public int PhanQuyenID { get; set; }
    public string? MoTa { get; set; }
}