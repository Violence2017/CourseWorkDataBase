using System.Collections.Generic;
using CourseWork.Models.Tables;
using CourseWork.ViewModels.Filters;
using CourseWork.ViewModels.Sorts;

namespace CourseWork.ViewModels
{
    public class IngredientViewModel
    {
        public IEnumerable<Ingredient> Items { get; set; }
        public PageViewModel PageViewModel { get; set; }
        public IngredientSort ItemsSort { get; set; }
        public IngredientFilter ItemsFilter { get; set; }
    }
}