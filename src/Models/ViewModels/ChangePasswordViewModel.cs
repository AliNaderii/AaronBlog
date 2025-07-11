using System.ComponentModel.DataAnnotations;

namespace Aaron.Models.ViewModels
{
    public class ChangePasswordViewModel
    {
        [Required(ErrorMessage = "رمز عبور فعلی الزامیست")]
        [DataType(DataType.Password)]
        [Display(Name = "رمز عبور فعلی")]
        public string CurrentPassword { get; set; } = string.Empty;

        [Required(ErrorMessage = "رمز عبور جدید الزامیست")]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "رمز عبور باید حداقل ۶ کاراکتر باشد.")]
        [Display(Name = "رمز عبور جدید")]
        public string NewPassword { get; set; } = string.Empty;

        [Required(ErrorMessage = "تکرار رمز عبور الزامیست")]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "تکرار رمز عبور با رمز عبور جدید مطابقت ندارد.")]
        [Display(Name = "تکرار رمز عبور جدید")]
        public string ConfirmNewPassword { get; set; } = string.Empty;
    }

}
