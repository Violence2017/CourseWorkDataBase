using System.ComponentModel.DataAnnotations;

namespace CourseWork.Models.Tables
{
    [Display(Name = "Ингредиенты блюда")]
    public sealed class DishIngredient
    {
        public enum Sort
        {
            CountAsc,
            CountDesc,
            DishAsc,
            DishDesc,
            IngredientAsc,
            IngredientDesc,
            IngredientCountAsc,
            IngredientCountDesc
        }


        [Display(Name = "Номер")] public int Id { get; set; }

        [Display(Name = "Количество в блюде")]
        [Required(ErrorMessage = "Укажите количество")]
        [Range(1, int.MaxValue, ErrorMessage = "Количество должно быть положительным числом большим нуля")]
        public int Count { get; set; }

        [Display(Name = "Блюдо")]
        [Required(ErrorMessage = "Укажите блюдо")]
        public int DishId { get; set; }

        [Display(Name = "Ингредиент")]
        [Required(ErrorMessage = "Укажите ингредиент")]
        public int IngredientId { get; set; }

        public Dish Dish { get; set; }
        public Ingredient Ingredient { get; set; }
    }
}