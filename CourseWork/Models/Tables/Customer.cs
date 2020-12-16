using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CourseWork.Models.Tables
{
    [Display(Name = "Заказчики")]
    public sealed class Customer
    {
        public enum Sort
        {
            SurnameAsc,
            SurnameDesc,
            NameAsc,
            NameDesc,
            MiddleNameAsc,
            MiddleNameDesc,
            PhoneNumberAsc,
            PhoneNumberDesc
        }

        public Customer()
        {
            Orders = new HashSet<Order>();
        }

        [Display(Name = "Номер")] public int Id { get; set; }

        [Display(Name = "Фамилия")]
        [Required(ErrorMessage = "Укажите фамилию")]
        [StringLength(100, ErrorMessage = "Количество символов не может превышать {1}")]
        public string Surname { get; set; }

        [Display(Name = "Имя")]
        [Required(ErrorMessage = "Укажите имя")]
        [StringLength(100, ErrorMessage = "Количество символов не может превышать {1}")]
        public string Name { get; set; }

        [Display(Name = "Отчество")]
        [Required(ErrorMessage = "Укажите отчество")]
        [StringLength(100, ErrorMessage = "Количество символов не может превышать {1}")]
        public string MiddleName { get; set; }

        [Display(Name = "Номер телефона")]
        [Required(ErrorMessage = "Укажите номер телефона")]
        [Phone(ErrorMessage = "Введите корректный номер телефона")]
        public string PhoneNumber { get; set; }

        public ICollection<Order> Orders { get; set; }

        public string FullName => Surname + " " + Name;
    }
}