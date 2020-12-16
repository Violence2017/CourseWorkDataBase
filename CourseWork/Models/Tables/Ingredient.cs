using System;
using System.ComponentModel.DataAnnotations;

namespace CourseWork.Models.Tables
{
    [Display(Name = "Ингредиенты")]
    public sealed class Ingredient
    {
        public enum Sort
        {
            NameAsc,
            NameDesc,
            ReleaseDateAsc,
            ReleaseDateDesc,
            CountAsc,
            CountDesc,
            CostAsc,
            CostDesc,
            ExpirationDateAsc,
            ExpirationDateDesc,
            ProviderAsc,
            ProviderDesc
        }


        [Display(Name = "Номер")] public int Id { get; set; }

        [Display(Name = "Название")]
        [Required(ErrorMessage = "Укажите название")]
        [StringLength(80, ErrorMessage = "Количество символов не может превышать 80")]
        public string Name { get; set; }

        [Display(Name = "Дата выпуска")]
        [Required(ErrorMessage = "Укажите дату выпуска")]
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }

        [Display(Name = "Количество")]
        [Required(ErrorMessage = "Укажите количество")]
        [Range(0, int.MaxValue, ErrorMessage = "Количество должно быть положительным числом")]
        public int Count { get; set; }

        [Display(Name = "Стоимость")]
        [Required(ErrorMessage = "Укажите стоимость")]
        [Range(0.1, double.MaxValue, ErrorMessage = "Стоимость должна быть положительным числом больше нуля")]
        [RegularExpression("\\d+([,]\\d{0,})?", ErrorMessage = "Стоимость должна быть положительным числом с плавающей точкой")]
        [DisplayFormat(DataFormatString = "{0:F2}")]
        public double Cost { get; set; }

        [Display(Name = "Срок годности (в сутках)")]
        [Required(ErrorMessage = "Укажите срок годности")]
        [Range(1, int.MaxValue, ErrorMessage = "Срок годности должен быть положительным числом больше нуля")]
        public int ExpirationDate { get; set; }

        [Display(Name = "Поставщик")]
        [Required(ErrorMessage = "Укажите поставщика")]
        public int ProviderId { get; set; }

        public Provider Provider { get; set; }
    }
}