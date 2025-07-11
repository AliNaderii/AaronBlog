using System.ComponentModel.DataAnnotations;

namespace Aaron.Models.ViewModels
{
    public class AdminViewModel
    {
        [Required(ErrorMessage = "لطفا نام کاربری خود را وارد کنید")]
        [StringLength(maximumLength:20, MinimumLength =3, ErrorMessage = "نام کاربری باید بین 3 تا 20 کاراکتر باشد")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "لطفا رمز عبور خود را وارد کنید")]
        public string Password { get; set; } = string.Empty;
    }
}
