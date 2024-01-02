using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using eRoomIT.Models;
using Microsoft.AspNetCore.Authorization;
using eRoomIT.Models.Entities;

namespace eRoomIT.Controllers;

[Authorize(Roles = "1, 2")]
public class UsersManagementController : Controller
{
    private readonly ILogger<UsersManagementController> _logger;
    private readonly AppDbContext _appDbContext;

    public UsersManagementController(ILogger<UsersManagementController> logger,
    AppDbContext appDbContext)
    {
        _logger = logger;
        _appDbContext = appDbContext;
    }
    public IActionResult Index()
    {
       List<User> users = _appDbContext.Users.ToList();
        return View(users);
    }
     public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(User user)
    {
        if (ModelState.IsValid)
        {
            _appDbContext.Add(user);
            await _appDbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(user);
    }

     public IActionResult Edit(int id)
    {
        _logger.LogInformation(id.ToString());
        User? users = _appDbContext.Users.Where(x=>x.NguoiDungID == id).FirstOrDefault();
        if (users == null)
        {
            return NotFound("Không tìm thấy người dùng");
        }
        else
        {
            return View(users);
        }
    }

    [HttpPost]
    public async Task<IActionResult> Edit(User users)
    {
        if (ModelState.IsValid)
        {
            _logger.LogInformation(users.NguoiDungID.ToString());
             User? oldUser = _appDbContext.Users.Where(x=>x.NguoiDungID == users.NguoiDungID).FirstOrDefault();
             if(oldUser == null)
             {
                 return NotFound("Không tìm thấy nguoi dung");
             }
             else
             {
                 oldUser.TenDangNhap = users.TenDangNhap;
                 oldUser.MatKhau = users.MatKhau;
                 oldUser.PhanQuyenID = users.PhanQuyenID;
                 oldUser.ThongTinKhac = users.ThongTinKhac;
                 _appDbContext.Users.Update(oldUser);
                 await _appDbContext.SaveChangesAsync();
                 return RedirectToAction(nameof(Index));
             }

        }
        return View(users);

    }
}
