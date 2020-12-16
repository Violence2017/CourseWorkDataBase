using System.Collections.Generic;
using CourseWork.Models.Tables;
using CourseWork.ViewModels.Sorts;

namespace CourseWork.ViewModels
{
    public class ProviderViewModel
    {
        public IEnumerable<Provider> Items { get; set; }
        public PageViewModel PageViewModel { get; set; }
        public ProviderSort ItemsSort { get; set; }
    }
}