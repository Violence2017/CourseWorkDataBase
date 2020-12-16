using System.ComponentModel.DataAnnotations;

namespace CourseWork.ViewModels.Identity
{
    public class RegisterViewModel
    {
        [Display(Name = "Email")]
        [Required(ErrorMessage = "{0} является обязательным полем для заполнения")]
        [EmailAddress(ErrorMessage = "Текст в поле {0} не является правильной записью email")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Пользователь с таким {0} уже существует")]
        public string Email { get; set; }

        [Display(Name = "Пароль")]
        [Required(ErrorMessage = "{0} является обязательным полем для заполнения")]
        [StringLength(30, ErrorMessage = "{0} должен содержать минимум {2} и максимум {1} символов", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Подтвердить пароль")]
        [Required(ErrorMessage = "{0} является обязательным полем для заполнения")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Запомнить")] public bool RememberMe { get; set; }

        public string ReturnUrl { get; set; }
    }
}