using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using eRoomIT.Models;
using Microsoft.AspNetCore.Authorization;
using eRoomIT.Models.Entities;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Identity.Client;

namespace eRoomIT.Controllers;

public class UserController : Controller
{
    private readonly ILogger<UserController> _logger;
    private readonly AppDbContext _dbContext;

    public UserController(ILogger<UserController> logger, AppDbContext context)
    {
        _logger = logger;
        _dbContext = context;
    }
    public IActionResult Login()
    {
        return View();
    }
    [HttpPost]
     public async Task<IActionResult> Login(LoginModel loginModel, string? returnUrl = null)
    {
        if (ModelState.IsValid)
        {
            User? user = _dbContext.Users.Where(u => u.TenDangNhap == loginModel.UserName && u.MatKhau == loginModel.Password).FirstOrDefault();
            if (user != null)
            {
               var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.TenDangNhap),
                    new Claim(ClaimTypes.Role, user.PhanQuyenID.ToString()),
                };

                var claimsIdentity = new ClaimsIdentity(
                    claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties
                {
                    //AllowRefresh = <bool>,
                    // Refreshing the authentication session should be allowed.

                    ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(5),
                    // The time at which the authentication ticket expires. A 
                    // value set here overrides the ExpireTimeSpan option of 
                    // CookieAuthenticationOptions set with AddCookie.

                    //IsPersistent = true,
                    // Whether the authentication session is persisted across 
                    // multiple requests. When used with cookies, controls
                    // whether the cookie's lifetime is absolute (matching the
                    // lifetime of the authentication ticket) or session-based.

                    //IssuedUtc = <DateTimeOffset>,
                    // The time at which the authentication ticket was issued.

                    //RedirectUri = <string>
                    // The full path or absolute URI to be used as an http 
                    // redirect response value.
                };
                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme, 
                    new ClaimsPrincipal(claimsIdentity), 
                    authProperties);
               return LocalRedirect(returnUrl ?? "/Home/Index");
            }
            else
            {
                ViewBag.Message = "Tai khoan khong ton tai";
            }
        }
        else
        {
            ViewBag.Message = "Khong hop le";
        }
        return View(loginModel);

    }

     public IActionResult AccessDenied()
    {
        return View();
    }
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Login", "User");
    }

}
