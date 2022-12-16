﻿using Microsoft.EntityFrameworkCore;
using WebModel;

namespace WebDataSource
{
    public class ClinicContext : DbContext
    {
        public ClinicContext(DbContextOptions<ClinicContext> options) : base(options)
        {

        }

        public DbSet<Person> People { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Contract> Contracts { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Visit> Visits { get; set; }
        public DbSet<Tooth> Teeth { get; set; }
        public DbSet<Treatment> Treatments { get; set; }
        public DbSet<SubTreatment> SubTreatments { get; set; }
        public DbSet<Sub2Treatment> Sub2Treatments { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Office> Offices { get; set; }
        public DbSet<PriceList> PriceLists { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<SubGroup> SubGroups { get; set; }
        public DbSet<Sub2Group> Sub2Groups { get; set; }
        
        protected override void OnModelCreating(ModelBuilder mb)
        {
            mb.Entity<Employee>().HasOne(e => e.Person);
            mb.Entity<Employee>().HasOne(e => e.User);
            mb.Entity<Patient>().HasOne(p => p.Person);

            mb.Entity<Patient>().HasOne(p => p.Employee);
            mb.Entity<Patient>().HasOne(p => p.PriceList);
            mb.Entity<Patient>().HasOne(p => p.Person);
        }
    }
}
