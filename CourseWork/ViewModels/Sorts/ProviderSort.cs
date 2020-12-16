using CourseWork.Models.Tables;

namespace CourseWork.ViewModels.Sorts
{
    public class ProviderSort
    {
        public ProviderSort(Provider.Sort? newSortProvider)
        {
            NameSort = newSortProvider == Provider.Sort.NameAsc ? Provider.Sort.NameDesc : Provider.Sort.NameAsc;
            AddressSort = newSortProvider == Provider.Sort.AddressAsc
                ? Provider.Sort.AddressDesc
                : Provider.Sort.AddressAsc;
            PhoneNumberSort = newSortProvider == Provider.Sort.PhoneNumberAsc
                ? Provider.Sort.PhoneNumberDesc
                : Provider.Sort.PhoneNumberAsc;
            Current = newSortProvider;
        }

        public Provider.Sort NameSort { get; }
        public Provider.Sort AddressSort { get; }
        public Provider.Sort PhoneNumberSort { get; }

        public Provider.Sort? Current { get; }
    }
}