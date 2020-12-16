using CourseWork.Models.Tables;

namespace CourseWork.ViewModels.Sorts
{
    public class DishIngredientSort
    {
        public DishIngredientSort(DishIngredient.Sort? newSortOrder)
        {
            CountSort = newSortOrder == DishIngredient.Sort.CountAsc
                ? DishIngredient.Sort.CountDesc
                : DishIngredient.Sort.CountAsc;
            DishSort = newSortOrder == DishIngredient.Sort.DishAsc
                ? DishIngredient.Sort.DishDesc
                : DishIngredient.Sort.DishAsc;
            IngredientSort = newSortOrder == DishIngredient.Sort.IngredientAsc
                ? DishIngredient.Sort.IngredientDesc
                : DishIngredient.Sort.IngredientAsc;
            IngredientCountSort = newSortOrder == DishIngredient.Sort.IngredientCountAsc
                ? DishIngredient.Sort.IngredientCountDesc
                : DishIngredient.Sort.IngredientCountAsc;
            Current = newSortOrder;
        }

        public DishIngredient.Sort CountSort { get; }
        public DishIngredient.Sort DishSort { get; }
        public DishIngredient.Sort IngredientSort { get; }
        public DishIngredient.Sort IngredientCountSort { get; }

        public DishIngredient.Sort? Current { get; }
    }
}