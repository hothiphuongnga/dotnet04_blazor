using System.ComponentModel.DataAnnotations;

public class UserLoginVM
{
    [Required(ErrorMessage = "User Name is required")]
    public string UserName { get; set; }
    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; }
}