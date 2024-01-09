using eRoomIT.Models.Entities;

namespace eRoomIT.Models.ViewModels;
public class RoomViewModel{
    public string? TenPhong { get; set; }
    public int? SoMay { get; set; }
    public string? SoDoPhong { get; set; }
    public string? MoTa { get; set; }

    public List<Computers>? Computers { get; set; }
}