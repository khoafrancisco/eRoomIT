using Microsoft.AspNetCore.Mvc;
using eRoomIT.Models;
using Microsoft.AspNetCore.Authorization;
using eRoomIT.Models.Entities;

namespace eRoomIT.Controllers;
[Authorize(Roles = "1, 2")]

public class ComputersController : Controller
{
    private readonly ILogger<ComputersController> _logger;
    private readonly AppDbContext _appDbContext;
    public ComputersController(ILogger<ComputersController> logger,
    AppDbContext appDbContext)
    {
        _logger = logger;
        _appDbContext = appDbContext;
    }
    // GET: /Computers
    public IActionResult Index()
    {
        List<Computers> computers = _appDbContext.Computer.ToList();
        return View(computers);
    }

    // GET: /Computers/Details/5
    public IActionResult Details(int id)
    {
        return View();
    }

    // GET: /Computers/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: /Computers/Create
    // [HttpPost]
    // public IActionResult Create(ComputersController computers)
    // {
    //     // Lưu thông tin máy tính vào cơ sở dữ liệu
    //     return RedirectToAction("Index");
    // }

    // // GET: /Computers/Edit/5
    // public IActionResult Edit(int id)
    // {
    //     return View();
    // }

    // // POST: /Computers/Edit/5
    // [HttpPost]
    // public IActionResult Edit(int id, ComputersController computers)
    // {
    //     // Cập nhật thông tin máy tính trong cơ sở dữ liệu
    //     return RedirectToAction("Index");
    // }

    // // GET: /Computers/Delete/5
    // public IActionResult Delete(int id)
    // {
    //     return View();
    // }

    // // POST: /Computers/Delete/5
    // [HttpPost]
    // public IActionResult Delete(int id, ComputersController computers)
    // {
    //     // Xóa máy tính khỏi cơ sở dữ liệu
    //     return RedirectToAction("Index");
    // }
}
