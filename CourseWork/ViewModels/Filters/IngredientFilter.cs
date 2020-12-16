using System.Collections.Generic;
using CourseWork.Models.Tables;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CourseWork.ViewModels.Filters
{
    public class IngredientFilter
    {
        public IngredientFilter(IList<Provider> providers, int? selectedProviderIndex, int? expirationDate,
            double? cost)
        {
            providers.Insert(0, new Provider {Id = 0, Name = "Все"});
            ProvidersFilter = new SelectList(providers, "Id", "Name", selectedProviderIndex);
            SelectedProviderIndex = selectedProviderIndex;
            ExpirationDateFilter = expirationDate;
            CostFilter = cost;
        }

        public SelectList ProvidersFilter { get; }
        public int? SelectedProviderIndex { get; }
        public int? ExpirationDateFilter { get; }
        public double? CostFilter { get; }
    }
}