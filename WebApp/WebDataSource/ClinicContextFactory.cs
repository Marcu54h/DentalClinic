using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;

namespace WebDataSource
{
    public class ClinicContextFactory : IDesignTimeDbContextFactory<ClinicContext>
    {
        public ClinicContext CreateDbContext(string[] args = default!)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ClinicContext>();
            optionsBuilder.UseSqlServer("Server=10.6.0.5;Database=DentalClinic;User Id=dental;Password=Sakerhet;Trusted_Connection=True;MultipleActiveResultSets=true");
            return new ClinicContext(optionsBuilder.Options);
        }
    }
}
