using CourseWork.Models.Tables;

namespace CourseWork.ViewModels.Sorts
{
    public class OrderSort
    {
        public OrderSort(Order.Sort? newSortOrder)
        {
            DateSort = newSortOrder == Order.Sort.DateAsc ? Order.Sort.DateDesc : Order.Sort.DateAsc;
            TimeSort = newSortOrder == Order.Sort.TimeAsc ? Order.Sort.TimeDesc : Order.Sort.TimeAsc;
            CostSort = newSortOrder == Order.Sort.CostAsc ? Order.Sort.CostDesc : Order.Sort.CostAsc;
            PaymentSort = newSortOrder == Order.Sort.PaymentAsc ? Order.Sort.PaymentDesc : Order.Sort.PaymentAsc;
            CompletedSort = newSortOrder == Order.Sort.CompletedAsc
                ? Order.Sort.CompletedDesc
                : Order.Sort.CompletedAsc;
            CustomerSort = newSortOrder == Order.Sort.CustomerAsc ? Order.Sort.CustomerDesc : Order.Sort.CustomerAsc;
            EmployeeSort = newSortOrder == Order.Sort.EmployeeAsc ? Order.Sort.EmployeeDesc : Order.Sort.EmployeeAsc;
            Current = newSortOrder;
        }

        public Order.Sort DateSort { get; }
        public Order.Sort TimeSort { get; }
        public Order.Sort CostSort { get; }
        public Order.Sort PaymentSort { get; }
        public Order.Sort CompletedSort { get; }
        public Order.Sort CustomerSort { get; }
        public Order.Sort EmployeeSort { get; }

        public Order.Sort? Current { get; }
    }
}