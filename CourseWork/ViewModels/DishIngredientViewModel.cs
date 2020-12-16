using System.Collections.Generic;
using CourseWork.Models.Tables;
using CourseWork.ViewModels.Sorts;

namespace CourseWork.ViewModels
{
    public class DishIngredientViewModel
    {
        public IEnumerable<DishIngredient> Items { get; set; }
        public PageViewModel PageViewModel { get; set; }
        public DishIngredientSort ItemsSort { get; set; }
    }
}