using System.ComponentModel.DataAnnotations;

namespace Aaron.Models.ViewModels
{
    public class ChangeUsernameViewModel
    {
        [Display(Name = "نام کاربری فعلی")]
        public string CurrentUsername { get; set; } = string.Empty;

        [Required(ErrorMessage = "نام کاربری جدید الزامیست")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "نام کاربری باید حداقل ۳ و حداکثر ۵۰ کاراکتر باشد.")]
        [Display(Name = "نام کاربری جدید")]
        public string NewUsername { get; set; } = string.Empty;

        [Required(ErrorMessage = "رمز عبور فعلی الزامیست")]
        [DataType(DataType.Password)]
        [Display(Name = "رمز عبور فعلی")]
        public string Password { get; set; } = string.Empty;
    }

}
