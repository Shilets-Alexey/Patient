using Microsoft.EntityFrameworkCore;
using PatientsApplication.DataAccess.Entities;
using System.Collections.Generic;

namespace PatientsApplication.DataAccess.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Patient> Patients { get; set; }

        public DbSet<Gender> Genders { get; set; }

        public DbSet<Active> Active { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            SetColumnsProperty<Patient>(modelBuilder);
            SetColumnsProperty<Gender>(modelBuilder);
            SetColumnsProperty<Active>(modelBuilder);
            FillingLookupWithDefaultValues<Gender>(modelBuilder, new[] { "male", "female", "other", "unknown" });
            FillingLookupWithDefaultValues<Active>(modelBuilder, new[] { "true", "false"});
        }

        private void SetColumnsProperty<T>(ModelBuilder modelBuilder) where T : BaseEntity
        {
            var entities = modelBuilder.Entity(typeof(T));
            entities.Property(nameof(BaseEntity.CreatedOn)).HasDefaultValueSql("getutcdate()");
            entities.Property(nameof(BaseEntity.ModifiedOn)).HasDefaultValueSql("getutcdate()");
        }

        private void FillingLookupWithDefaultValues<T>(ModelBuilder modelBuilder, string [] values) where T : BaseLookup, new()
        {
            if(values == default || values.Length == 0)
            {
                return;
            }
            foreach (string value in values)
            {
                T entity = new T() { Name = value };
                modelBuilder.Entity<T>().HasData(entity);
            }
        }
    }
}