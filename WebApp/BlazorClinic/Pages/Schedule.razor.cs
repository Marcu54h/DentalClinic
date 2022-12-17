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
        public RadzenScheduler<Visit>? scheduler = new();
        private bool contextIsBussy = false;

        [Inject]
        protected ILogger<Schedule> logger { get; set; } = default!;
        [Inject]
        protected ClinicContext context { get; set; } = default!;
        [Inject]
        public ContextMenuService contextMenu { get; set; } = default!;
        [Inject]
        public NavigationManager navigationManager { get; set; } = default!;

        IList<Visit> VisitList = new List<Visit>();
        IList<Employee> Employees = new List<Employee>();
        
        IList<Visit> selectedVisits = default!;

        private DateTime currentDate;

        protected override async Task OnParametersSetAsync()
        {
            await base.OnParametersSetAsync();
            
        }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            var getEmployees = context.Employees
                                      .Include(e => e.Person)
                                      .AsNoTracking();
            contextIsBussy = true;

            var getEmployeesTask = getEmployees.ToListAsync();

            Employees = await getEmployeesTask;

            if (getEmployeesTask.IsCompleted)
            {
                contextIsBussy = false;
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

        protected async Task OnLoadData(SchedulerLoadDataEventArgs args)
        {
            
            if (!contextIsBussy)
            {
                contextIsBussy = true;

                var visitsWithProps = context.Visits
                                         .Where(v => v.Date > args.Start && v.Date < args.End)
                                         .Include(v => v.Employee)
                                         .ThenInclude(e => e.Person)
                                         .Include(v => v.Patient)
                                         .ThenInclude(p => p.Person)
                                         .Include(v => v.Treatments)
                                         .Include(v => v.Comments)
                                         .Include(v => v.Teeth)
                                         .AsNoTracking();

                Task<List<Visit>>  dataLoadingTask = visitsWithProps.ToListAsync();

                if (context is null)
                {
                    logger.LogError("Visits component - ClinicContext is null!");
                    return;
                }

                VisitList = await dataLoadingTask;

                if (dataLoadingTask.IsCompleted)
                {
                    contextIsBussy = false;
                }
            }
        }

        protected void OnSlotRender(SchedulerSlotRenderEventArgs args)
        {
            currentDate = scheduler!.CurrentDate;
            // Highlight today in month view
            if (args.View.Text == "Miesiąc" && args.Start.Date == DateTime.Today)
            {
                args.Attributes["style"] = "background: rgba(255,220,40,.2);";
            }

            // Highlight working hours (9-18)
            if ((args.View.Text == "Tydzień" || args.View.Text == "Dzień") && args.Start.Hour > 7 && args.Start.Hour < 22)
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

            await scheduler!.Reload();
        }

        void ShowNotification(NotificationMessage message)
        {
            notificationService.Notify(message);

            logger.LogInformation(@"{severity} notification", message.Severity);
        }

    }
}
