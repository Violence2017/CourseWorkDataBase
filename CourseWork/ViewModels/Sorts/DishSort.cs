using CourseWork.Models.Tables;

namespace CourseWork.ViewModels.Sorts
{
    public class DishSort
    {
        public DishSort(Dish.Sort? newSortOrder)
        {
            NameSort = newSortOrder == Dish.Sort.NameAsc ? Dish.Sort.NameDesc : Dish.Sort.NameAsc;
            CostSort = newSortOrder == Dish.Sort.CostAsc ? Dish.Sort.CostDesc : Dish.Sort.CostAsc;
            CookingTimeSort = newSortOrder == Dish.Sort.CookingTimeAsc
                ? Dish.Sort.CookingTimeDesc
                : Dish.Sort.CookingTimeAsc;
            OrderSort = newSortOrder == Dish.Sort.OrderAsc ? Dish.Sort.OrderDesc : Dish.Sort.OrderAsc;
            Current = newSortOrder;
        }

        public Dish.Sort NameSort { get; }
        public Dish.Sort CostSort { get; }
        public Dish.Sort CookingTimeSort { get; }
        public Dish.Sort OrderSort { get; }

        public Dish.Sort? Current { get; }
    }
}