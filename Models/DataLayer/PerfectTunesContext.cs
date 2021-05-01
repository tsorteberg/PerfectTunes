/***************************************************************
* Name        : PerfectTunesContext.cs
* Author      : Tom Sorteberg
* Created     : 04/18/2021
* Course      : CIS 174
* Version     : 1.0
* OS          : Windows 10 Pro, Visual Studio Community 2019
* Copyright   : This is my own original work based on
*               specifications issued by our instructor
* Description : Final Project.
* I have not used unauthorized source code, either modified or 
* unmodified. I have not given other fellow student(s) access 
* to my program.         
***************************************************************/
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace PerfectTunes.Models
{
    public class PerfectTunesContext : IdentityDbContext<User>
    {
        public PerfectTunesContext(DbContextOptions<PerfectTunesContext> options)
            : base(options)
        { }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Instrument> Instruments { get; set; }
        public DbSet<Department> Departments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Instrument>().HasOne(d => d.Department)
                .WithMany(i => i.Instruments)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.ApplyConfiguration(new SeedDepartments());
            modelBuilder.ApplyConfiguration(new SeedInstruments());
            modelBuilder.ApplyConfiguration(new SeedBrands());
        }
    }
}
