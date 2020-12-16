using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CourseWork.Models.Tables
{
    [Display(Name = "Поставщики")]
    public sealed class Provider
    {
        public enum Sort
        {
            NameAsc,
            NameDesc,
            AddressAsc,
            AddressDesc,
            PhoneNumberAsc,
            PhoneNumberDesc
        }

        public Provider()
        {
            Ingredients = new HashSet<Ingredient>();
        }

        [Display(Name = "Номер")] public int Id { get; set; }

        [Display(Name = "Название")]
        [Required(ErrorMessage = "Укажите название")]
        [StringLength(100, MinimumLength = 5,
            ErrorMessage = "Количество символов не может превышать 100 или быть меньше 5")]
        public string Name { get; set; }

        [Display(Name = "Адрес")]
        [Required(ErrorMessage = "Укажите адрес")]
        [StringLength(100, MinimumLength = 5,
            ErrorMessage = "Количество символов не может превышать 100 или быть меньше 5")]
        public string Address { get; set; }

        [Display(Name = "Номер телефона")]
        [Required(ErrorMessage = "Укажите номер телефона")]
        [Phone(ErrorMessage = "Введите корректный номер телефона")]
        public string PhoneNumber { get; set; }

        public ICollection<Ingredient> Ingredients { get; set; }
    }
}