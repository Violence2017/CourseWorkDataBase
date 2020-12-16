using System.Collections.Generic;
using CourseWork.Models.Tables;
using CourseWork.ViewModels.Filters;
using CourseWork.ViewModels.Sorts;

namespace CourseWork.ViewModels
{
    public class EmployeeViewModel
    {
        public IEnumerable<Employee> Items { get; set; }
        public PageViewModel PageViewModel { get; set; }
        public EmployeeSort ItemsSort { get; set; }
        public EmployeeFilter ItemsFilter { get; set; }
    }
}