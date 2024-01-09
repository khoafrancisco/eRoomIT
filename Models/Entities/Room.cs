using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace eRoomIT.Models.Entities;

[Table("PhongMay")]
public class Room
{
    [Key]
    public int ? PhongMayID { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập tên phòng")]
    public string? TenPhong { get; set; }

    [Range(1, 10000, ErrorMessage = "Vui lòng nhập số máy từ 1 đến 10000")]
    public int? SoMay { get; set; }

    public string? SoDoPhong { get; set; }
    [AllowedExtensions(new string[] { ".jpg", ".jpeg", ".png" })]
    [NotMapped]
    public IFormFile? UploadFile { get; set; }
    public string? MoTa { get; set; }
}
