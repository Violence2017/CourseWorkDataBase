using System.Collections.Generic;
using CourseWork.Models.Tables;
using CourseWork.ViewModels.Sorts;

namespace CourseWork.ViewModels
{
    public class DishViewModel
    {
        public IEnumerable<Dish> Items { get; set; }
        public PageViewModel PageViewModel { get; set; }
        public DishSort ItemsSort { get; set; }
    }
}