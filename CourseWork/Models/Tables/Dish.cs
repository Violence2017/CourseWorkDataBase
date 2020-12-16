using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CourseWork.Models.Tables
{
    [Display(Name = "Блюда")]
    public sealed class Dish
    {
        public enum Sort
        {
            NameAsc,
            NameDesc,
            CostAsc,
            CostDesc,
            CookingTimeAsc,
            CookingTimeDesc,
            OrderAsc,
            OrderDesc
        }

        public Dish()
        {
            Ingredients = new HashSet<DishIngredient>();
        }

        [Display(Name = "Номер")] public int Id { get; set; }

        [Display(Name = "Название")]
        [Required(ErrorMessage = "Укажите название")]
        [StringLength(80, ErrorMessage = "Количество символов не может превышать 80")]
        public string Name { get; set; }

        [Display(Name = "Стоимость")]
        [Required(ErrorMessage = "Укажите стоимость")]
        [Range(0.1, double.MaxValue, ErrorMessage = "Стоимость должна быть положительным числом больше нуля")]
        [RegularExpression("\\d+([,]\\d{0,})?", ErrorMessage = "Стоимость должна быть положительным числом с плавающей запятой")]
        [DisplayFormat(DataFormatString = "{0:F2}")]
        public double Cost { get; set; }

        [Display(Name = "Время приготовления (в минутах)")]
        [Required(ErrorMessage = "Укажите время приготовления")]
        [Range(1, int.MaxValue, ErrorMessage = "Время приготовления должно быть положительным числом больше нуля")]
        public int CookingTime { get; set; }

        [Display(Name = "Заказ")]
        [Required(ErrorMessage = "Укажите заказ")]
        public int OrderId { get; set; }

        public Order Order { get; set; }
        public ICollection<DishIngredient> Ingredients { get; set; }
    }
}