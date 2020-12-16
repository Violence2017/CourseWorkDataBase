using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using CourseWork.Models.Tables;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CourseWork.ViewModels.Filters
{
    public class OrderFilter
    {
        public OrderFilter(IList<Employee> employees, int? selectedEmployeeIndex, int? dishCount, IList<Dish> dishes,
            int? selectedDishIndex, int? selectedPaymentTypeIndex)
        {
            employees.Insert(0, new Employee {Id = 0, Name = "Все"});
            EmployeesFilter = new SelectList(employees, "Id", "FullName", selectedEmployeeIndex);
            SelectedEmployeeIndex = selectedEmployeeIndex;
            DishCountFilter = dishCount;
            dishes.Insert(0, new Dish {Id = 0, Name = "Все"});
            DishesFilter = new SelectList(dishes, "Id", "Name", selectedDishIndex);
            SelectedDishIndex = selectedDishIndex;
            PaymentTypesFilter = CreatePaymentTypeSelectList(selectedPaymentTypeIndex);
            SelectedPaymentTypeIndex = selectedPaymentTypeIndex;
        }

        public SelectList EmployeesFilter { get; }
        public int? SelectedEmployeeIndex { get; }
        public int? DishCountFilter { get; }
        public SelectList DishesFilter { get; }
        public int? SelectedDishIndex { get; }
        public SelectList PaymentTypesFilter { get; }
        public int? SelectedPaymentTypeIndex { get; }

        private static IEnumerable<string> GetPaymentTypeNames()
        {
            IList<string> names = new List<string>();
            var members = typeof(Order.PaymentType).GetMembers();
            foreach (var memberInfo in members)
            {
                var attribute = memberInfo.GetCustomAttribute(typeof(DisplayAttribute));
                if (attribute is DisplayAttribute displayAttribute) names.Add(displayAttribute.Name);
            }

            return names;
        }

        private static SelectList CreatePaymentTypeSelectList(int? selectedPaymentTypeIndex)
        {
            var paymentTypeNames = GetPaymentTypeNames();
            var paymentTypes = Enum.GetValues(typeof(Order.PaymentType))
                .Cast<Order.PaymentType>()
                .Zip(paymentTypeNames)
                .Select(o => new {Id = (int) o.First, Text = o.Second})
                .ToList();
            paymentTypes.Insert(0, new {Id = 0, Text = "Все"});
            return new SelectList(paymentTypes, "Id", "Text", selectedPaymentTypeIndex);
        }
    }
}