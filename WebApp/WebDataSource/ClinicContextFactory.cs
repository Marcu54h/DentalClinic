using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace WebDataSource
{
    public class ClinicContextFactory : IDesignTimeDbContextFactory<ClinicContext>
    {
        private readonly string? _connectionString = string.Empty;


        public ClinicContextFactory() 
        {
            _connectionString = "Server=(LocalDb)\\MSSQLLocalDB;Database=DentalClinicLocal;Integrated Security=True;MultipleActiveResultSets=true";
        }

        public ClinicContextFactory(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("DentalClinic");
        }

        public ClinicContext CreateDbContext(string[] args = default!)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ClinicContext>();
            optionsBuilder.UseSqlServer(_connectionString);
            return new ClinicContext(optionsBuilder.Options);
        }
    }
}
