using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Radzen;
using Radzen.Blazor.Rendering;
using Radzen.Blazor;
using System;
using WebDataSource;
using WebModel;
using BlazorClinic.Pages.Visits;
using System.Collections.Generic;

namespace BlazorClinic.Pages
{
    public partial class Schedule : ComponentBase
    {
        public RadzenScheduler<Visit> scheduler = new();

        [Inject]
        protected ILogger<Schedule> logger { get; set; } = default!;
        [Inject]
        protected ClinicContext context { get; set; } = default!;
        [Inject]
        public ContextMenuService contextMenu { get; set; } = default!;
        [Inject]
        public NavigationManager navigationManager { get; set; } = default!;

        IList<Visit> VisitList = new List<Visit>();
        IList<Visit> selectedVisits = default!;

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            var visitsWithProps = context.Visits
                                         .Include(v => v.Employee)
                                         .ThenInclude(e => e.Person)
                                         .Include(v => v.Patient)
                                         .ThenInclude(p => p.Person)
                                         .Include(v => v.Treatments)
                                         .Include(v => v.Comments)
                                         .Include(v => v.Teeth)
                                         .AsNoTracking();

            if (context is null)
            {
                logger.LogError("Visits component - ClinicContext is null!");
                return;
            }

            if (visitsWithProps is not null) 
            {
                VisitList = await visitsWithProps.ToListAsync();
            }
            else
            {
                logger.LogInformation("context.Visits returns no visits data.");
            }
        }

        async Task OnSlotSelect(SchedulerSlotSelectEventArgs args)
        {
            logger.LogInformation(@"SlotSelect: Start={start} End={end}", args.Start, args.End);

           /* Visit data = await DialogService.OpenAsync<AddVisit>("Umów nową wizytę",
                new Dictionary<string, object> { { "Start", args.Start }, { "End", args.End } });

            if (data != null)
            {
                VisitList.Add(data);
                // Either call the Reload method or reassign the Data property of the Scheduler
                await scheduler.Reload();
            }*/
           await Task.Delay(500);
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
            if (args.View.Text == "Month" && args.Start.Date == DateTime.Today)
            {
                args.Attributes["style"] = "background: rgba(255,220,40,.2);";
            }

            // Highlight working hours (9-18)
            if ((args.View.Text == "Week" || args.View.Text == "Day") && args.Start.Hour > 7 && args.Start.Hour < 22)
            {
                args.Attributes["style"] = "background: rgba(255,220,40,.2);";
            }
        }

        protected void OnVisitRender(SchedulerAppointmentRenderEventArgs<Visit> args)
        {
            string employeeFavoriteColor = args.Data.Employee.FavoriteColor;

            if (args.Data.filled)
            {
                args.Attributes["style"] = @"background-color: " + employeeFavoriteColor + ";";
            }
            else
            {
                if (employeeFavoriteColor.ToLower() == "white")
                {
                    args.Attributes["style"] = @"background-color: " + employeeFavoriteColor + "; " +
                                        "color: black; border-bottom-style: groove; " +
                                        "border-bottom-color: red; border-bottom-width: 2px;";
                }
                else
                {
                    args.Attributes["style"] = @"background-color: " + employeeFavoriteColor + "; " +
                                        "border-bottom-style: groove; border-bottom-color: red; border-bottom-width: 2px;";
                }
                
            }
            
        }

        async Task OnVisitSelect(SchedulerAppointmentSelectEventArgs<Visit> args)
        {
            logger.LogInformation(@"VisitSelect: Visit={args}", args.Data);

            Dictionary<string, object> editVisitDialogParams = new Dictionary<string, object>
            {
                {"Visit", args.Data },
            };

            await dialogService.OpenAsync<EditVisit>("Zmień wizytę", editVisitDialogParams);

            await scheduler.Reload();
        }
    }
}
