namespace CourseWork.ViewModels.Filters
{
    public class EmployeeFilter
    {
        public EmployeeFilter(string position, string education)
        {
            PositionFilter = position;
            EducationFilter = education;
        }

        public string PositionFilter { get; }
        public string EducationFilter { get; }
    }
}