using CourseWork.Models.Tables;

namespace CourseWork.ViewModels.Sorts
{
    public class IngredientSort
    {
        public IngredientSort(Ingredient.Sort? newSortOrder)
        {
            NameSort = newSortOrder == Ingredient.Sort.NameAsc ? Ingredient.Sort.NameDesc : Ingredient.Sort.NameAsc;
            ReleaseDateSort = newSortOrder == Ingredient.Sort.ReleaseDateAsc
                ? Ingredient.Sort.ReleaseDateDesc
                : Ingredient.Sort.ReleaseDateAsc;
            CountSort = newSortOrder == Ingredient.Sort.CountAsc ? Ingredient.Sort.CountDesc : Ingredient.Sort.CountAsc;
            CostSort = newSortOrder == Ingredient.Sort.CostAsc ? Ingredient.Sort.CostDesc : Ingredient.Sort.CostAsc;
            ExpirationDateSort = newSortOrder == Ingredient.Sort.ExpirationDateAsc
                ? Ingredient.Sort.ExpirationDateDesc
                : Ingredient.Sort.ExpirationDateAsc;
            ProviderSort = newSortOrder == Ingredient.Sort.ProviderAsc
                ? Ingredient.Sort.ProviderDesc
                : Ingredient.Sort.ProviderAsc;
            Current = newSortOrder;
        }

        public Ingredient.Sort NameSort { get; }
        public Ingredient.Sort ReleaseDateSort { get; }
        public Ingredient.Sort CountSort { get; }
        public Ingredient.Sort CostSort { get; }
        public Ingredient.Sort ExpirationDateSort { get; }
        public Ingredient.Sort ProviderSort { get; }

        public Ingredient.Sort? Current { get; }
    }
}