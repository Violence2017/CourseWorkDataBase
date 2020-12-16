using CourseWork.Models.Tables;

namespace CourseWork.ViewModels.Sorts
{
    public class EmployeeSort
    {
        public EmployeeSort(Employee.Sort? newSortOrder)
        {
            SurnameSort = newSortOrder == Employee.Sort.SurnameAsc
                ? Employee.Sort.SurnameDesc
                : Employee.Sort.SurnameAsc;
            NameSort = newSortOrder == Employee.Sort.NameAsc ? Employee.Sort.NameDesc : Employee.Sort.NameAsc;
            MiddleNameSort = newSortOrder == Employee.Sort.MiddleNameAsc
                ? Employee.Sort.MiddleNameDesc
                : Employee.Sort.MiddleNameAsc;
            PositionSort = newSortOrder == Employee.Sort.PositionAsc
                ? Employee.Sort.PositionDesc
                : Employee.Sort.PositionAsc;
            EducationSort = newSortOrder == Employee.Sort.EducationAsc
                ? Employee.Sort.EducationDesc
                : Employee.Sort.EducationAsc;
            PhoneNumberSort = newSortOrder == Employee.Sort.PhoneNumberAsc
                ? Employee.Sort.PhoneNumberDesc
                : Employee.Sort.PhoneNumberAsc;
            Current = newSortOrder;
        }

        public Employee.Sort SurnameSort { get; }
        public Employee.Sort NameSort { get; }
        public Employee.Sort MiddleNameSort { get; }
        public Employee.Sort PositionSort { get; }
        public Employee.Sort EducationSort { get; }
        public Employee.Sort PhoneNumberSort { get; }

        public Employee.Sort? Current { get; }
    }
}