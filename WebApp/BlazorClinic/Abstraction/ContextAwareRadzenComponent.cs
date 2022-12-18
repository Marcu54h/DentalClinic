using Microsoft.AspNetCore.Components;
using Radzen;
using WebDataSource;

namespace BlazorClinic.Abstraction
{
    public class ContextAwareRadzenComponent : RadzenComponent
    {
        [Inject]
        protected ClinicContext Context { get; set; } = default!;
    }
}
