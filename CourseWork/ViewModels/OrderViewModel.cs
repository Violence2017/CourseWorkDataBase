using System.Collections.Generic;
using CourseWork.Models.Tables;
using CourseWork.ViewModels.Filters;
using CourseWork.ViewModels.Sorts;

namespace CourseWork.ViewModels
{
    public class OrderViewModel
    {
        public IEnumerable<Order> Items { get; set; }
        public PageViewModel PageViewModel { get; set; }
        public OrderSort ItemsSort { get; set; }
        public OrderFilter ItemsFilter { get; set; }
    }
}