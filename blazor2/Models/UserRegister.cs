// tạo ra 1 đối tượng để binding dữ liệu từ form đăng ký, dự liệu này khớp với các trường input trong file register
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class UserRegisterViewModel
{
    [Required(ErrorMessage = "Username không được để trống")]
    [MinLength(3, ErrorMessage = "Username phải có ít nhất 3 ký tự")]
    //maxLength
    [MaxLength(20, ErrorMessage = "Username không được vượt quá 20 ký tự")]
    public string Username { get; set; } = "";

    [Required(ErrorMessage = "Email không được để trống")]
    [EmailAddress(ErrorMessage = "Email không đúng định dạng")]
    public string Email { get; set; } = "";

    [Required(ErrorMessage = "Password không được để trống")]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$", ErrorMessage = "Mật khẩu phải có ít nhất 8 ký tự, bao gồm chữ hoa, chữ thường và số")]
    public string Password { get; set; } = "";
    public bool Gender { get; set; } = false; // true false

    [Required(ErrorMessage = "City không được để trống")]
    public string City { get; set; } = "";
    public List<string> Hobbies { get; set; } = new List<string>();
}