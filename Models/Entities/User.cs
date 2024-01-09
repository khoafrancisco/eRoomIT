using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace eRoomIT.Models.Entities;

[Table("NguoiDung")] // Maps to the NguoiDung table in your database
public class User
{
    [Key]
    public int NguoiDungID { get; set; }

    [Required]
    public string TenDangNhap { get; set; }  = string.Empty;

    [Required]
    public string MatKhau { get; set; } = string.Empty;

    public int PhanQuyenID { get; set; }

    public string? ThongTinKhac { get; set; }

    [ForeignKey("PhanQuyenID")]
    public Role? Role { get; set; }
}