using EmployeeManagement.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Repository.Context
{
    public partial class EmployeeDbContext: DbContext
    {
        public virtual DbSet<Employee> Employee { get; set; }
        public EmployeeDbContext(DbContextOptions<EmployeeDbContext> options)
        : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.ToTable("Employee");

                entity.Property(e => e.FirstName);
                entity.Property(e => e.LastName);
                entity.Property(e => e.Email);
                entity.Property(e => e.Position);
                entity.Property(e => e.Phone);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
