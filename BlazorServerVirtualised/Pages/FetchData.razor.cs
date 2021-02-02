using BlazorServerVirtualised.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web.Virtualization;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorServerVirtualised.Pages
{
    public partial class FetchData
    {
        [Inject] WeatherForecastService ForecastService { get;set; }
        private IQueryable<WeatherForecast> forecasts;
        protected override async Task OnInitializedAsync()
        {
            forecasts = (await ForecastService.GetForecastAsync(DateTime.Now)).AsQueryable();
            ItemsCount = forecasts.Count();
        }

        public int ItemsCount { get; set; }

        private async ValueTask<ItemsProviderResult<WeatherForecast>> LoadItems(ItemsProviderRequest request)
        {
            var numItems = Math.Min(request.Count, ItemsCount - request.StartIndex);
            var items = forecasts.Skip(request.StartIndex).Take(numItems).ToList();
            return new ItemsProviderResult<WeatherForecast>(items, ItemsCount);
        }
    }
}