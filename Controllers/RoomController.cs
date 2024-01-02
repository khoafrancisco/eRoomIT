using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using eRoomIT.Models;
using Microsoft.AspNetCore.Authorization;
using eRoomIT.Models.Entities;

namespace eRoomIT.Controllers;

[Authorize(Roles = "1, 2")]
public class RoomController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly AppDbContext _appDbContext;

    public RoomController(ILogger<HomeController> logger,
    AppDbContext appDbContext)
    {
        _logger = logger;
        _appDbContext = appDbContext;
    }
    public IActionResult Index()
    {
       List<Room> rooms = _appDbContext.Rooms.ToList();
        return View(rooms);
    }
     public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Room room)
    {
        if (ModelState.IsValid)
        {
            _appDbContext.Add(room);
            await _appDbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(room);
    }

     public IActionResult Edit(int id)
    {
        _logger.LogInformation(id.ToString());
        Room? room = _appDbContext.Rooms.Where(x=>x.PhongMayID == id).FirstOrDefault();
        if (room == null)
        {
            return NotFound("Không tìm thấy phòng máy");
        }
        else
        {
            return View(room);
        }
    }

    [HttpPost]
    public async Task<IActionResult> Edit(Room room)
    {
        if (ModelState.IsValid)
        {
            _logger.LogInformation(room.PhongMayID.ToString());
             Room? oldRoom = _appDbContext.Rooms.Where(x=>x.PhongMayID == room.PhongMayID).FirstOrDefault();
             if(oldRoom == null)
             {
                 return NotFound("Không tìm thấy phòng máy");
             }
             else
             {
                 oldRoom.TenPhong = room.TenPhong;
                 oldRoom.SoMay = room.SoMay;
                 oldRoom.SoDoPhong = room.SoDoPhong;
                 oldRoom.MoTa = room.MoTa;
                 _appDbContext.Rooms.Update(oldRoom);
                 await _appDbContext.SaveChangesAsync();
                 return RedirectToAction(nameof(Index));
             }

        }
        return View(room);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteAsync(int id)
    {

        Room? room = _appDbContext.Rooms.Where(x=>x.PhongMayID == id).FirstOrDefault();
        if(room == null)
        {
            return NotFound("Không tìm thấy phòng máy");
        }
        else
        {
            _appDbContext.Rooms.Remove(room);
            await _appDbContext.SaveChangesAsync();
            return Json(new { success = true, message = "Xóa thành công" });
        }
    }
}
