using eRoomIT.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations.Builders;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
  options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
        options.SlidingExpiration = true;
        options.AccessDeniedPath = "/User/AccessDenied/";
        options.LoginPath =  "/User/Login/";
    });

// builder.Services.ConfigureApplicationCookie(optiton => {
//     optiton.LoginPath = "~/Views/Shared/AccessDenied";
//     optiton.LogoutPath = "/Account/Logout";
//     optiton.AccessDeniedPath = "/Shared/AccessDenied";
// });
// Add services to the container.
builder.Services.AddControllersWithViews();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection(); // chuyển hướng http sang https
app.UseStaticFiles();  // sử dụng file tĩnh

app.UseRouting(); // xác định đường dẫn

app.UseAuthentication(); // xác dịnh người dùng
app.UseAuthorization(); // xác định quyền hạn

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
