using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using eRoomIT.Models;
using Microsoft.AspNetCore.Authorization;
using eRoomIT.Models.Entities;
using eRoomIT.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace eRoomIT.Controllers;

[Authorize(Roles = "Admin, User")]
public class HomeController : Controller
{

    private readonly ILogger<HomeController> _logger;
    private readonly AppDbContext _appDbContext;


    public HomeController(ILogger<HomeController> logger, AppDbContext context)
    {
        _logger = logger;
        _appDbContext = context;
    }
    public IActionResult Index()
    {
        List<Room> rooms = _appDbContext.Rooms.ToList();
        return View(rooms);
    }
    public IActionResult Detail(int id)
    {
        Room? room = _appDbContext.Rooms.Where(r => r.PhongMayID == id).FirstOrDefault();
        if (room == null)
        {
            return NotFound("Không tìm thấy phòng máy");
        }
        else
        {
            List<Computers> _computers = _appDbContext.Computer.Where(c => c.PhongMayID == id).ToList();
            foreach (var computer in _computers)
            {
                computer.Status = _appDbContext.Status.Where(s => s.TinhTrangID == computer.TinhTrangID).FirstOrDefault();
            }
            RoomViewModel roomViewModel = new RoomViewModel();
            roomViewModel.TenPhong = room.TenPhong;
            roomViewModel.SoDoPhong = room.SoDoPhong;
            roomViewModel.SoMay = _computers.Count;
            roomViewModel.MoTa = room.MoTa;
            roomViewModel.Computers = _computers;
            return View(roomViewModel);
        }
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
