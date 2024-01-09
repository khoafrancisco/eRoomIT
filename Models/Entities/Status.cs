using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using eRoomIT.Models.Entities;

[Table("TinhTrang")]
public class Status{
    [Key]
    public int TinhTrangID { get; set; }
    public string? TrangThai { get; set; }
}