using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CourseWork.Models.Tables
{
    [Display(Name = "Заказы")]
    public sealed class Order
    {
        public enum PaymentType
        {
            [Display(Name = "Оплата наличными")] Cash = 1,

            [Display(Name = "Оплата кредитной картой")]
            CreditCard = 2
        }

        public enum Sort
        {
            DateAsc,
            DateDesc,
            TimeAsc,
            TimeDesc,
            CostAsc,
            CostDesc,
            PaymentAsc,
            PaymentDesc,
            CompletedAsc,
            CompletedDesc,
            CustomerAsc,
            CustomerDesc,
            EmployeeAsc,
            EmployeeDesc
        }

        public Order()
        {
            Dishes = new HashSet<Dish>();
        }

        [Display(Name = "Номер")] public int Id { get; set; }

        [Display(Name = "Дата заказа")]
        [Required(ErrorMessage = "Укажите дату заказа")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; } = System.DateTime.Now;

        [Display(Name = "Время заказа")]
        [Required(ErrorMessage = "Укажите время заказа")]
        [DataType(DataType.Time)]
        public DateTime Time { get; set; } = System.DateTime.Now;

        [Display(Name = "Стоимость")]
        [Required(ErrorMessage = "Укажите стоимость")]
        [Range(0.1, double.MaxValue, ErrorMessage = "Стоимость должна быть положительным числом больше нуля")]
        [RegularExpression("\\d+([,]\\d{0,})?", ErrorMessage = "Стоимость должна быть положительным числом с плавающей запятой")]
        [DisplayFormat(DataFormatString = "{0:F2}")]
        public double Cost { get; set; }

        [Display(Name = "Тип оплаты")]
        [Required(ErrorMessage = "Укажите тип оплаты")]
        public PaymentType Payment { get; set; }

        [Display(Name = "Выполнен")]
        [Required(ErrorMessage = "Укажите выполнен ли заказ")]
        public bool Completed { get; set; }

        [Display(Name = "Заказчик")]
        [Required(ErrorMessage = "Укажите заказчика")]
        public int CustomerId { get; set; }

        [Display(Name = "Сотрудник")]
        [Required(ErrorMessage = "Укажите сотрудника")]
        public int EmployeeId { get; set; }

        public Customer Customer { get; set; }
        public Employee Employee { get; set; }
        public ICollection<Dish> Dishes { get; set; }

        public string DateTime => Date.ToShortDateString() + " " + Time.ToShortTimeString();
    }
}