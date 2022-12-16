using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Radzen;
using WebDataSource;
using WebModel;

namespace BlazorClinic.Pages
{
    public partial class Schedule : ComponentBase
    {
        [Inject]
        protected ILogger<Schedule> logger { get; set; } = default!;
        [Inject]
        protected ClinicContext context { get; set; } = default!;
        [Inject]
        public ContextMenuService contextMenu { get; set; } = default!;
        [Inject]
        public NavigationManager navigationManager { get; set; } = default!;

        IEnumerable<Visit> VisitEnum = Enumerable.Empty<Visit>();
        IList<Visit> selectedVisits = default!;

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            var visitsWithProps = context.Visits
                                         .Include(v => v.Employee)
                                         .ThenInclude(e => e.Person)
                                         .AsNoTracking();

            if (context is null)
            {
                logger.LogError("Visits component - ClinicContext is null!");
                return;
            }

            if (visitsWithProps is not null) 
            {
                VisitEnum = await visitsWithProps.ToListAsync();
            }
            else
            {
                logger.LogInformation("context.Visits returns no visits data.");
            }

            
        }

        void OnCellContextMenu(DataGridCellMouseEventArgs<Visit> args)
        {
            selectedVisits = new List<Visit>() { args.Data };
            contextMenu.Open(args,
                new List<ContextMenuItem> {
                    new ContextMenuItem() { Text = "Nowy pacjent", Value = "/patient/new" },
                    new ContextMenuItem() { Text = "Conetxt Menu Item 2", Value = "/patient/edit" },
                    new ContextMenuItem() { Text = "Conetxt Menu Item 3", Value = 3 }
                },
                (e) => {

                    logger.LogInformation($"Menu item with Value={e.Value} clicked. Column: {args.Column.Property}, EmployeeID: {args.Data.Id}");
                    navigationManager.NavigateTo(e.Value.ToString()!);
                    contextMenu.Close();
                }
            );
        }

        void OnRowSelect(Patient patient)
        {
            var p = patient;
        }

        protected void OnSlotRender(SchedulerSlotRenderEventArgs args)
        {

            // Highlight today in month view
            if (args.View.Text == "Miesiąć" && args.Start.Date == DateTime.Today)
            {
                args.Attributes["style"] = "background: rgba(255,220,40,.2);";
            }

            // Highlight working hours (9-18)
            if ((args.View.Text == "Tydzień" || args.View.Text == "Dzień") && args.Start.Hour > 8 && args.Start.Hour < 19)
            {
                args.Attributes["style"] = "background: rgba(255,220,40,.2);";
            }
        }
    }
}
