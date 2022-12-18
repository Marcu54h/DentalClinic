using BlazorClinic.Abstraction;
using Microsoft.EntityFrameworkCore;
using Radzen;
using Radzen.Blazor;
using WebModel;

namespace BlazorClinic.Pages.Patients
{
    public partial class Patients : ContextAwareRadzenComponent
    {
        IEnumerable<Patient> patients = Enumerable.Empty<Patient>();

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            
        }

        protected override async Task OnParametersSetAsync()
        {
            await base.OnParametersSetAsync();
            patients = Context.Patients
                              .Include(p => p.Person)
                              .AsNoTracking();
        }

    }
}
