using BlazorClinic.Delegates;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Radzen;
using Radzen.Blazor;
using WebDataSource;

namespace BlazorClinic.Pages
{
    public partial class Index : RadzenComponent
    {
        private RadzenChart? chart = new RadzenChart();
        private bool loadingImgVisible { get; set; }

        private IList<DataItem> chartData2019 = new List<DataItem>();
        private IList<DataItem> chartData2020 = new List<DataItem>();
        private IList<DataItem> chartData2021 = new List<DataItem>();
        private IList<DataItem> chartData2022 = new List<DataItem>();

        public event ContextIsWorkingEventHandler ContextWork = default!;

        [Inject]
        protected ClinicContext context { get; set; } = default!;

        protected override void OnInitialized()
        {
            base.OnInitialized();
            loadingImgVisible = true;
        }

        protected override async Task OnParametersSetAsync()
        {
            await base.OnParametersSetAsync();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);
            if (firstRender)
            {
                //await initializeChartData();
            }
            
        }

        private async Task initializeChartData()
        {
            DateTime startMonth = new DateTime(2019, 1, 1);

            for (int i = 1; i <= 12; i++)
            {
                chartData2019.Add(new DataItem
                    {
                        Month = startMonth.ToString("MM"),
                        Count = await context.Visits
                                             .Where(v => v.Date.Year == startMonth.Year &&
                                                         v.Date.Month == startMonth.Month)
                                             .CountAsync()
                    }
                );

                chartData2020.Add(new DataItem
                    {
                        Month = startMonth.ToString("MM"),
                        Count = await context.Visits
                                                 .Where(v => v.Date.Year == startMonth.AddYears(1).Year &&
                                                             v.Date.Month == startMonth.Month)
                                                 .CountAsync()
                    }
                );

                chartData2021.Add(new DataItem
                    {
                        Month = startMonth.ToString("MM"),
                        Count = await context.Visits
                                                 .Where(v => v.Date.Year == startMonth.AddYears(2).Year &&
                                                             v.Date.Month == startMonth.Month)
                                                 .CountAsync()
                    }
                );

                chartData2022.Add(new DataItem
                    {
                        Month = startMonth.ToString("MM"),
                        Count = await context.Visits
                                                 .Where(v => v.Date.Year == startMonth.AddYears(3).Year &&
                                                             v.Date.Month == startMonth.Month)
                                                 .CountAsync()
                    }
                );
                startMonth = startMonth.AddMonths(1);
            }
            Task chartReloadTask = chart.Reload();
            await chartReloadTask;
            if (chartReloadTask.IsCompleted)
            {
                loadingImgVisible = false;
                //await chart.Reload();
                StateHasChanged();
            }
        }

        protected virtual void OnDataLoading() 
        {
            if (ContextWork is not null)
            {
                ContextWork(this, new ContextEventArgs { IsLoading = true });
            }
        }

        protected virtual void OnDataLoaded() { }

        class DataItem
        {
            public string Month { get; set; } = string.Empty;
            public int Count { get; set; }
        }
    }

    
}
