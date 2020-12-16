using CourseWork.Models.Tables;

namespace CourseWork.ViewModels.Sorts
{
    public class CustomerSort
    {
        public CustomerSort(Customer.Sort? newSortOrder)
        {
            SurnameSort = newSortOrder == Customer.Sort.SurnameAsc
                ? Customer.Sort.SurnameDesc
                : Customer.Sort.SurnameAsc;
            NameSort = newSortOrder == Customer.Sort.NameAsc ? Customer.Sort.NameDesc : Customer.Sort.NameAsc;
            MiddleNameSort = newSortOrder == Customer.Sort.MiddleNameAsc
                ? Customer.Sort.MiddleNameDesc
                : Customer.Sort.MiddleNameAsc;
            PhoneNumberSort = newSortOrder == Customer.Sort.PhoneNumberAsc
                ? Customer.Sort.PhoneNumberDesc
                : Customer.Sort.PhoneNumberAsc;
            Current = newSortOrder;
        }

        public Customer.Sort SurnameSort { get; }
        public Customer.Sort NameSort { get; }
        public Customer.Sort MiddleNameSort { get; }
        public Customer.Sort PhoneNumberSort { get; }

        public Customer.Sort? Current { get; }
    }
}