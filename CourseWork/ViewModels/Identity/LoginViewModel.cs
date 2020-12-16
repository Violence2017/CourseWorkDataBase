using System.ComponentModel.DataAnnotations;

namespace CourseWork.ViewModels.Identity
{
    public class LoginViewModel
    {
        [Display(Name = "Email")]
        [Required(ErrorMessage = "{0} является обязательным полем для заполнения")]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "Пароль")]
        [Required(ErrorMessage = "{0} является обязательным полем для заполнения")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Запомнить")] public bool RememberMe { get; set; }

        public string ReturnUrl { get; set; }
    }
}