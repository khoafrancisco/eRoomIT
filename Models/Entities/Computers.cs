using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eRoomIT.Models.Entities;

[Table("MayTinh")]
public class Computers
{
    [Key]
    public int MayTinhID { get; set; }
    public int PhongMayID { get; set; }
    public string ? TenMay { get; set; }
    public string ? CPU { get; set; }
    public string ? RAM { get; set; }
    public string ? BoNho { get; set; }
    public string? ManHinh { get; set; }
    public int TinhTrangID { get; set; }
}
