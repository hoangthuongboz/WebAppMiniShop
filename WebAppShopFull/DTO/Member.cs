using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DTO
{
    public class Member
    {
        public long Id { get; set; }
        [Display(Name ="Tên đăng nhập:")]
        [Required(ErrorMessage ="Vui lòng nhập tên đăng nhập vào nhé!")]
        public string Username { get; set; }
        [Display(Name ="Mật khẩu")]
        [Required(ErrorMessage ="Vui lòng nhập mật khẩu vào nhé")]
        public string Password { get; set; }
        public string OldPassword { get; set; }
        [Display(Name = "Email")]
        [Required(ErrorMessage = "Vui lòng nhập Email vào nhé")]
        public string Email { get; set; }
        public List<Role> Roles { get; set; }
    }
}
