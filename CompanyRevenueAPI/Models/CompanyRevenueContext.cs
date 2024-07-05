using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CompanyRevenueAPI.Models
{
    public partial class CompanyRevenueContext : DbContext
    {
        public CompanyRevenueContext()
        {
        }

        public CompanyRevenueContext(DbContextOptions<CompanyRevenueContext> options)
            : base(options)
        {
        }

        public virtual DbSet<MonthlyRevenue> MonthlyRevenues { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Name=DefaultConnection");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MonthlyRevenue>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("MonthlyRevenue");

                entity.Property(e => e.AccumulatedCurrentMonthRevenue).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.AccumulatedLastYearRevenue).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.CompanyId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("CompanyID");

                entity.Property(e => e.CompanyName).HasMaxLength(100);

                entity.Property(e => e.CurrentMonthRevenue).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.IndustryType).HasMaxLength(50);

                entity.Property(e => e.LastYearCurrentMonthRevenue).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.LastYearSameMonthComparison).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Notes).HasMaxLength(500);

                entity.Property(e => e.PreviousMonthComparison).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.PreviousMonthRevenue).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.PreviousPeriodComparison).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.ReportDate)
                    .HasMaxLength(7)
                    .IsUnicode(false);

                entity.Property(e => e.YearMonth)
                    .HasMaxLength(5)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
