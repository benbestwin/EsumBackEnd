using System;
using System.Collections.Generic;

namespace CompanyRevenueAPI.Models
{
    public partial class MonthlyRevenue
    {
        public string ReportDate { get; set; } = null!;
        public string YearMonth { get; set; } = null!;
        public string CompanyId { get; set; } = null!;
        public string CompanyName { get; set; } = null!;
        public string IndustryType { get; set; } = null!;
        public decimal? CurrentMonthRevenue { get; set; }
        public decimal? PreviousMonthRevenue { get; set; }
        public decimal? LastYearCurrentMonthRevenue { get; set; }
        public decimal? PreviousMonthComparison { get; set; }
        public decimal? LastYearSameMonthComparison { get; set; }
        public decimal? AccumulatedCurrentMonthRevenue { get; set; }
        public decimal? AccumulatedLastYearRevenue { get; set; }
        public decimal? PreviousPeriodComparison { get; set; }
        public string? Notes { get; set; }
    }
}
