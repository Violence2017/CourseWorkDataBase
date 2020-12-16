using System.Collections.Generic;
using CourseWork.Models.Tables;
using CourseWork.ViewModels.Sorts;

namespace CourseWork.ViewModels
{
    public class CustomerViewModel
    {
        public IEnumerable<Customer> Items { get; set; }
        public PageViewModel PageViewModel { get; set; }
        public CustomerSort ItemsSort { get; set; }
    }
}