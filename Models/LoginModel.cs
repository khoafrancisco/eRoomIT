using System.ComponentModel.DataAnnotations;
namespace eRoomIT.Models;
public class LoginModel
{
    [Required]
    [Display(Name = "Username or email")] 
    public string UserName { get; set; } = string.Empty;
    [Required(ErrorMessage = "Password is required")]

    [DataType(DataType.Password)]
    public string Password { get; set; } = string.Empty;
    [Display(Name = "Remember Me")]
    public bool RememberMe { get; set; }
}