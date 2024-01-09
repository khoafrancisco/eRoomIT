using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using eRoomIT.Models;
using Microsoft.AspNetCore.Authorization;
using eRoomIT.Models.Entities;

namespace eRoomIT.Controllers;

[Authorize(Roles = "Admin")]
public class RoomController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly AppDbContext _appDbContext;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public RoomController(ILogger<HomeController> logger,
    AppDbContext appDbContext, IWebHostEnvironment webHostEnvironment)
    {
        _logger = logger;
        _appDbContext = appDbContext;
        _webHostEnvironment = webHostEnvironment;
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
            if(room.UploadFile != null)
                {
                    var fileName = Guid.NewGuid().ToString()  + Path.GetExtension(room.UploadFile.FileName);
                    var file = Path.Combine(_webHostEnvironment.WebRootPath, "images",fileName );
                    var stream = new FileStream(file, FileMode.Create);
                    await room.UploadFile.CopyToAsync(stream);
                    room.SoDoPhong = fileName;
                }
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
             Room? oldRoom = _appDbContext.Rooms.Where(x=>x.PhongMayID == room.PhongMayID).FirstOrDefault();
             if(oldRoom == null)
             {
                 return NotFound("Không tìm thấy phòng máy");
             }
             else
             {
                
                if(room.UploadFile != null)
                {
                    var fileName = Guid.NewGuid().ToString()  + Path.GetExtension(room.UploadFile.FileName);
                    var file = Path.Combine(_webHostEnvironment.WebRootPath, "images",fileName );
                    var stream = new FileStream(file, FileMode.Create);
                    await room.UploadFile.CopyToAsync(stream);
                    oldRoom.SoDoPhong = fileName;
                }
                 oldRoom.TenPhong = room.TenPhong;
                 oldRoom.SoMay = room.SoMay;
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
